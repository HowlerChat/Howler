import * as React from 'react';
import { AuthState } from '@aws-amplify/ui-components';
import NavMenu from './navbar/NavMenu';
import CloseButton from './CloseButton';
import Container from './Container';
import CreateSpaceModal from './modals/CreateSpaceModal';

const Layout : React.FunctionComponent<{ setAuthState: React.Dispatch<React.SetStateAction<AuthState | undefined>> }> = props => {
    let [createSpaceVisible, setCreateSpaceVisible] = React.useState(false);

    return (
        <React.Fragment>
            <CreateSpaceModal visible={createSpaceVisible} onClose={() => setCreateSpaceVisible(false)}/>
            <NavMenu setAuthState={props.setAuthState} showCreateSpaceModal={() => setCreateSpaceVisible(true)}/>
            <CloseButton />
            <Container>
                {props.children}
            </Container>
        </React.Fragment>
    );
};

export default Layout;