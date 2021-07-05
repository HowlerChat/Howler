import * as React from 'react';
import Modal from '../Modal';
import Input from '../Input';
import Button from '../Button';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faFileImage } from '@fortawesome/free-solid-svg-icons';
import SpaceIcon from '../SpaceIcon';
import './JoinSpaceModal.scss';

type JoinSpaceModalProps = {
    visible: boolean,
    onClose: () => void
};

const JoinSpaceModal : React.FunctionComponent<JoinSpaceModalProps> = props => {
    let [space, setSpace] = React.useState<{ iconUrl: string, name: string } | undefined>(undefined);

    return <Modal visible={props.visible} onClose={props.onClose} title="Join a Space">
        <div className="modal-join-space">
            <div><Input onChange={(e) => e.target.value == "" ? setSpace(undefined) : setSpace({ iconUrl: "/shipyard.png", name: "Shipyard!" })} placeholder="Enter the vanity URL or invite URL to join"/></div>
            <div className="modal-join-space-icon">
                {(!space ?
                    <SpaceIcon size="large" selected={false} iconUrl="/howler.png" /> :
                    <>
                        <SpaceIcon size="large" selected={true} iconUrl={space.iconUrl}/>
                        <div className="howler-modal-title">{space.name}</div>
                    </>
                )}
            </div>
            <div className="modal-join-space-actions">
                <span className="modal-join-space-info">By joining a hosted Space you agree to Howler's <a href="#">Community Guidelines</a></span>
                <Button type="primary" disabled={!space} onClick={()=>{}}>Join Space</Button>
            </div>
        </div>
    </Modal>;
}

export default JoinSpaceModal;