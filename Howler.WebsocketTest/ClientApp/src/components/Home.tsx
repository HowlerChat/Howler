import * as React from 'react';
import { connect } from 'react-redux';
import * as signalR from '@microsoft/signalr';

class Home extends React.Component<{}, {user: string, message: string, otherUser: string, otherMessage: string}> {
  private connection: signalR.HubConnection
  
  constructor(props: {}) {
    super(props);
    this.displayMessage = this.displayMessage.bind(this);
    this.connection = new signalR.HubConnectionBuilder().withUrl("http://localhost:8000/examplehub").build();
    this.connection.start();
    this.state = {
      user: "",
      message: "",
      otherUser: "",
      otherMessage: ""
    };
  }

  public componentDidMount() {
    this.connection.on("ReceiveMessage", this.displayMessage);
  }

  public displayMessage(user: string, message: string) {
    this.setState({ otherUser: user, otherMessage: message });
  }

  public sendMessage() {
    this.connection.send("SendMessage", this.state.user, this.state.message);
  }

  public render() {
    return <div>
      <h1>Ephemeral Chat Example</h1>
      <div>
        <label>Username:</label>
        <input type="text" value={this.state.user} onChange={(e) => this.setState({user: e.target.value})}></input>
        <label>Message:</label>
        <input type="text" value={this.state.message} onChange={(e) => this.setState({message: e.target.value})}></input>
        <button onClick={() => this.sendMessage()}>Send</button>
      </div>
      <div><b>{this.state.otherUser}: </b>{this.state.otherMessage}</div>
    </div>;
  }
};

export default connect()(Home);
