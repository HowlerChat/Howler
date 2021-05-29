import * as React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import {faChevronDown} from '@fortawesome/free-solid-svg-icons';
import './ChannelGroup.scss';

export default class ChannelGroup extends React.PureComponent<{ group: { groupName: string, channels: { channelName: string, isFocused?: boolean, mentionCount?: number, mentions?: string }[] } }, {}> {
    public render() {
        return <div className="channel-group">
            <div className="channel-group-name">{this.props.group.groupName}</div>
            {this.props.group.channels.map(channel => <div key={channel.channelName} className="channel-group-channel">
                <div className={"channel-group-channel-name" + (channel.isFocused ? "-focused": "")}>{channel.channelName}
                    {(!!channel.mentionCount ?
                        <span className={"channel-group-channel-name-mentions-" + channel.mentions}>{channel.mentionCount}</span> :
                        <></>)}
                </div>
            </div>)}
        </div>;
    }
}