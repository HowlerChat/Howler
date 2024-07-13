import * as React from 'react';
import Modal from '../Modal';
import Input from '../Input';
import Button from '../Button';
import SpaceIcon from '../SpaceIcon';
import { ApplicationState } from '../../store';
import * as Localization from '../../store/Localization';
import * as Servers from '../../store/Servers';
import { connect } from 'react-redux';
import './JoinSpaceModal.scss';
import { useNavigate } from 'react-router';

type JoinSpaceModalProps = {
    visible: boolean,
    onClose: () => void
} & typeof Localization.actionCreators & Localization.LocalizationInfoState & Servers.ServersState;

const JoinSpaceModal : React.FunctionComponent<JoinSpaceModalProps> = props => {
    let [space, setSpace] = React.useState<{ iconUrl: string, name: string, spaceId: string } | undefined>(undefined);
    let connection = props.connection as WebSocket;
    let navigate = useNavigate();

    const lookupSpace = (url: string) => {
        if (url == "") {
            setSpace(undefined);
        } else {
            // connection.on("GetSpaceByUrlResponse", foundSpace)
            // connection.send("GetSpaceByUrl", url);
        }
    };

    const foundSpace = (response: any) => {
        setSpace({ iconUrl: response.iconUrl, name: response.spaceName, spaceId: response.spaceId });
        // connection.off("GetSpaceByUrlResponse");
    }

    return <Modal visible={props.visible} onClose={props.onClose} title={props.localizations["JOIN_SPACE_TITLE"]([])}>
        <div className="modal-join-space">
            <div><Input onChange={(e) => lookupSpace(e.target.value)} placeholder={props.localizations["JOIN_SPACE_PROMPT"]([])}/></div>
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
                <Button type="primary" disabled={!space} onClick={()=>{ if (!!space) { /*connection.send("JoinSpace", space.spaceId);*/ navigate("/"); } }}>{props.localizations["JOIN_SPACE"]([])}</Button>
            </div>
        </div>
    </Modal>;
}

export default connect(
    (state: ApplicationState) => { console.log(state); return {...state.localizations, ...state.servers} as Localization.LocalizationInfoState & Servers.ServersState; },
    Localization.actionCreators
)(JoinSpaceModal);
