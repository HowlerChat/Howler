import * as React from 'react';
import { connect } from 'react-redux';
import { useParams } from 'react-router';
import { ApplicationState } from '../store';
import * as Spaces from '../store/Spaces';
import ChannelList from './ChannelList';
import Channel from './Channel';
import UserStatus from './UserStatus';

import './Space.scss';

type SpaceProps = {
  user: any,
  setAuthState: React.Dispatch<React.SetStateAction<string | undefined>>,
} & typeof Spaces.actionCreators & Spaces.SpacesState;

const Space : React.FunctionComponent<SpaceProps> = props => {
  let params = useParams<{serverId: string, spaceId: string, channelId: string}>();

  React.useEffect(() => {
    if (props.spaces.length == 0 && params.spaceId) {
      props.requestSpace('params.serverId', params.spaceId);
    }
  }, []);

  if (!props || !props.spaces || !params.spaceId || !params.channelId) {
    return <></>;
  };

  let space = props.spaces.find(s => s.spaceId === params.spaceId) || props.spaces[0];
  return <div className="space-container">
    <div className="space-container-channels">
      <ChannelList space={space} channelId={params.channelId} />
      <UserStatus setAuthState={props.setAuthState} user={props.user}/>
    </div>
    <Channel spaceId={params.spaceId} channelId={params.channelId} />
  </div>;
};

export default connect(
  (state: ApplicationState) => {
    return state.spaces;
  },
  Spaces.actionCreators
)(Space);
