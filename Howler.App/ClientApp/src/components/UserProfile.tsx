import { faReply, faShieldAlt, faUserEdit, faUserPlus } from '@fortawesome/free-solid-svg-icons';
import { useDropzone } from 'react-dropzone';
import * as React from 'react';
import Button from './Button';
import Input from './Input';
import TooltipButton from './TooltipButton';
import UserOnlineStateIndicator from './UserOnlineStateIndicator';
import './UserProfile.scss';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

const UserProfile : React.FunctionComponent<{user: any, editMode?: boolean, dismiss?: () => void, onEditModeClick?: () => void}> = props => {
    let [status, setStatus] = React.useState<string>(props.user.status);
    const onDrop = React.useCallback(acceptedFiles => {
        // Do something with the files
      }, [])
      const {getRootProps, getInputProps} = useDropzone({onDrop})

    return <div className="user-profile">
        <div className="user-profile-header">
            {(props.editMode ?
                <div className="user-profile-icon-editable" style={{ backgroundImage: `url(${props.user.userIcon})` }} {...getRootProps()} >
                    <input {...getInputProps()}/>
                </div> :
                <div className="user-profile-icon" style={{ backgroundImage: `url(${props.user.userIcon})` }}/>
            )}
            <div className="user-profile-text">
                <div className="user-profile-username">{props.user.userName}</div>
                <div className="user-profile-state">
                    <UserOnlineStateIndicator user={props.user}/>
                </div>
            </div>
            {(props.editMode ?
                <></> :
                <div className="user-profile-edit-button" onClick={props.onEditModeClick}>
                    <FontAwesomeIcon icon={faUserEdit}/>
                </div>)}
        </div>
        <div className="user-profile-content">
            {(props.editMode ?
                <>
                    <div className="user-profile-content-section-header">Status</div>
                    <div className="user-profile-info">
                        <Input placeholder="Status goes here" value={status} onChange={(e) => setStatus(e.target.value)}/>
                    </div>
                    <div className="user-profile-editor-actions">
                        <Button type="primary" disabled={status == props.user.status} onClick={() => {if (props.dismiss !== undefined) { props.dismiss(); }}}>Save Changes</Button>
                    </div>
                </> :
                <>
                    <div className="user-profile-content-section-header">Status</div>
                    <div className="user-profile-info">{props.user.status}</div>
                    <div className="user-profile-content-section-header">Roles</div>
                    <div className="user-profile-roles"></div>
                    <div className="user-profile-content-section-header"></div>
                    <div className="user-profile-actions">
                        <TooltipButton icon={faReply} text="Send Message" onClick={() => {}}/>
                        <TooltipButton icon={faUserPlus} text="Add Friend" onClick={() => {}}/>
                    </div>
                    <div className="user-profile-content-section-header"></div>
                    <div className="user-profile-actions">
                        <TooltipButton type="danger" icon={faShieldAlt} text="Block User" onClick={() => {}}/>
                    </div>
                </>)}
        </div>
    </div>;
};

export default UserProfile;