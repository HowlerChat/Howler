import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { AuthState } from '@aws-amplify/ui-components';
import { ApplicationState } from '../store';
import * as Farcaster from '../store/Farcaster';
import ChannelList from './ChannelList';
import Channel from './Channel';
import UserStatus from './UserStatus';

import './Space.scss';
import { Cast, MerkleAPIClient } from '@standard-crypto/farcaster-js';

type SpaceProps = {
  user: any,
} & typeof Farcaster.actionCreators & Farcaster.FarcasterState;

const Space : React.FunctionComponent<SpaceProps> = props => {
  return <div className="space-container">
    <div className="space-container-channels">
      <ChannelList channelId={"3ec22786-bc0d-4adf-b1f7-69c65c00f162"} />
      <UserStatus user={props.user}/>
    </div>
    <Channel casts={props.casts} channelId={"3ec22786-bc0d-4adf-b1f7-69c65c00f162"} />
  </div>;
};

export default connect(
  (state: ApplicationState) => state.farcaster,
  Farcaster.actionCreators
)(Space);
