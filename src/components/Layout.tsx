import * as React from 'react';
import { AuthState } from '@aws-amplify/ui-components';
import NavMenu from './navbar/NavMenu';
import CloseButton from './CloseButton';
import Container from './Container';
import CreateSpaceModal from './modals/CreateSpaceModal';
import JoinSpaceModal from './modals/JoinSpaceModal';

const Layout : React.FunctionComponent<{}> = props => {
    let [createSpaceVisible, setCreateSpaceVisible] = React.useState(false);
    let [joinSpaceVisible, setJoinSpaceVisible] = React.useState(false);

    return (
        <React.Fragment>
            {/* <CreateSpaceModal visible={createSpaceVisible} onClose={() => setCreateSpaceVisible(false)}/> */}
            {/* <JoinSpaceModal visible={joinSpaceVisible} onClose={() => setJoinSpaceVisible(false)}/> */}
            <NavMenu showCreateSpaceModal={() => setCreateSpaceVisible(true)} showJoinSpaceModal={() => setJoinSpaceVisible(true)}/>
            {(() => (typeof window !== 'undefined' && typeof window.process === 'object' && Object.keys(window.process).find(k => k == "type") !== undefined) ? <CloseButton /> : <></>)()}
            <Container>
                {props.children}
            </Container>
        </React.Fragment>
    );
};

export default Layout;
