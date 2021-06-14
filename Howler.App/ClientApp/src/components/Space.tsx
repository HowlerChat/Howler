import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import * as signalR from '@microsoft/signalr';
import { ApplicationState } from '../store';
import * as Spaces from '../store/Spaces';
import ChannelList from './ChannelList';
import Channel from './Channel';

import './Space.scss';

type SpaceProps = {user: any} & typeof Spaces.actionCreators & Spaces.SpacesState & RouteComponentProps<{ spaceId: string, channelId: string }>;

class Space extends React.Component<SpaceProps, {connection: signalR.HubConnection | null, loginToken: string, space: any, flashError: string | null, error: string | null, spaceId: string }> {
  
  constructor(props: SpaceProps) {
    super(props);
    // this.getSpaceResponse = this.getSpaceResponse.bind(this);
    // this.noSpaceFound = this.noSpaceFound.bind(this);

    this.state = {
      connection: null,
      loginToken: "",
      space: this.props.spaces.find(s => s.spaceId === props.match.params.spaceId),
      flashError: null,
      error: null,
      spaceId: ""
    };
  }

  public componentDidMount() {
    if (!!this.props.user && (this.state.connection == null || this.state.connection.state == signalR.HubConnectionState.Disconnected)) {
      this.connect(this.props.user.signInUserSession.accessToken.jwtToken);
    }
  }

  public async connect(token: string) {
    // This is a really bad example for illustration only, we won't
    // be doing signalR like this in the actual client.
    let connection = new signalR.HubConnectionBuilder()
      .withUrl("http://localhost:5000/howler", { accessTokenFactory: () => token }).build();
    try {
      await connection.start();
      // connection.on("GetSpaceResponse", this.getSpaceResponse);
      // connection.on("NoSpaceFound", this.noSpaceFound);

      this.setState({connection, error: null});
    } catch (e) {
      let error = e as signalR.HttpError;
      this.setState({error: error.message});
    }
  }

  public render() {
    return <div className="space-container">
      <ChannelList space={this.state.space} channelId={this.props.match.params.channelId} />
      <Channel space={this.state.space} channelId={this.props.match.params.channelId} />
    </div>;
  }
  /*old:
  {(this.state.error != null ? <div>Error: {this.state.error}</div> : <></>)}
            <div>State: {(this.state.connection || { state: signalR.HubConnectionState.Disconnected}).state}</div>
            {(() => {
            if (this.state.connection != null && this.state.connection.state == signalR.HubConnectionState.Connected) {
                return <><div>
                {(!!this.state.flashError ? <div>{this.state.flashError}</div> : <></>)}
                <label>SpaceId:</label>
                <input type="text" value={this.state.spaceId} onChange={(e) => this.setState({spaceId: e.target.value})}></input>
                <button onClick={() => this.getSpace()}>Send</button>
                </div>
                <div><b>{JSON.stringify(this.props.spaces)}</b></div></>;
            } else {
                return <div>
                <a href="https://howler.auth.us-west-2.amazoncognito.com/login?client_id=6b75ooll3b86ugauhu22vj39ra&response_type=token&scope=email+openid+profile&redirect_uri=http://localhost:8000">Sign in here</a>
                <label>Token:</label>
                <input type="text" value={this.state.loginToken} onChange={(e) => this.setState({loginToken: e.target.value})}></input>
                <button onClick={async () => await this.connect("")}>Connect</button>
                </div>;
            }
            })()}
            */
};

export default connect(
  (state: ApplicationState) => state.spaces,
  Spaces.actionCreators
)(Space);
