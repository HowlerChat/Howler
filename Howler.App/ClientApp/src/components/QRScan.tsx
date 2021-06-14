import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import {faChevronDown} from '@fortawesome/free-solid-svg-icons';
import ChannelGroup from './ChannelGroup';
import QRCode from 'react-qr-code';
import './QRLogin.scss';
import { isFunction } from 'util';

type QRScanProps = RouteComponentProps<{ id: string }>;;

export default class QRScan extends React.PureComponent<QRScanProps, { keyPair?: CryptoKeyPair | null, authData?: any | null, confirmValue?: string | null }> {
    constructor(props: QRScanProps) {
        super(props);

        this.setAuthData = this.setAuthData.bind(this);
        this.setConfirmValue = this.setConfirmValue.bind(this);

        let setConfirmValue = this.setConfirmValue;
        crypto.subtle.generateKey({
                name: "ECDH",
                namedCurve: "P-256"
            },
            true,
            ["deriveBits"]).then(key => {
            crypto.subtle.exportKey('jwk', key.publicKey).then(
                pubKey => {
                    fetch("http://localhost:5000/qrauth/" + props.match.params.id, {
                        method: 'POST',
                        headers: { 'Content-Type' : 'application/json' },
                        body: JSON.stringify({"authorizedPublicKey": pubKey})
                    }).then(data => {
                        if (data.ok) {
                            data.json().then(d => this.setAuthData(d));
                            
                            window.setInterval(function() {
                                fetch("http://localhost:5000/qrauth/" + props.match.params.id).then(newData => {
                                    if (newData.ok) {
                                        newData.json().then(nd => {
                                            if (nd.unauthorizedPublicKey != null) {
                                                window.crypto.subtle.importKey(
                                                    "jwk",
                                                    nd.unauthorizedPublicKey,
                                                    {
                                                        name: "ECDH",
                                                        namedCurve: "P-256"
                                                    },
                                                    false,
                                                    []).then(unAuthPubKey => {
                                                        crypto.subtle.deriveBits({ name: "ECDH", public: unAuthPubKey }, key.privateKey, 128).then(derivedKey => {
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
                                                                let enc = new TextEncoder();
                                                                crypto.subtle.encrypt({name: "AES-CTR", counter: new Uint8Array(16), length: 128}, k, enc.encode(JSON.stringify({...window.localStorage}))).then(payload => {
                                                                    var binary = '';
                                                                    var bytes = new Uint8Array(payload);
                                                                    var len = bytes.byteLength;
                                                                    for (var i = 0; i < len; i++) {
                                                                        binary += String.fromCharCode( bytes[ i ] );
                                                                    }
                                                                    let result = window.btoa( binary );
                                                                    fetch("http://localhost:5000/qrauth/" + props.match.params.id,
                                                                    {
                                                                        method: "POST",
                                                                        headers: { 'Content-Type' : 'application/json' },
                                                                        body: JSON.stringify({...nd, payload: result})
                                                                    })
                                                                })
                                                            })
                                                            crypto.subtle.digest('SHA-256', derivedKey).then(
                                                                hash => {
                                                                    setConfirmValue(Array.from(new Uint8Array(hash)).map(b => b.toString(16).padStart(2, '0')).join(""));
                                                                });
                                                        });
                                                });
                                            }
                                        });
                                    }
                                });
                            }, 2000);
                        }
                    });

                    this.setState(s => {
                        return {
                            ...s,
                            authorizedKeyPair: key
                        };
                    });
                }
            )});

        this.state = {
            keyPair: null,
            authData: null
        };
    }

    public setAuthData(data: any) {
        this.setState({authData: data});
    }

    public setConfirmValue(derivedKey: string) {
        this.setState({confirmValue: derivedKey});
    }

    public render() {
        return <div className="sign-in-qr">
            <span>{this.props.match.params.id}</span><br/>
            <span>{this.state.confirmValue}</span>
        </div>
    }
}