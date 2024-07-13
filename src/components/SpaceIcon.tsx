import * as React from 'react';
import './SpaceIcon.scss';

type SpaceIconProps = {
    selected: boolean,
    iconUrl: string,
    size: "regular" | "large"
}
const SpaceIcon : React.FunctionComponent<SpaceIconProps> = props => {
    return <div className={`space-icon ${(props.selected ? "space-icon-selected" : "")} space-icon-${props.size}`} style={{ backgroundImage: `url(${props.iconUrl})` }}></div>;
};

export default SpaceIcon;
