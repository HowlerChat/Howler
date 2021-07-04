import * as React from 'react';
import Modal from '../Modal';
import Input from '../Input';
import Button from '../Button';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faFileImage } from '@fortawesome/free-solid-svg-icons';
import './CreateSpaceModal.scss';

type CreateSpaceModalProps = {
    visible: boolean,
    onClose: () => void
};

const CreateSpaceModal : React.FunctionComponent<CreateSpaceModalProps> = props => {
    return <Modal visible={props.visible} onClose={props.onClose} title="Create a Space">
        <div className="modal-create-space-left">
            <Input placeholder="Choose a name for your Space"/>
            <div className="attachment-drop">
                <span className="attachment-drop-icon">
                    <FontAwesomeIcon icon={faFileImage}/>
                </span>
                <div>Drag and drop or click to add a Space banner</div>
            </div>
            <div>By creating a hosted Space you agree to Howler's <a href="#">Community Guidelines</a></div>
        </div>
        <div className="modal-create-space-right">
            <div className="attachment-drop">
                <span className="attachment-drop-icon">
                    <FontAwesomeIcon icon={faFileImage}/>
                </span>
                <div>Drag and drop or click to add a Space icon</div>
            </div>
            <Button type="primary" onClick={()=>{}}>Create Account</Button>
            <Button type="secondary" onClick={()=>{}}>Advanced Settings</Button>
        </div>
    </Modal>;
}

export default CreateSpaceModal;