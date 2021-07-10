import * as Spaces from './Spaces';
import * as Localization from './Localization';
import * as Connection from './Connection';
import { all } from 'redux-saga/effects';

// The top-level state object
export interface ApplicationState {
    spaces: Spaces.SpacesState | undefined;
    localizations: Localization.LocalizationInfoState | undefined;
    connections: Connection.ConnectionInfoState | undefined;
}

// Whenever an action is dispatched, Redux will update each top-level application state property using
// the reducer with the matching name. It's important that the names match exactly, and that the reducer
// acts on the corresponding ApplicationState property type.
export const reducers = {
    spaces: Spaces.reducer,
    localizations: Localization.reducer,
    connections: Connection.reducer,
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
