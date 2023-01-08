import { faTimes } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React from 'react';
import './Modal.scss';

type ModalProps = {
    title: string,
    visible: boolean,
    onClose: () => void
}

const Modal : React.FunctionComponent<ModalProps> = props => {
    return props.visible ? <div className="invisible-dismissal invisible-dark">
        <div className="howler-modal">
            <div className="howler-modal-close" onClick={() => props.onClose()}><FontAwesomeIcon icon={faTimes}/></div>
            <div className="howler-modal-title">{props.title}</div>
            <div className="howler-modal-container">
                {props.children}
            </div>
        </div>
    </div> : <></>;
}

export default Modal;