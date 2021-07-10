import * as React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import {faChevronDown} from '@fortawesome/free-solid-svg-icons';
import * as moment from 'moment-timezone';
import { RouteComponentProps, withRouter } from 'react-router';
import './Channel.scss';

type ChannelProps = { space: any, channelId: string } & RouteComponentProps<{ spaceId: string, channelId: string }>;

class Channel extends React.PureComponent<ChannelProps, { pendingMessage: string, messages: { timestamp: string, id: string, content: { senderId: string, type: "post"|"event", text: string | string[] }|{ senderId: string, type: "embed", videoUrl: string, width?: string, height?: string}}[]}> {
    mapSenderToUser(senderId: string) {
        return {
            "@<00000000-0000-0000-0000-000000000000>": {
                "displayName": "Swearwolf",
                "userTag": "Swearwolf#6463",
                "userIcon": "https://avatars.githubusercontent.com/u/7929478?v=4"
            },
            "@<00000000-0000-0000-0000-000000000001>": {
                "displayName": "Bananaman",
                "userTag": "Bongocat#3211",
                "userIcon": "https://avatars.githubusercontent.com/u/1?v=4"
            },
            "@<00000000-0000-0000-0000-000000000002>": {
                "displayName": "Test User",
                "userTag": "Test#0000",
                "userIcon": "https://avatars.githubusercontent.com/u/2?v=4"
            }
        }[senderId] || {
            "displayName": "Unknown User",
            "userTag": "Unknown#0000",
            "userIcon": "https://avatars.githubusercontent.com/u/3?v=4"
        };
    }

    submitMessage() {
        this.setState(prevState => { return {...prevState, messages: prevState.messages.concat([{
            "timestamp": "1622272285000",
            "id": Math.random().toString(16).substring(2, 8), // just need a random value for now
            "content": {
                "type": "post",
                "senderId": "@<00000000-0000-0000-0000-000000000000>",
                "text": prevState.pendingMessage
            }
        }]), pendingMessage: ""} });
    }

    constructor(props: ChannelProps) {
        super(props);

        this.submitMessage = this.submitMessage.bind(this);

        this.state = {
            pendingMessage: "",
            // This is a no-good, very bad approach to message management, and only for prototyping
            messages: [
// 00000000-0000-0000-0000-000000000000
/*
POST /spaces/{spaceId}/channels/{channelId}/messages
{
    "senderId": "@<00000000-0000-0000-0000-0000000000000>",
    "algorithm": "sha3-256",
    "id": "F0E4C2F76C58916EC258F246851BEA091D14D4247A2FC3E18694461B1816E13B",
    "nonce": "new Date().getLongMillis()",
    "type": "embed" | "post" | "event" | "share",
    "content": {
        "videoUrl": "One thing that defines me is I",
        "width": "640",
        "height": "480"
    },
    // only supplied by server:
    "timestamp": "45678",
    "lastModifiedTimestamp": "3456789"|undefined
    "lastModifiedHash": "d3456789076"
    "reactions": [
        {
            "emojiId": "678"
            "spaceId": "03902923-0000...",
            "emojiName": ":howler:",
            "count": 3,
            "userIds": [
                "@<00000000-0000-0000-0000-0000000000000>",
                "@<00000000-0000-0000-0000-0000000000001>",
                "@<00000000-0000-0000-0000-0000000000002>"
            ]
        }
    ],
    "mentions": {
        "users": ["@<00000000-0000-0000-0000-0000000000000>"],
        "roles": ["@<00000000-0000-0000-0000-0000000000000>"],
        "channels": ["@<00000000-0000-0000-0000-0000000000000>"]
    },
    "messageReplyId": "F0E4C2F76C58916EC258F246851BEA091D14D4247A2FC3E18694461B1816E13B"
}
PATCH /spaces/{spaceId}/channels/{channelId}/messages/{messageId}
{
    "type": "post",
    "content": {
        "text": "Oops"
    }
    "nonce": "some nonce value"
    "lastModifiedHash": ""
}
{
    "senderId": "@<00000000-0000-0000-0000-0000000000000>",
    "algorithm": "sha3-256",
    "id": "A0E4C2F76C58916EC258F246851BEA091D14D4247A2FC3E18694461B1816E13B",
    "referenceId": "F0E4C2F76C58916EC258F246851BEA091D14D4247A2FC3E18694461B1816E13B"
    "type": "post",
    "content": {
        "text": "really really don't"
    }
}
{
    "senderId": "@<00000000-0000-0000-0000-0000000000000>",
    "algorithm": "sha3-256",
    "id": "D0E4C2F76C58916EC258F246851BEA091D14D4247A2FC3E18694461B1816E13B",
    "referenceId": "A0E4C2F76C58916EC258F246851BEA091D14D4247A2FC3E18694461B1816E13B"
    "type": "post",
    "content": {
        "text": "kick puppies"
    }
}

POST /spaces/{spaceId}/channels/{channelId}/messages/{messageId}/attachments

*/
                {
                    "timestamp": "1622315285000",
                    "id": "F0E4C2F76C58916EC258F246851BEA091D14D4247A2FC3E18694461B1816E13B",
                    "content": {
                        "senderId": "@<00000000-0000-0000-0000-000000000000>",
                        "type": "post",
                        "text": ["I think I finally got message glomming working", "Here's the test case", "Well I'll be damned."]
                    }
                },
                {
                    "timestamp": "1622315295000",
                    "id": "A0E4C2F76C58916EC258F246851BEA091D14D4247A2FC3E18694461B1816E13A",
                    "content": {
                        "senderId": "@<00000000-0000-0000-0000-000000000002>",
                        "type": "post",
                        "text": "Awesome, ok so let's alternate between users"
                    }
                },
                {
                    "timestamp": "1622315296000",
                    "id": "F0E4C2F76C58916EC258F246851BEA091D14D4247A2FC3E18694461B1816E13C",
                    "content": {
                        "senderId": "@<00000000-0000-0000-0000-000000000000>",
                        "type": "post",
                        "text": "Shouldn't be contingent on other users, should be separated..."
                    }
                },
                {
                    "timestamp": "1622315396000",
                    "id": "F0E4C2F76C58916EC258F246851BEA091D14D4247A2FC3E18694461B1816E13D",
                    "content": {
                        "senderId": "@<00000000-0000-0000-0000-000000000000>",
                        "type": "post",
                        "text": "...by timestamps of sufficient distance"
                    }
                },
                // {
                //     "timestamp": "1622272885000",
                //     "id": "B0E4C2F76C58916EC258F246851BEA091D14D4247A2FC3E18694461B1816E13B",
                //     "content": {
                //         "senderId": "@<00000000-0000-0000-0000-000000000002>",
                //         "type": "post",
                //         "text": "Here is yet another"
                //     }
                // },
                // {
                //     "timestamp": "1622272888000",
                //     "id": "C0E4C2F76C58916EC258F246851BEA091D14D4247A2FC3E18694461B1816E13B",
                //     "content": {
                //         "senderId": "@<00000000-0000-0000-0000-000000000002>",
                //         "type": "post",
                //         "text": "In theory this will get grouped with glomming"
                //     }
                // },
                // {
                //     "timestamp": "1622272999000",
                //     "id": "A345678909865678",
                //     "content": {
                //         "senderId": "@<00000000-0000-0000-0000-000000000000>",
                //         "type": "embed",
                //         "videoUrl": "https://www.youtube.com/embed/dQw4w9WgXcQ"
                //     }
                // }
            ]
        }
    }

    public render() {
        return <div className="channel">
            <div className="channel-name">#general-en</div>
            <div className="message-list">
                {this.state.messages.map(message => {
                    let sender = this.mapSenderToUser(message.content.senderId);
                    let time = moment.tz(parseInt(message.timestamp, 10), "America/Los_Angeles");

                    // todo: message glomming
                    return <div key={message.id} className="message">
                        <div className="message-sender-icon" style={{ backgroundImage: `url(${sender.userIcon})` }}/>
                        <div className="message-content">
                            <span className="message-sender-name">{sender.displayName}</span><span className="message-timestamp">{time.format('h:mma')}</span>
                            {(() => {
                                if (message.content.type == "post") {
                                    if (Array.isArray(message.content.text)) {
                                        let content = (message.content.text as string[]);
                                        return content.map((c, i) =>
                                            <div key={i} className="message-post-content">{c}</div>
                                        );
                                    } else {
                                        let content = (message.content as {text: string});
                                        return <div className="message-post-content">{content.text}</div>;
                                    }
                                } else if (message.content.type == "embed") {
                                    let content = (message.content as {videoUrl: string, width?: string, height?: string});
                                    return <div className="message-post-content">
                                        <iframe
                                            width={content.width || "560"}
                                            height={content.height || "315"}
                                            src={content.videoUrl} 
                                            frameBorder="0"
                                            allow="autoplay; encrypted-media"></iframe>
                                    </div>;
                                }
                            })()}
                            
                        </div>
                    </div>
                })}
            </div>
            <textarea
                className="message-editor"
                placeholder={"Send a message to #general-en"}
                rows={1}
                value={this.state.pendingMessage}
                onChange={(e) => this.setState({pendingMessage: e.target.value})}
                onKeyDown={(e) => {
                    if (e.key === 'Enter') {
                        this.submitMessage();
                        e.preventDefault();
                    }
                }}/>
        </div>;
    }
}

export default withRouter(Channel);