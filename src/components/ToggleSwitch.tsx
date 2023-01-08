import * as React from 'react';
import './ToggleSwitch.scss';

type ToggleSwitchProps = {
    active: boolean,
    onClick?: React.MouseEventHandler<HTMLDivElement>,
};

const ToggleSwitch : React.FunctionComponent<ToggleSwitchProps> = props => {
    return <div onClick={props.onClick} className={"howler-toggle-switch" + (props.active ? " howler-toggle-switch-active" : "")}><div className="howler-toggle-switch-flipper"/></div>;
};

export default ToggleSwitch;