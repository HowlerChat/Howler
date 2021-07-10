import * as React from 'react';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as signalR from '@microsoft/signalr';
import * as Connection from '../store/Connection';
import { useHistory } from 'react-router';

import './Connecting.scss';

const Connecting : React.FunctionComponent<typeof Connection.actionCreators & { connections: Connection.ConnectionInfoState | undefined }> = props => {
    let connection = (props.connections || { connection: { state: null } }).connection;
    let history = useHistory();

    React.useEffect(() => {
        if (props.connections != null && props.connections.connection != null && props.connections.connection.state === signalR.HubConnectionState.Connected && Object.keys(props.connections.userSpaces).length == 0)
        {
            props.connections.connection.on("GetUserSpacesResponse", props.updateUserSpaces);
            props.connections.connection.on("NoUserSpacesFound", () => history.push("/spaces/join"));
            props.connections.connection.send("GetUserSpaces");
        }
    }, [connection]);
    
    return <div className="connecting-splash">
        <div className="connecting-icon" style={{backgroundImage: "url('/howler.png')"}}></div><br/>
        <div className="connecting-message">{(connection || {state: "Connecting"}).state}</div>
    </div>
}

export default connect(
    (state: ApplicationState) => { return { connections: state.connections }; },
    Connection.actionCreators
)(Connecting);