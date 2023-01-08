import * as React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import {faChevronDown} from '@fortawesome/free-solid-svg-icons';
import ChannelGroup from './ChannelGroup';
import QRCode from 'react-qr-code';
import './QRLogin.scss';
import { isFunction } from 'util';

type QRLoginProps = { };

export default class QRLogin extends React.PureComponent<QRLoginProps, { keyPair?: CryptoKeyPair | null, identifier?: string | null, authData?: any | null, confirmValue?: string | null }> {
    constructor(props: QRLoginProps) {
        super(props);

        this.setAuthData = this.setAuthData.bind(this);
        let setAuthData = this.setAuthData;
        crypto.subtle.generateKey({
                name: "ECDH",
                namedCurve: "P-256"
            },
            true,
            ["deriveBits"]).then(key => {
                crypto.subtle.exportKey('jwk', key.publicKey).then(
                    jwk => {
                        let enc = new TextEncoder();
                        crypto.subtle.digest('SHA-256', enc.encode(JSON.stringify(jwk))).then(
                            hash => {
                                const id = Array.from(new Uint8Array(hash)).map(b => b.toString(16).padStart(2, '0')).join("");
                                window.setInterval(async function() {
                                    let data = await fetch("http://localhost:5000/qrauth/" + id);

                                    if (data.ok) {
                                        setAuthData(await data.json());
                                    }
                                }, 2000);

                                this.setState(s => {
                                    return {
                                        ...s,
                                        keyPair: key,
                                        identifier: id
                                    };
                                });
                            })
                        })
                    }
                );

        this.state = {
            keyPair: null,
            identifier: null,
            authData: null
        };
    }

    public setAuthData(data: any) {
        window.crypto.subtle.importKey(
            "jwk",
            data.authorizedPublicKey,
            {
                name: "ECDH",
                namedCurve: "P-256"
            },
            false,
            []).then(pubKey => {
                if (this.state.keyPair != null) {
                    crypto.subtle.deriveBits({ name: "ECDH", public: pubKey }, this.state.keyPair.privateKey, 128).then(derivedKey => {
                        if (this.state.keyPair != null) {
                            crypto.subtle.exportKey('jwk', this.state.keyPair.publicKey).then(jwk => {
                                fetch("http://localhost:5000/qrauth/" + this.state.identifier,
                                {
                                    method: "POST",
                                    headers: { 'Content-Type' : 'application/json' },
                                    body: JSON.stringify({...data, "unauthorizedPublicKey": jwk })
                                })
                            });
                        }
                        window.crypto.subtle.importKey(
                            "raw",
                            derivedKey,
                            {
                                name: "AES-CTR",
                                length: 128
                            },
                            false,
                            ["encrypt", "decrypt"]
                            ).then(k => {
                                if (data.payload != null) {
                                    let binary_string =  window.atob(data.payload);
                                    let len = binary_string.length;
                                    let bytes = new Uint8Array( len );
                                    for (let i = 0; i < len; i++)        {
                                        bytes[i] = binary_string.charCodeAt(i);
                                    }
                                    // TODO: other than the obvious refactoring, don't just let this come through, make the user confirm.
                                    crypto.subtle.decrypt({name: "AES-CTR", counter: new Uint8Array(16), length: 128}, k, bytes.buffer).then(v =>  {
                                        let dec = new TextDecoder();
                                        let output = dec.decode(v);
                                        let obj = JSON.parse(output);
                                        for (let k in obj) {
                                            window.localStorage.setItem(k, obj[k]);
                                        }
                                        window.location.reload();
                                    })
                                }
                            })

                        crypto.subtle.digest('SHA-256', derivedKey).then(
                            hash => {
                                this.setState({confirmValue: Array.from(new Uint8Array(hash)).map(b => b.toString(16).padStart(2, '0')).join("")})
                            });
                    });
                }
            });
        this.setState({authData: data});
    }

    public render() {
        return this.state.identifier != null ? <div className="qr-login">
            <div className="sign-in-qr">
                <QRCode value={"http://localhost:8000/qrauth/" + this.state.identifier} level="H"/>
            </div>
            <span>Confirm value: {this.state.confirmValue}</span>
        </div> : <div className="sign-in-qr-loading"></div>;
    }
}