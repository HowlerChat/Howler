import React from 'react';

import './Input.scss';

type InputProps = {
    placeholder?: string | undefined,
    value?: string | undefined,
    onChange?: React.ChangeEventHandler<HTMLInputElement>
}

const Input : React.FunctionComponent<InputProps> = props => {
    return <input className="howler-input" {...props}/>;
}

export default Input;