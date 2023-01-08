import * as React from 'react';
import './Tooltip.scss';

type TooltipProps = {
    arrow: "up" | "down" | "right" | "left" | "none",
    visible?: boolean,
    className?: string | undefined,
};

const Tooltip : React.FunctionComponent<TooltipProps> = props =>
    <span className={"howler-tooltip howler-tooltip-arrow-" + props.arrow + (!props.visible ? " howler-tooltip-invisible" : "")}>
        {props.children}
    </span>;

export default Tooltip;