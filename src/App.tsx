import React from 'react';
import { connect } from 'react-redux';
import { Redirect, Route, useHistory } from 'react-router';
import * as ethers from 'ethers';

import Layout from './components/Layout';
import Space from './components/Space';
import { ApplicationState } from './store';
import * as Localization from './store/Localization';
import * as Farcaster from './store/Farcaster';
import { LocalizationInfoState } from './store/Localization';
import Connecting from './components/Connecting';

import './sass/custom.scss';
import JoinSpaceModal from './components/modals/JoinSpaceModal';
import Button from './components/Button';

type AppProps = typeof Localization.actionCreators & typeof Farcaster.actionCreators & ApplicationState;

enum AuthState {
    LoggedOut = 0,
    SignedIn = 1,
}

const App: React.FunctionComponent<AppProps> = (props) => {
    const [authState, setAuthState] = React.useState<AuthState>(AuthState.LoggedOut);
    const [user, setUser] = React.useState<ethers.Wallet>();
    const [phrase, setPhrase] = React.useState<string>();
    const history = useHistory();

    const login = () => {
        setAuthState(AuthState.SignedIn);
        const wallet = ethers.Wallet.fromMnemonic(phrase!, "m/44'/60'/0'/0/0");
        props.requestConnection(wallet);
        setUser(wallet);
        window.localStorage.setItem("mnemonic", phrase!);
    }
    
    React.useEffect(() => {
        props.requestLocalization("en-US");

        if (authState == AuthState.LoggedOut) {
            const mnemonic = window.localStorage.getItem("mnemonic");

            if (mnemonic) {
                const wallet = ethers.Wallet.fromMnemonic(mnemonic, "m/44'/60'/0'/0/0");
                props.requestConnection(wallet);
                setUser(wallet);
                setAuthState(AuthState.SignedIn);
            }
        }
    }, []);
    
    return authState === AuthState.SignedIn && user ?
        // todo: without even really saying, holy shit this needs a refactor
        (props.farcaster.isLoading ?
            <>
                <Route>
                    <Connecting />
                </Route>
            </> : 
            <>
                <Route exact path='/'>
                    <Redirect to={'/casts'} />
                </Route>
                <Route exact path='/casts' component={(p: any) => <Layout><Space {...p} connection={props.farcaster.connection} casts={props.farcaster.casts} user={user} /></Layout>} />
            </>) :
        <div className="login-pane">
            <div/>
            <div className="sign-in">
                <div className="sign-in-header">
                    <img width="100" height="100" src={process.env.PUBLIC_URL + "/howler.png"}/> Howlcaster
                </div>
                <div className="sign-in-info">
                    Enter your seed phrase to sign in:
                </div>
                <div className="seed-phrase">
                    <textarea className="seed-phrase-input" onChange={(e) => setPhrase(e.target.value)} value={phrase}/>
                </div>
                <div className="sign-in-buttons">
                    <Button type="primary" onClick={() => login()}>Log In</Button>
                </div>
            </div>
            {/*<div className="vertical-divider"/>
            <QRLogin />*/}
            <div/>
        </div>;
};

export default connect(
    (state: ApplicationState) => { return {...state}; },
    { ...Localization.actionCreators, ...Farcaster.actionCreators }
)(App);
