import * as React from 'react';
import NavMenu from './NavMenu';
import CloseButton from './CloseButton';
import Container from './Container';

export default class Layout extends React.PureComponent<{ children?: React.ReactNode }, {}> {
    public render() {
        return (
            <React.Fragment>
                <NavMenu />
                <CloseButton />
                <Container>
                    {this.props.children}
                </Container>
            </React.Fragment>
        );
    }
}