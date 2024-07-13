import { Buffer } from 'buffer'
import React from 'react';
import { connect } from 'react-redux';
import { Navigate, Route, Routes } from 'react-router';
import Layout from './components/Layout';
import Space from './components/Space';
import { ApplicationState } from './store';
import * as Localization from './store/Localization';
import * as Servers from './store/Servers';
import * as Spaces from './store/Spaces';
import { LocalizationInfoState } from './store/Localization';
import Connecting from './components/Connecting';

import './sass/custom.scss';
import JoinSpaceModal from './components/modals/JoinSpaceModal';
import { Login } from './components/Login';
window.Buffer = Buffer;

type AppProps = typeof Localization.actionCreators & typeof Servers.actionCreators & ApplicationState;

const App: React.FunctionComponent<AppProps> = (props) => {
    const [authState, setAuthState] = React.useState<string>();
    const [user, setUser] = React.useState<any>({
      displayName: "Cassie",
      userTag: "Cassie#6463",
      userName: "@cassie",
      state: "online",
      status: "Custom Status",
      userIcon: "https://avatars.githubusercontent.com/u/7929478?v=4",
    });
    
    React.useEffect(() => {
        props.requestLocalization("en-US");
        // return onAuthUIStateChange((nextAuthState, authData) => {
        //     setAuthState(nextAuthState);
        //     setUser(authData);
        //     if (nextAuthState == AuthState.SignedIn) {
        //         const user = authData as any;
        //         props.setAuthToken(user.signInUserSession.accessToken.jwtToken);
        //         props.requestConnection('3ec22786-bc0d-4adf-b1f7-69c65c00f162');
        //     }
        // });
    }, []);

    React.useEffect(() => {
        if (props.servers != null && 
            props.servers.servers['3ec22786-bc0d-4adf-b1f7-69c65c00f162'] != null &&
            props.servers.connection != null &&
            props.servers.connection.readyState === WebSocket.OPEN &&
            props.servers.servers['3ec22786-bc0d-4adf-b1f7-69c65c00f162'].userSpaces == null) {

            // props.servers.servers['3ec22786-bc0d-4adf-b1f7-69c65c00f162'].connection.on("GetUserSpacesResponse", (userSpaces) => {
            //     props.updateUserSpaces('3ec22786-bc0d-4adf-b1f7-69c65c00f162', userSpaces);
            //     props.servers!.servers['3ec22786-bc0d-4adf-b1f7-69c65c00f162'].connection!.send("SubscribeToSpacesAndChannel", Object.keys(userSpaces), "", "")
            // });
            // props.servers.servers['3ec22786-bc0d-4adf-b1f7-69c65c00f162'].connection.on("NoUserSpacesFound", () => {
            //     history.push("/spaces/join");
            // });
            // props.servers.servers['3ec22786-bc0d-4adf-b1f7-69c65c00f162'].connection.send("GetUserSpaces");
        }
    }, [props.servers])

    return !props.localizations || props.localizations.isLoading || !props.localizations.localizations["JOIN_SPACE_TITLE"] ? <></> : 
    // authState === "SignedIn" && user ?
    //     (!user.attributes.sub || props.servers == null || props.servers.servers['3ec22786-bc0d-4adf-b1f7-69c65c00f162'] == null || props.servers.isConnecting || props.servers.connection == null || props.servers.connection.readyState !== WebSocket.OPEN) ?
    //         <>
    //           <Connecting />
    //         </> : 
    //         (Object.keys((props.servers.servers['3ec22786-bc0d-4adf-b1f7-69c65c00f162'].userSpaces || {})).length == 0) ?
                <Routes>
                    <Route path="/" element={<Login/>} />
                    <Route path="/spaces/join" element={<JoinSpaceModal {...props} onClose={()=>{}} visible={true} />} />
                    <Route path='/spaces/:spaceId/:channelId' element={<Layout><Space {...props} setAuthState={setAuthState} user={user} /></Layout>} />
                </Routes>;
};

export default connect(
    (state: ApplicationState) => { return state; },
    { ...Localization.actionCreators, ...Servers.actionCreators, ...Spaces.actionCreators }
)(App);
