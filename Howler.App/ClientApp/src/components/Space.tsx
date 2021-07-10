import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { AuthState } from '@aws-amplify/ui-components';
import { ApplicationState } from '../store';
import * as Spaces from '../store/Spaces';
import ChannelList from './ChannelList';
import Channel from './Channel';
import UserStatus from './UserStatus';

import './Space.scss';

type SpaceProps = {
  user: any,
  setAuthState: React.Dispatch<React.SetStateAction<AuthState | undefined>>,
} & typeof Spaces.actionCreators & Spaces.SpacesState & RouteComponentProps<{ spaceId: string, channelId: string }>;

class Space extends React.Component<SpaceProps, { loginToken: string, space: any, flashError: string | null, error: string | null, spaceId: string }> {
  
  constructor(props: SpaceProps) {
    super(props);

    this.state = {
      loginToken: "",
      space: this.props.spaces.find(s => s.spaceId === props.match.params.spaceId),
      flashError: null,
      error: null,
      spaceId: ""
    };
  }

  public render() {
    return <div className="space-container">
      <div className="space-container-channels">
        <ChannelList space={this.state.space} channelId={this.props.match.params.channelId} />
        <UserStatus setAuthState={this.props.setAuthState} user={{ userName: "Swearwolf", userIcon: "https://avatars.githubusercontent.com/u/7929478?v=4", status: "TODAY'S THE DAY", state: "online" }}/>
      </div>
      <Channel space={this.state.space} channelId={this.props.match.params.channelId} />
    </div>;
  }
};

export default connect(
  (state: ApplicationState) => state.spaces,
  Spaces.actionCreators
)(Space);
