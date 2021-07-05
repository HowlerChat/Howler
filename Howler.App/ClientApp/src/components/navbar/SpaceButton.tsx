import * as React from 'react';
import { useParams } from 'react-router';
import { Link } from 'react-router-dom';
import SpaceIcon from '../SpaceIcon';
import './SpaceButton.scss';

type SpaceButtonProps = { space: any  };

const SpaceButton : React.FunctionComponent<SpaceButtonProps> = props => {
    let { spaceId } = useParams<{spaceId: string}>();
    return <Link className="space-button" to={`/spaces/${props.space.spaceId}/${props.space.defaultChannelId || "00000000-0000-0000-0000-000000000000"}`}>
        <SpaceIcon selected={spaceId == props.space.spaceId} size="regular" iconUrl={props.space.iconUrl} />
    </Link>;
}

export default SpaceButton;