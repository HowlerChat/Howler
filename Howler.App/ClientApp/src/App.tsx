import React from 'react';
import { connect } from 'react-redux';
import { Redirect, Route } from 'react-router';
import { AmplifyAuthenticator, AmplifySignIn, AmplifySignOut, AmplifySignUp } from '@aws-amplify/ui-react';
import { AuthState, onAuthUIStateChange } from '@aws-amplify/ui-components';
import QRLogin from './components/QRLogin';
import QRScan from './components/QRScan';
import Layout from './components/Layout';
import Space from './components/Space';
import { ApplicationState } from './store';
import * as Localization from './store/Localization';
import * as Connection from './store/Connection';
import * as signalR from '@microsoft/signalr';
import { ConnectionInfoState } from './store/Connection';
import { LocalizationInfoState } from './store/Localization';
import Connecting from './components/Connecting';

import './sass/custom.scss';
import JoinSpaceModal from './components/modals/JoinSpaceModal';

type AppProps = typeof Localization.actionCreators & typeof Connection.actionCreators &
{
    localizations: LocalizationInfoState | undefined,
    connections: ConnectionInfoState | undefined,
};

const App: React.FunctionComponent<AppProps> = (props) => {
    const [authState, setAuthState] = React.useState<AuthState>();
    const [user, setUser] = React.useState<any>();

    React.useEffect(() => {
        props.requestLocalization("en-US");
        return onAuthUIStateChange((nextAuthState, authData) => {
            setAuthState(nextAuthState);
            setUser(authData);
            if (nextAuthState == AuthState.SignedIn) {
                const user = authData as any;
                props.requestConnection(user.signInUserSession.accessToken.jwtToken);
            }
        });
    }, []);

    return authState === AuthState.SignedIn && user ?
        (!user.attributes.sub || props.connections == null || props.connections.isConnecting || props.connections.connection == null || props.connections.connection.state !== signalR.HubConnectionState.Connected ||  Object.keys(props.connections.userSpaces).length == 0) ?
            <>
                <Route>
                    <Connecting />
                </Route>
                <Route exact path="/spaces/join" component={(props: any) => <JoinSpaceModal {...props} visible={true} />} />
            </> :
            <>
                <Route exact path='/'>
                    <Redirect to={'/spaces/' + props.connections.userSpaces[user.attributes.sub][0] + "/" + props.connections.userSpaces[user.attributes.sub][0]} />
                </Route>
                <Route exact path='/spaces/:spaceId/:channelId' component={(props: any) => <Layout><Space {...props} setAuthState={setAuthState} user={user} /></Layout>} />
                <Route exact path='/qrauth/:id' component={(props: any) => <QRScan {...props}/>} />
            </> :
            <div className="login-pane">
                <div/>
                <div className="sign-in">
                    <AmplifyAuthenticator usernameAlias="email">
                        <AmplifySignUp
                            slot="sign-up"
                            headerText="Create a Howler Account"
                            usernameAlias="email"
                            formFields={[
                            {
                                type: "email",
                                label: "Email Address",
                                placeholder: "valid@example.com",
                                inputProps: { required: true, autocomplete: "username" },
                            },
                            {
                                type: "password",
                                label: "Password",
                                placeholder: "Password",
                                inputProps: { required: true, autocomplete: "new-password" },
                            },
                            {
                                type: "phone_number",
                                label: "Phone Number",
                                placeholder: "(833)-793-2796",
                            },
                            ]} 
                        />
                        <AmplifySignIn slot="sign-in" headerText="Sign in to Howler" usernameAlias="email" />
                    </AmplifyAuthenticator>
                </div>
                {/*<div className="vertical-divider"/>
                <QRLogin />*/}
                <div/>
            </div>;
};

export default connect(
    (state: ApplicationState) => { return { localizations: state.localizations, connections: state.connections }; },
    { ...Localization.actionCreators, ...Connection.actionCreators }
)(App);
