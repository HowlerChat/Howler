import * as React from 'react';
import Modal from '../Modal';
import Input from '../Input';
import Button from '../Button';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faFileImage } from '@fortawesome/free-solid-svg-icons';
import './CreateSpaceModal.scss';
import { getLocalization } from '../../api/howlerApi';
import { ApplicationState } from '../../store';
import * as Localization from '../../store/Localization';
import { connect } from 'react-redux';

type CreateSpaceModalProps = {
    visible: boolean,
    onClose: () => void
} & typeof Localization.actionCreators & Localization.LocalizationInfoState;

const CreateSpaceModal : React.FunctionComponent<CreateSpaceModalProps> = props => {
    return <Modal visible={props.visible} onClose={props.onClose} title={props.localizations["CREATE_SPACE_TITLE"]([])}>
        <div className="modal-create-space-left">
            <Input placeholder={props.localizations["CREATE_SPACE_PROMPT"]([])}/>
            <div className="attachment-drop">
                <span className="attachment-drop-icon">
                    <FontAwesomeIcon icon={faFileImage}/>
                </span>
                <div>{props.localizations["SPACE_BANNER_ATTACHMENT"]([])}</div>
            </div>
            <div>{props.localizations["SPACE_CREATE_AGREEMENT_LEFT"]([])}<a href="#">{props.localizations["COMMUNITY_GUIDELINES"]([])}</a>{props.localizations["SPACE_CREATE_AGREEMENT_RIGHT"]([])}</div>
        </div>
        <div className="modal-create-space-right">
            <div className="attachment-drop">
                <span className="attachment-drop-icon">
                    <FontAwesomeIcon icon={faFileImage}/>
                </span>
                <div>{props.localizations["SPACE_ICON_ATTACHMENT"]([])}</div>
            </div>
            <Button type="primary" onClick={()=>{}}>{props.localizations["CREATE_SPACE"]([])}</Button>
            <Button type="secondary" onClick={()=>{}}>{props.localizations["ADVANCED_SETTINGS"]([])}</Button>
        </div>
    </Modal>;
}

export default connect(
    (state: ApplicationState) => state.localizations,
    Localization.actionCreators
)(CreateSpaceModal);