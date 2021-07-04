import { AmplifySignOut } from '@aws-amplify/ui-react';
import { Auth } from 'aws-amplify';
import { AuthState } from '@aws-amplify/ui-components';
import * as React from 'react';
import { Link } from 'react-router-dom';
import SpaceButton from './SpaceButton';
import ExpandableNavMenu from './ExpandableNavMenu';
import './NavMenu.scss';

export default class NavMenu extends React.PureComponent<{ setAuthState: React.Dispatch<React.SetStateAction<AuthState | undefined>>, showCreateSpaceModal: () => void}, { isOpen: boolean }> {
    public state = {
        isOpen: false
    };

    //<div className="sign-out" onClick={async () => await this.signOut()}><FontAwesomeIcon icon={faSignOutAlt}/></div>
    public render() {
        return (
            <header>
                <img src="/howler.png" className="logo"/>
                <SpaceButton space={{ spaceName: "Shipyard!", spaceId: "00000000-0000-0000-0000-000000000000", iconUrl: "/shipyard.png" }} />
                <SpaceButton space={{ spaceName: "Other thing", spaceId: "00000000-0000-0000-0000-000000000001", iconUrl: "/howler.png" }} />
                <ExpandableNavMenu showCreateSpaceModal={this.props.showCreateSpaceModal}/>
            </header>
        );
    }

    private toggle = () => {
        this.setState({
            isOpen: !this.state.isOpen
        });
    }

    private async signOut() {
        try {
            await Auth.signOut().then(() => {
                // Welcome to an incredibly stupid hack, courtesy of this genius decision:
                // https://github.com/aws-amplify/amplify-js/issues/1269#issuecomment-479963543
                this.props.setAuthState(AuthState.SignedOut);
            });
        } catch (error) {
            console.log('error signing out: ', error);
        }
    }
}
