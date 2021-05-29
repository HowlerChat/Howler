import * as React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import {faChevronDown} from '@fortawesome/free-solid-svg-icons';
import ChannelGroup from './ChannelGroup';
import './ChannelList.scss';
import { isFunction } from 'util';

export default class ChannelList extends React.PureComponent<{}, { groups: { groupName: string, channels: { channelName: string, isFocused?: boolean, mentionCount?: number, mentions?: string }[] }[] }> {
    constructor(props: { }) {
        super(props);

        this.state = {
            groups: [
                {
                    groupName: "General",
                    channels: [
                        {
                            channelName: "#build-logs",
                            mentionCount: 20,
                            mentions: "role"
                        },
                        {
                            channelName: "#build-breakers"
                        },
                        {
                            channelName: "#yell-at-us",
                            mentionCount: 1,
                            mentions: "you"
                        },
                        {
                            channelName: "#general-en",
                            isFocused: true
                        },
                        {
                            channelName: "#general-es"
                        },
                        {
                            channelName: "#complete-nonsense"
                        },
                    ]
                },
                {
                    groupName: "Botmasters",
                    channels: [
                        {
                            channelName: "#terminator-thunderdome"
                        },
                        {
                            channelName: "#api-development"
                        },
                    ]
                },
                {
                    groupName: "Townhall",
                    channels: [
                        {
                            channelName: "#may-08-townhall"
                        },
                        {
                            channelName: "#may-01-townhall"
                        },
                    ]
                },
            ]
        };
    }
    public render() {
        return <div className="channels-list">
            <div className="space-header">
                <span className="space-header-name">Shipyard!</span>
                <span className="space-context-menu-toggle-button"><FontAwesomeIcon icon={faChevronDown}/></span>
            </div>
            {this.state.groups.map(group => <ChannelGroup key={group.groupName} group={group}/>)}
        </div>;
    }
}