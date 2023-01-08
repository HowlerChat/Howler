import * as React from 'react';
import { useParams } from 'react-router';
import { Link } from 'react-router-dom';
import SpaceIcon from '../SpaceIcon';
import './SpaceButton.scss';

type SpaceButtonProps = { space: any  };

const SpaceButton : React.FunctionComponent<SpaceButtonProps> = props => {
    let { spaceId } = useParams<{spaceId: string}>();
    return <Link className="space-button" to={`/casts`}>
        <SpaceIcon selected={true} size="regular" iconUrl={props.space.iconUrl} />
    </Link>;
}

export default SpaceButton;
