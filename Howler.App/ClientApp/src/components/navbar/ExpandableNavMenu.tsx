import * as React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import {faBars, faCompressAlt, faPlus, faSearch} from '@fortawesome/free-solid-svg-icons';
import './ExpandableNavMenu.scss';
import { useState } from 'react';
import { Button, Tooltip } from 'reactstrap';
import { useHistory } from 'react-router';

export default function ExpandableNavMenu() {
    let history = useHistory();
    const [isExpanded, setExpanded] = useState(false);
    const [isSearchTooltipOpen, setSearchTooltipOpen] = useState(false);
    const [isAddTooltipOpen, setAddTooltipLOpen] = useState(false);
    const [isJoinTooltipOpen, setJoinTooltipOpen] = useState(false);
    const toggleSearchTooltip = () => setSearchTooltipOpen(!isSearchTooltipOpen);
    const toggleAddTooltip = () => setAddTooltipLOpen(!isAddTooltipOpen);
    const toggleJoinTooltip = () => setJoinTooltipOpen(!isJoinTooltipOpen);

    return isExpanded ? 
        <div className="expanded-nav-menu">
            <div className="invisible-dismissal" onClick={() => setExpanded(false)}/>
            <Button className="expanded-nav-search-spaces" id="expanded-nav-search-button" onClick={(e) => history.push("/spaces/search")}><FontAwesomeIcon icon={faSearch}/></Button><Tooltip target="expanded-nav-search-button" placement="right" isOpen={isSearchTooltipOpen} toggle={toggleSearchTooltip}>Search for Public Spaces</Tooltip>
            <Button className="expanded-nav-search-spaces" id="expanded-nav-add-button" onClick={(e) => history.push("/spaces/add")}><FontAwesomeIcon icon={faPlus}/></Button><Tooltip target="expanded-nav-add-button" placement="right" isOpen={isAddTooltipOpen} toggle={toggleAddTooltip}>Add a Space</Tooltip>
            <Button className="expanded-nav-search-spaces" id="expanded-nav-join-button" onClick={(e) => history.push("/spaces/join")}><FontAwesomeIcon icon={faCompressAlt}/></Button><Tooltip target="expanded-nav-join-button" placement="right" isOpen={isJoinTooltipOpen} toggle={toggleJoinTooltip}>Join an existing Space</Tooltip>
        </div> :
        <div className="expand-button" onClick={() => setExpanded(true)}><FontAwesomeIcon icon={faBars}/></div>
}