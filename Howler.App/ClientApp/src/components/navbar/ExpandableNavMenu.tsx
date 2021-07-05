import * as React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import {faBars, faCompressAlt, faPlus, faSearch} from '@fortawesome/free-solid-svg-icons';
import './ExpandableNavMenu.scss';
import { useState } from 'react';
import { useHistory } from 'react-router';
import Button from '../Button';
import Tooltip from '../Tooltip';

type ExpandableNavMenuProps = {
    showCreateSpaceModal: () => void,
    showJoinSpaceModal: () => void,
};

const ExpandableNavMenu : React.FunctionComponent<ExpandableNavMenuProps> = props => {
    let history = useHistory();
    const [isExpanded, setExpanded] = useState(false);

    return isExpanded ? 
        <div className="expanded-nav-menu">
            <div className="invisible-dismissal" onClick={() => setExpanded(false)}/>
            <Button className="expanded-nav-search-spaces" icon type="primary" onClick={(e) => history.push("/spaces/search")} tooltip="Search for Public Spaces"><FontAwesomeIcon icon={faSearch}/></Button>
            <Button className="expanded-nav-add-spaces" icon type="primary" onClick={(e) => { setExpanded(false); props.showCreateSpaceModal(); }} tooltip="Add a Space"><FontAwesomeIcon icon={faPlus}/></Button>
            <Button className="expanded-nav-join-spaces" icon type="primary" onClick={(e) => { setExpanded(false); props.showJoinSpaceModal(); }} tooltip="Join an existing Space"><FontAwesomeIcon icon={faCompressAlt}/></Button>
        </div> :
        <div className="expand-button" onClick={() => setExpanded(true)}><FontAwesomeIcon icon={faBars}/></div>;
};

export default ExpandableNavMenu;