import { tooltip } from 'aws-amplify';
import * as React from 'react';
import './Button.scss';
import Tooltip from './Tooltip';

type ButtonProps = {
    type: "primary" | "secondary" | "tertiary",
    disabled?: boolean,
    icon?: boolean,
    className?: string | undefined,
    onClick: React.MouseEventHandler<HTMLSpanElement>,
    tooltip?: string | undefined,
};

const Button : React.FunctionComponent<ButtonProps> = props => {
    const [isTooltipOpen, setTooltipOpen] = React.useState(false);
    
    return <>
        <span className={
            "howler-button " +
            "howler-button-" + props.type +
            (props.disabled ? " howler-button-disabled" : "") +
            (props.icon ? " howler-button-icon" : "") +
            (!!props.className ? " " + props.className : "")}
            onClick={props.onClick}
            onMouseEnter={() => setTooltipOpen(true)}
            onMouseLeave={() => setTooltipOpen(false)}
        >
            {props.children}
        </span>
        {(!!props.tooltip ?
            <Tooltip visible={isTooltipOpen} arrow="left">{props.tooltip}</Tooltip> :
            <></>)}
    </>;
};

export default Button;