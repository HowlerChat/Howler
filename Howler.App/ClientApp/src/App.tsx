import React from 'react';
import { connect } from 'react-redux';
import { Route } from 'react-router';
import { AmplifyAuthenticator, AmplifySignIn, AmplifySignOut, AmplifySignUp } from '@aws-amplify/ui-react';
import { AuthState, onAuthUIStateChange } from '@aws-amplify/ui-components';
import QRLogin from './components/QRLogin';
import QRScan from './components/QRScan';
import Layout from './components/Layout';
import Space from './components/Space';
import { ApplicationState } from './store';
import * as Localization from './store/Localization';

import './sass/custom.scss';

const App: React.FunctionComponent<typeof Localization.actionCreators> = (props) => {
    const [authState, setAuthState] = React.useState<AuthState>();
    const [user, setUser] = React.useState<any>();

    React.useEffect(() => {
        props.requestLocalization("en-US");
        return onAuthUIStateChange((nextAuthState, authData) => {
            console.log(nextAuthState);
            setAuthState(nextAuthState);
            setUser(authData);
        });
    }, []);

    return authState === AuthState.SignedIn && user ? (<>
        <Route exact path='/spaces/:spaceId/:channelId' component={(props: any) => <Layout setAuthState={setAuthState}><Space {...props} user={user} /></Layout>} />
        <Route exact path='/qrauth/:id' component={(props: any) => <QRScan {...props}/>} />
    </>) : <div className="login-pane">
        <div/>
        <div className="sign-in">
            <AmplifyAuthenticator usernameAlias="email">
                <AmplifySignUp
                    slot="sign-up"
                    usernameAlias="email"
                    formFields={[
                    {
                        type: "email",
                        label: "Custom Email Label",
                        placeholder: "Custom email placeholder",
                        inputProps: { required: true, autocomplete: "username" },
                    },
                    {
                        type: "password",
                        label: "Custom Password Label",
                        placeholder: "Custom password placeholder",
                        inputProps: { required: true, autocomplete: "new-password" },
                    },
                    {
                        type: "phone_number",
                        label: "Custom Phone Label",
                        placeholder: "Custom phone placeholder",
                    },
                    ]} 
                />
                <AmplifySignIn slot="sign-in" usernameAlias="email" />
            </AmplifyAuthenticator>
        </div>
        <div className="vertical-divider"/>
        <QRLogin />
        <div/>
    </div>;
};

export default connect(
    (state: ApplicationState) => state.localizations,
    Localization.actionCreators
)(App);
