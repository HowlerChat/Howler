import * as React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import {faBars, faCompressAlt, faPlus, faSearch} from '@fortawesome/free-solid-svg-icons';
import { useState } from 'react';
import { useHistory } from 'react-router';
import Button from '../Button';
import Tooltip from '../Tooltip';
import { ApplicationState } from '../../store';
import * as Localization from '../../store/Localization';
import { connect } from 'react-redux';
import './ExpandableNavMenu.scss';

type ExpandableNavMenuProps = {
    showCreateSpaceModal: () => void,
    showJoinSpaceModal: () => void,
} & typeof Localization.actionCreators & Localization.LocalizationInfoState;

const ExpandableNavMenu : React.FunctionComponent<ExpandableNavMenuProps> = props => {
    let history = useHistory();
    const [isExpanded, setExpanded] = useState(false);

    return isExpanded ? 
        <div className="expanded-nav-menu">
            <div className="invisible-dismissal" onClick={() => setExpanded(false)}/>
            <Button className="expanded-nav-search-spaces" icon type="primary" onClick={(e) => history.push("/spaces/search")} tooltip={props.localizations["TOOLTIP_SEARCH_SPACES"]([])}><FontAwesomeIcon icon={faSearch}/></Button>
            <Button className="expanded-nav-add-spaces" icon type="primary" onClick={(e) => { setExpanded(false); props.showCreateSpaceModal(); }} tooltip={props.localizations["TOOLTIP_ADD_SPACE"]([])}><FontAwesomeIcon icon={faPlus}/></Button>
            <Button className="expanded-nav-join-spaces" icon type="primary" onClick={(e) => { setExpanded(false); props.showJoinSpaceModal(); }} tooltip={props.localizations["TOOLTIP_JOIN_SPACE"]([])}><FontAwesomeIcon icon={faCompressAlt}/></Button>
        </div> :
        <div className="expand-button" onClick={() => setExpanded(true)}><FontAwesomeIcon icon={faBars}/></div>;
};

export default connect(
    (state: ApplicationState) => state.localizations,
    Localization.actionCreators
)(ExpandableNavMenu);