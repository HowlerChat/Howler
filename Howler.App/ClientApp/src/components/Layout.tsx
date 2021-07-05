import * as React from 'react';
import { AuthState } from '@aws-amplify/ui-components';
import NavMenu from './navbar/NavMenu';
import CloseButton from './CloseButton';
import Container from './Container';
import CreateSpaceModal from './modals/CreateSpaceModal';
import JoinSpaceModal from './modals/JoinSpaceModal';

const Layout : React.FunctionComponent<{ setAuthState: React.Dispatch<React.SetStateAction<AuthState | undefined>> }> = props => {
    let [createSpaceVisible, setCreateSpaceVisible] = React.useState(false);
    let [joinSpaceVisible, setJoinSpaceVisible] = React.useState(false);

    return (
        <React.Fragment>
            <CreateSpaceModal visible={createSpaceVisible} onClose={() => setCreateSpaceVisible(false)}/>
            <JoinSpaceModal visible={joinSpaceVisible} onClose={() => setJoinSpaceVisible(false)}/>
            <NavMenu setAuthState={props.setAuthState} showCreateSpaceModal={() => setCreateSpaceVisible(true)} showJoinSpaceModal={() => setJoinSpaceVisible(true)}/>
            <CloseButton />
            <Container>
                {props.children}
            </Container>
        </React.Fragment>
    );
};

export default Layout;