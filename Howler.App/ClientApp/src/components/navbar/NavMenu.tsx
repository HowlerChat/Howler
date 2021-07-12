import { AmplifySignOut } from '@aws-amplify/ui-react';
import * as React from 'react';
import { Link } from 'react-router-dom';
import SpaceButton from './SpaceButton';
import ExpandableNavMenu from './ExpandableNavMenu';
import './NavMenu.scss';

type NavMenuProps = {
    showCreateSpaceModal: () => void,
    showJoinSpaceModal: () => void,
};

export default class NavMenu extends React.PureComponent<NavMenuProps, { isOpen: boolean }> {
    public state = {
        isOpen: false
    };

    public render() {
        return (
            <header>
                <img src="/howler.png" className="logo"/>
                <div className="nav-menu-spaces">
                    <SpaceButton space={{ spaceName: "Shipyard!", spaceId: "3ec22786-bc0d-4adf-b1f7-69c65c00f162", iconUrl: "/shipyard.png" }} />
                    <SpaceButton space={{ spaceName: "Other thing", spaceId: "00000000-0000-0000-0000-000000000001", iconUrl: "/howler.png" }} />
                </div>
                <ExpandableNavMenu {...this.props}/>
            </header>
        );
    }
}
