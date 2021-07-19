import * as Spaces from './Spaces';
import * as Localization from './Localization';
import * as Connection from './Connection';
import * as Servers from './Servers';
import * as Configs from './Configs';
import { all, call, select } from 'redux-saga/effects';
import { ServersState } from './Servers';

// The top-level state object
export interface ApplicationState {
    spaces: Spaces.SpacesState | undefined;
    localizations: Localization.LocalizationInfoState | undefined;
    connections: Connection.ConnectionInfoState | undefined;
    servers: Servers.ServersState | undefined;
    configs: Configs.ConfigState;
}

// Whenever an action is dispatched, Redux will update each top-level application state property using
// the reducer with the matching name. It's important that the names match exactly, and that the reducer
// acts on the corresponding ApplicationState property type.
export const reducers = {
    spaces: Spaces.reducer,
    localizations: Localization.reducer,
    connections: Connection.reducer,
    servers: Servers.reducer,
};

export const rootSaga = function*() {
    yield all([
        Spaces.spaceSagas.watchSpaceRequests(),
        Connection.connectionSagas.watchConnectionRequests(),
        Localization.localizationSagas.watchLocalizationInfoRequests(),
    ]);
};

// This type can be used as a hint on action creators so that its 'dispatch' and 'getState' params are
// correctly typed to match your store.
export interface AppThunkAction<TAction> {
    (dispatch: (action: TAction) => void, getState: () => ApplicationState): void;
}

const selectTokenForServer = (state: ApplicationState) => 
    (serverId: string) => (state.servers as ServersState).tokens[serverId];

export function* callServer<T>(
    serverId: string,
    callForServer: (getTokenForServer: (serverId: string) => string) => (serverId: string) => () => Promise<T>,
) {
    let getTokenForServer = selectTokenForServer(yield select());
    yield call(callForServer(getTokenForServer)(serverId));
}