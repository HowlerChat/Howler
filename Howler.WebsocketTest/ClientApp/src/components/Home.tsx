import * as React from 'react';
import { connect } from 'react-redux';
import * as signalR from '@microsoft/signalr';

class Home extends React.Component<{}, {connection: signalR.HubConnection | null, loginToken: string, error: string | null, user: string, message: string, otherUser: string, otherMessage: string}> {
  
  constructor(props: {}) {
    super(props);
    this.displayMessage = this.displayMessage.bind(this);
    
    this.state = {
      connection: null,
      loginToken: "",
      error: null,
      user: "",
      message: "",
      otherUser: "",
      otherMessage: ""
    };
  }

  public componentDidMount() {
  }

  public async connect() {
    // This is a really bad example for illustration only, we won't
    // be doing signalR like this in the actual client.
    let connection = new signalR.HubConnectionBuilder()
      .withUrl("http://localhost:8000/examplehub", { accessTokenFactory: () => this.state.loginToken }).build();
    try {
      await connection.start();
      connection.on("ReceiveMessage", this.displayMessage);

      this.setState({connection, error: null});
    } catch (e) {
      let error = e as signalR.HttpError;
      this.setState({error: error.message});
    }
  }

  public displayMessage(user: string, message: string) {
    this.setState({ otherUser: user, otherMessage: message });
  }

  public sendMessage() {
    if (this.state.connection != null) {
      this.state.connection.send("SendMessage", this.state.user, this.state.message);
    }
  }

  public render() {
    return <div>
      <h1>Ephemeral Chat Example</h1>
      {(this.state.error != null ? <div>Error: {this.state.error}</div> : <></>)}
      <div>State: {(this.state.connection || { state: signalR.HubConnectionState.Disconnected}).state}</div>
      {(() => {
        if (this.state.connection != null && this.state.connection.state == signalR.HubConnectionState.Connected) {
          return <><div>
            <label>Username:</label>
            <input type="text" value={this.state.user} onChange={(e) => this.setState({user: e.target.value})}></input>
            <label>Message:</label>
            <input type="text" value={this.state.message} onChange={(e) => this.setState({message: e.target.value})}></input>
            <button onClick={() => this.sendMessage()}>Send</button>
          </div>
          <div><b>{this.state.otherUser}: </b>{this.state.otherMessage}</div></>;
        } else {
          return <div>
            <label>Token:</label>
            <input type="text" value={this.state.loginToken} onChange={(e) => this.setState({loginToken: e.target.value})}></input>
            <button onClick={async () => await this.connect()}>Connect</button>
          </div>;
        }
      })()}
    </div>;
  }
};

export default connect()(Home);
