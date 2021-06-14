import * as React from 'react';
import { AuthState } from '@aws-amplify/ui-components';
import NavMenu from './NavMenu';
import CloseButton from './CloseButton';
import Container from './Container';

export default class Layout extends React.PureComponent<{ children?: React.ReactNode, setAuthState: React.Dispatch<React.SetStateAction<AuthState | undefined>> }, {}> {
    public render() {
        return (
            <React.Fragment>
                <NavMenu setAuthState={this.props.setAuthState} />
                <CloseButton />
                <Container>
                    {this.props.children}
                </Container>
            </React.Fragment>
        );
    }
}