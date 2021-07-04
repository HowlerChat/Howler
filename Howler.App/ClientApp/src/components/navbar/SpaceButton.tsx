import * as React from 'react';
import { useParams } from 'react-router';
import { Link } from 'react-router-dom';
import './SpaceButton.scss';

type SpaceButtonProps = { space: any  };

const SpaceButton : React.FunctionComponent<SpaceButtonProps> = props => {
    let { spaceId } = useParams<{spaceId: string}>();
    return <Link to={`/spaces/${props.space.spaceId}/${props.space.defaultChannelId || "00000000-0000-0000-0000-000000000000"}`}>
        <div className={`space-icon ${(spaceId == props.space.spaceId ? "space-icon-selected" : "")}`} style={{ backgroundImage: `url(${props.space.iconUrl})` }}></div>
    </Link>;
}

export default SpaceButton;