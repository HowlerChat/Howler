import * as Localization from './Localization';
import * as Farcaster from './Farcaster';
import * as Configs from './Configs';
import { all, call, put, select } from 'redux-saga/effects';

// The top-level state object
export interface ApplicationState {
    localizations: Localization.LocalizationInfoState | undefined;
    farcaster: Farcaster.FarcasterState;
    configs: Configs.ConfigState;
}

// Whenever an action is dispatched, Redux will update each top-level application state property using
// the reducer with the matching name. It's important that the names match exactly, and that the reducer
// acts on the corresponding ApplicationState property type.
export const reducers = {
    localizations: Localization.reducer,
    farcaster: Farcaster.reducer,
};

export const rootSaga = function*() {
    yield all([
        Farcaster.farcasterSagas.watchConnectionRequests(),
        Localization.localizationSagas.watchLocalizationInfoRequests(),
    ]);
};

// This type can be used as a hint on action creators so that its 'dispatch' and 'getState' params are
// correctly typed to match your store.
export interface AppThunkAction<TAction> {
    (dispatch: (action: TAction) => void, getState: () => ApplicationState): void;
}
