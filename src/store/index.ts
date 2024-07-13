import * as Spaces from './Spaces';
import * as Localization from './Localization';
import * as Servers from './Servers';
import * as Channels from './Channels';
import * as Configs from './Configs';
import { all, call, put, select } from 'redux-saga/effects';

// The top-level state object
export interface ApplicationState {
    spaces: Spaces.SpacesState | undefined;
    localizations: Localization.LocalizationInfoState | undefined;
    servers: Servers.ServersState | undefined;
    activeChannel: Channels.ChannelState | undefined;
    configs: Configs.ConfigState;
}

// Whenever an action is dispatched, Redux will update each top-level application state property using
// the reducer with the matching name. It's important that the names match exactly, and that the reducer
// acts on the corresponding ApplicationState property type.
export const reducers = {
    spaces: Spaces.spacesReducer,
    localizations: Localization.localizationReducer,
    servers: Servers.serversReducer,
    activeChannel: Channels.channelReducer,
};

export const rootSaga = function*() {
    yield all([
      Localization.localizationSagas.watchLocalizationInfoRequests(),
      Servers.serversSagas.watchServerTokenRequests(),
      Spaces.spaceSagas.watchSpaceRequests(),
      Channels.channelSagas.watchChannelRequests(),
    ]);
};

// This type can be used as a hint on action creators so that its 'dispatch' and 'getState' params are
// correctly typed to match your store.
export interface AppThunkAction<TAction> {
    (dispatch: (action: TAction) => void, getState: () => ApplicationState): void;
}
