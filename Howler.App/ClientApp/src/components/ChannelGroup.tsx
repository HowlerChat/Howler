import * as React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import {faChevronDown} from '@fortawesome/free-solid-svg-icons';
import './ChannelGroup.scss';
import { Link, useParams } from 'react-router-dom';

const ChannelGroup : React.FunctionComponent<{ group: { groupName: string, channels: { channelId: string, channelName: string, mentionCount?: number, mentions?: string }[] } }> = props => {
    let { spaceId, channelId } = useParams<{spaceId: string, channelId: string}>();
    return <div className="channel-group">
        <div className="channel-group-name">{props.group.groupName}</div>
        {props.group.channels.map(channel => <Link key={channel.channelName} to={`/spaces/${spaceId}/${channel.channelId}`}>
            <div className="channel-group-channel">
                <div className={"channel-group-channel-name" + (channel.channelId === channelId ? "-focused": "")}>{channel.channelName}
                    {(!!channel.mentionCount ?
                        <span className={"channel-group-channel-name-mentions-" + channel.mentions}>{channel.mentionCount}</span> :
                        <></>)}
                </div>
            </div>
        </Link>)}
    </div>;
}

export default ChannelGroup;