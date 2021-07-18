import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps, useParams } from 'react-router';
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
} & typeof Spaces.actionCreators & Spaces.SpacesState;

const Space : React.FunctionComponent<SpaceProps> = props => {
  let params = useParams<{spaceId: string, channelId: string}>();

  if (props.spaces.length == 0) {
    props.requestSpace(params.spaceId);
  }

  let space = props.spaces.find(s => s.spaceId === params.spaceId) || props.spaces[0];
  console.log(space);
  return <div className="space-container">
    <div className="space-container-channels">
      <ChannelList space={space} channelId={params.channelId} />
      <UserStatus setAuthState={props.setAuthState} user={props.user}/>
    </div>
    <Channel space={space} channelId={params.channelId} />
  </div>;
};

export default connect(
  (state: ApplicationState) => state.spaces,
  Spaces.actionCreators
)(Space);
