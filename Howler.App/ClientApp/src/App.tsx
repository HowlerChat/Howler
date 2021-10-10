import React from 'react';
import { connect } from 'react-redux';
import { Redirect, Route, useHistory } from 'react-router';
import { AmplifyAuthenticator, AmplifySignIn, AmplifySignOut, AmplifySignUp } from '@aws-amplify/ui-react';
import { AuthState, onAuthUIStateChange } from '@aws-amplify/ui-components';
import QRLogin from './components/QRLogin';
import QRScan from './components/QRScan';
import Layout from './components/Layout';
import Space from './components/Space';
import { ApplicationState } from './store';
import * as Localization from './store/Localization';
import * as Servers from './store/Servers';
import * as signalR from '@microsoft/signalr';
import { LocalizationInfoState } from './store/Localization';
import Connecting from './components/Connecting';

import './sass/custom.scss';
import JoinSpaceModal from './components/modals/JoinSpaceModal';

type AppProps = typeof Localization.actionCreators & typeof Servers.actionCreators & ApplicationState;

const App: React.FunctionComponent<AppProps> = (props) => {
    const [authState, setAuthState] = React.useState<AuthState>();
    const [user, setUser] = React.useState<any>();
    const history = useHistory();
    
    React.useEffect(() => {
        props.requestLocalization("en-US");
        return onAuthUIStateChange((nextAuthState, authData) => {
            setAuthState(nextAuthState);
            setUser(authData);
            if (nextAuthState == AuthState.SignedIn) {
                const user = authData as any;
                props.setAuthToken(user.signInUserSession.accessToken.jwtToken);
                props.requestConnection('3ec22786-bc0d-4adf-b1f7-69c65c00f162');
            }
        });
    }, []);

    React.useEffect(() => {
        if (props.servers != null && 
            props.servers.servers['3ec22786-bc0d-4adf-b1f7-69c65c00f162'] != null &&
            props.servers.servers['3ec22786-bc0d-4adf-b1f7-69c65c00f162'].connection != null &&
            props.servers.servers['3ec22786-bc0d-4adf-b1f7-69c65c00f162'].connection.state === signalR.HubConnectionState.Connected &&
            props.servers.servers['3ec22786-bc0d-4adf-b1f7-69c65c00f162'].userSpaces == null) {

            props.servers.servers['3ec22786-bc0d-4adf-b1f7-69c65c00f162'].connection.on("GetUserSpacesResponse", (userSpaces) => {
                props.updateUserSpaces('3ec22786-bc0d-4adf-b1f7-69c65c00f162', userSpaces);
                props.servers!.servers['3ec22786-bc0d-4adf-b1f7-69c65c00f162'].connection!.send("SubscribeToSpacesAndChannel", Object.keys(userSpaces), "", "")
            });
            props.servers.servers['3ec22786-bc0d-4adf-b1f7-69c65c00f162'].connection.on("NoUserSpacesFound", () => {
                history.push("/spaces/join");
            });
            props.servers.servers['3ec22786-bc0d-4adf-b1f7-69c65c00f162'].connection.send("GetUserSpaces");
        }
    }, [props.servers])

    return authState === AuthState.SignedIn && user ?
        // todo: without even really saying, holy shit this needs a refactor
        (!user.attributes.sub || props.servers == null || props.servers.servers['3ec22786-bc0d-4adf-b1f7-69c65c00f162'] == null || props.servers.servers['3ec22786-bc0d-4adf-b1f7-69c65c00f162'].isConnecting || props.servers.servers['3ec22786-bc0d-4adf-b1f7-69c65c00f162'].connection == null || props.servers.servers['3ec22786-bc0d-4adf-b1f7-69c65c00f162'].connection.state !== signalR.HubConnectionState.Connected) ?
            <>
                <Route>
                    <Connecting />
                </Route>
            </> : 
            (Object.keys((props.servers.servers['3ec22786-bc0d-4adf-b1f7-69c65c00f162'].userSpaces || {})).length == 0) ?
                <>
                    <Route exact path="/spaces/join" component={(props: any) => <JoinSpaceModal {...props} serverId="3ec22786-bc0d-4adf-b1f7-69c65c00f162" visible={true} />} />
                </> :
                <>
                    <Route exact path='/'>
                        <Redirect to={'/servers/' + props.configs[user.attributes.sub].serverIds[0] + '/spaces/' + props.configs[user.attributes.sub].spaceIds[0] + "/" + props.configs[user.attributes.sub].spaceIds[0]} />
                    </Route>
                    <Route exact path='/servers/:serverId/spaces/:spaceId/:channelId' component={(props: any) => <Layout><Space {...props} setAuthState={setAuthState} user={user} /></Layout>} />
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
    (state: ApplicationState) => { return {...state}; },
    { ...Localization.actionCreators, ...Servers.actionCreators }
)(App);
