import React from 'react';

import './Input.scss';

type InputProps = {
    placeholder?: string | undefined
}

const Input : React.FunctionComponent<InputProps> = props => {
    return <input className="howler-input" placeholder={props.placeholder}/>;
}

export default Input;