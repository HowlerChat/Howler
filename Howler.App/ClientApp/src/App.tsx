import React from 'react';
import { Route } from 'react-router';
import { withAuthenticator, AmplifySignOut } from '@aws-amplify/ui-react';
import { AuthState, onAuthUIStateChange } from '@aws-amplify/ui-components';
import Layout from './components/Layout';
import Home from './components/Home';
import Counter from './components/Counter';
import FetchData from './components/FetchData';

import './custom.css'

const App = () => {
    const [authState, setAuthState] = React.useState<AuthState>();
    const [user, setUser] = React.useState<any>();

    React.useEffect(() => {
        return onAuthUIStateChange((nextAuthState, authData) => {
            setAuthState(nextAuthState);
            setUser(authData);
        });
    }, []);

    return <>
        <AmplifySignOut />
        <Layout>
            <Route exact path='/' component={Home} />
            <Route path='/counter' component={Counter} />
            <Route path='/fetch-data/:startDateIndex?' component={FetchData} />
        </Layout>
    </>;
};

export default withAuthenticator(App);
