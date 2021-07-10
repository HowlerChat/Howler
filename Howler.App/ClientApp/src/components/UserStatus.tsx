import * as React from 'react';
import { connect } from 'react-redux';
import { faEdit, faSignOutAlt } from '@fortawesome/free-solid-svg-icons';
import { AuthState } from '@aws-amplify/ui-components';
import { Auth } from 'aws-amplify';
import Tooltip from './Tooltip';
import TooltipButton from './TooltipButton';
import TooltipDivider from './TooltipDivider';
import UserProfile from './UserProfile';
import { ApplicationState } from '../store';
import * as Localization from '../store/Localization';

import './UserStatus.scss';

type UserStatusProps = {
    user: any,
    setAuthState: React.Dispatch<React.SetStateAction<AuthState | undefined>>,
} & typeof Localization.actionCreators & Localization.LocalizationInfoState;

const UserStatus : React.FunctionComponent<UserStatusProps> = props => {
    let [isMenuExpanded, setIsMenuExpanded] = React.useState<boolean>(false);
    let [isProfileEditorOpen, setIsProfileEditorOpen] = React.useState<boolean>(false);

    return <>
        {(isProfileEditorOpen ?
            <>
                <div className="invisible-dismissal invisible-dark">
                    <UserProfile editMode={true} user={props.user} dismiss={() => setIsProfileEditorOpen(false)} />
                    <div className="invisible-dismissal" onClick={() => setIsProfileEditorOpen(false)}/>
                </div>
            </> :
            <></>)}
        {(isMenuExpanded ? 
            <>
                <div className="invisible-dismissal" onClick={() => setIsMenuExpanded(false)}/>
                <Tooltip className="user-status-menu" arrow="none" visible={isMenuExpanded}>
                    <TooltipButton text="Edit Profile" icon={faEdit} onClick={() => { setIsMenuExpanded(false); setIsProfileEditorOpen(true); }}/>
                    <TooltipDivider/>
                    <TooltipButton type="danger" text="Log Out" icon={faSignOutAlt} onClick={async () => await signOut(props)}/>
                </Tooltip>
            </> :
            <></>)}
        <div onClick={() => setIsMenuExpanded(true)} className="user-status">
            <div className="user-status-icon" style={{ backgroundImage: `url(${props.user.userIcon})` }}/>
            <div className="user-status-text">
                <div className="user-status-username">{props.user.userName}</div>
                <div className="user-status-info">{props.user.status}</div>
            </div>
        </div>
    </>;
}


async function signOut(userProps: UserStatusProps) {
    try {
        await Auth.signOut().then(() => {
            // Welcome to an incredibly stupid hack, courtesy of this genius decision:
            // https://github.com/aws-amplify/amplify-js/issues/1269#issuecomment-479963543
            userProps.setAuthState(AuthState.SignedOut);
        });
    } catch (error) {
        console.log('error signing out: ', error);
    }
}

export default connect(
    (state: ApplicationState) => state.localizations,
    Localization.actionCreators
)(UserStatus);