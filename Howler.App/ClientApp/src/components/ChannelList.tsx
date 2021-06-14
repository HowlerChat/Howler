import * as React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import {faChevronDown} from '@fortawesome/free-solid-svg-icons';
import ChannelGroup from './ChannelGroup';
import './ChannelList.scss';
import { isFunction } from 'util';

type ChannelListProps = { space: any, channelId: string };

export default class ChannelList extends React.PureComponent<ChannelListProps, { groups: { groupName: string, channels: { channelId: string, channelName: string, mentionCount?: number, mentions?: string }[] }[] }> {
    constructor(props: ChannelListProps) {
        super(props);

        this.state = {
            groups: [
                {
                    groupName: "General",
                    channels: [
                        {
                            channelId: "00000000-0000-0000-0000-000000000000",
                            channelName: "#build-logs",
                            mentionCount: 20,
                            mentions: "role"
                        },
                        {
                            channelId: "00000000-0000-0000-0000-000000000001",
                            channelName: "#build-breakers"
                        },
                        {
                            channelId: "00000000-0000-0000-0000-000000000002",
                            channelName: "#yell-at-us",
                            mentionCount: 1,
                            mentions: "you"
                        },
                        {
                            channelId: "00000000-0000-0000-0000-000000000003",
                            channelName: "#general-en"
                        },
                        {
                            channelId: "00000000-0000-0000-0000-000000000004",
                            channelName: "#general-es"
                        },
                        {
                            channelId: "00000000-0000-0000-0000-000000000005",
                            channelName: "#complete-nonsense"
                        },
                    ]
                },
                {
                    groupName: "Botmasters",
                    channels: [
                        {
                            channelId: "00000000-0000-0000-0000-000000000006",
                            channelName: "#terminator-thunderdome"
                        },
                        {
                            channelId: "00000000-0000-0000-0000-000000000007",
                            channelName: "#api-development"
                        },
                    ]
                },
                {
                    groupName: "Townhall",
                    channels: [
                        {
                            channelId: "00000000-0000-0000-0000-000000000008",
                            channelName: "#may-08-townhall"
                        },
                        {
                            channelId: "00000000-0000-0000-0000-000000000009",
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
            {this.state.groups.map(group => <ChannelGroup key={group.groupName} selectedChannelId={this.props.channelId} group={group}/>)}
        </div>;
    }
}