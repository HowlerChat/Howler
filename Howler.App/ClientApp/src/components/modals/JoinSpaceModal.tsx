import * as React from 'react';
import Modal from '../Modal';
import Input from '../Input';
import Button from '../Button';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faFileImage } from '@fortawesome/free-solid-svg-icons';
import SpaceIcon from '../SpaceIcon';
import { ApplicationState } from '../../store';
import * as Localization from '../../store/Localization';
import { connect } from 'react-redux';
import './JoinSpaceModal.scss';

type JoinSpaceModalProps = {
    visible: boolean,
    onClose: () => void
} & typeof Localization.actionCreators & Localization.LocalizationInfoState;

const JoinSpaceModal : React.FunctionComponent<JoinSpaceModalProps> = props => {
    let [space, setSpace] = React.useState<{ iconUrl: string, name: string } | undefined>(undefined);

    return <Modal visible={props.visible} onClose={props.onClose} title={props.localizations["JOIN_SPACE_TITLE"]([])}>
        <div className="modal-join-space">
            <div><Input onChange={(e) => e.target.value == "" ? setSpace(undefined) : setSpace({ iconUrl: "/shipyard.png", name: "Shipyard!" })} placeholder={props.localizations["JOIN_SPACE_PROMPT"]([])}/></div>
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
                <span className="modal-join-space-info">{props.localizations["SPACE_JOIN_AGREEMENT_LEFT"]([])}<a href="#">{props.localizations["COMMUNITY_GUIDELINES"]([])}</a></span>
                <Button type="primary" disabled={!space} onClick={()=>{}}>{props.localizations["JOIN_SPACE"]([])}</Button>
            </div>
        </div>
    </Modal>;
}

export default connect(
    (state: ApplicationState) => state.localizations,
    Localization.actionCreators
)(JoinSpaceModal);