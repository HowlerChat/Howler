import * as React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import {faChevronDown} from '@fortawesome/free-solid-svg-icons';
import * as moment from 'moment-timezone';
import { connect } from 'react-redux';
import { RouteComponentProps, withRouter } from 'react-router';
import * as Farcaster from '../store/Farcaster';
import './Channel.scss';
import { Cast } from '@standard-crypto/farcaster-js';
import { ApplicationState } from '../store';

type ChannelProps = { casts: Cast[], channelId: string } & Farcaster.FarcasterState & typeof Farcaster.actionCreators;

const Channel: React.FunctionComponent<ChannelProps> = (props) => {
    let [pendingMessage, setPendingMessage] = React.useState<string>();
    let ref = React.createRef<HTMLDivElement>();

    const submitMessage = () => {
        props.submitCast(pendingMessage!);
        // this.setState(prevState => { return {...prevState, messages: prevState.messages.concat([{
        //     // "timestamp": "1622272285000",
        //     "messageId": Math.random().toString(16).substring(2, 8), // just need a random value for now
        //     "content": {
        //         "type": "post",
        //         "senderId": "@<00000000-0000-0000-0000-000000000000>",
        //         "text": prevState.pendingMessage
        //     }
        // }]), pendingMessage: ""} });
    };

    const scrollToBottom = () => {
        ref.current!.scrollIntoView({ behavior: "smooth" });
    };

    React.useEffect(() => {
        scrollToBottom();
    }, [props.casts]);

    return (<div className="channel">
        <div className="channel-name">#untagged</div>
        <div className="message-list">
            {props.casts.map(message => {
                let sender = message.author;
                let time = moment.tz(message.timestamp, "America/Chicago");

                // todo: message glomming
                return <div key={message.hash} className="message">
                    <div className="message-sender-icon" style={{ backgroundImage: `url(${(sender.pfp || {url: ""}).url})` }}/>
                    <div className="message-content">
                        <span className="message-sender-name">{sender.displayName}</span><span className="message-timestamp">{time.format('h:mma')}</span>
                        {(() => {
                            // if (message..type == "post") {
                                // if (Array.isArray(message.text)) {
                                    // let content = (message.content.text as string[]);
                                    // return content.map((c, i) =>
                                    //     <div key={i} className="message-post-content">{c}</div>
                                    // );
                                // } else {
                                    let content = (message as {text: string});
                                    return <div className="message-post-content">{content.text}</div>;
                                // }
                            // } else if (message.content.type == "embed") {
                            //     let content = (message.content as {videoUrl: string, width?: string, height?: string});
                            //     return <div className="message-post-content">
                            //         <iframe
                            //             width={content.width || "560"}
                            //             height={content.height || "315"}
                            //             src={content.videoUrl} 
                            //             frameBorder="0"
                            //             allow="autoplay; encrypted-media"></iframe>
                            //     </div>;
                            // }
                        })()}
                        
                    </div>
                </div>
            })}
            <div className="scroll-target" ref={ref}></div>
        </div>
        <textarea
            className="message-editor"
            placeholder={"Send a message to #untagged"}
            rows={1}
            value={pendingMessage}
            onChange={(e) => setPendingMessage(e.target.value)}
            onKeyDown={(e) => {
                if (e.key === 'Enter') {
                    submitMessage();
                    e.preventDefault();
                }
            }}/>
    </div>);
}

export default connect(
    (state: ApplicationState) => { return {...state}; },
    {...Farcaster.actionCreators }
)(Channel);

  