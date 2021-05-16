import * as React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import Counter from './components/Counter';
import FetchData from './components/FetchData';
import QueryString from 'query-string';

import './custom.css'

class App extends React.Component<{}, {}> {
    componentDidMount() {
        let token = window.localStorage.getItem("token");

        if (token == null) {
            console.log(window.location.hash);
            if (window.location.hash.startsWith("#id_token=")) {
                let auth: any = QueryString.parse(window.location.hash);
                console.log(auth);
                window.localStorage.setItem("token", auth.id_token);
            } else {
                window.location.href = "https://auth.howler.chat/login?client_id=6b75ooll3b86ugauhu22vj39ra&response_type=token&scope=email+openid+profile&redirect_uri=http://localhost:8000";
            }
        }
    }

    render() {
        return <Layout>
            <Route exact path='/' component={Home} />
            <Route path='/counter' component={Counter} />
            <Route path='/fetch-data/:startDateIndex?' component={FetchData} />
        </Layout>;
    }
}

export default App;
