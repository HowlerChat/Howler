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
                    <SpaceButton space={{ spaceName: "Farcaster", spaceId: "00000000-0000-0000-0000-000000000000", iconUrl: "https://www.farcaster.xyz/img/logo_128.png" }} />
                </div>
                {/* <ExpandableNavMenu {...this.props}/> */}
            </header>
        );
    }
}
