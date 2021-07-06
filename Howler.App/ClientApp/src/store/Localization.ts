import { Action, Reducer } from 'redux';
import { call, put, select, takeEvery, takeLatest, takeLeading } from 'redux-saga/effects';
import { getLocalization, getSpace } from '../api/howlerApi';
import { ApplicationState, AppThunkAction } from './';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface LocalizationInfoState {
    isLoading: boolean;
    direction: string;
    langId: string;
    localizations: LocalizationMap;
}

export type LocalizationMap = { [key: string]: (args: object) => string }

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestLocalizationInfoAction {
    type: 'REQUEST_LOCALIZATION_INFO';
    langId: string;
}

interface RequestingLocalizationInfoAction {
    type: 'REQUESTING_LOCALIZATION_INFO';
    langId: string;
}

interface ReceiveLocalizationInfoAction {
    type: 'RECEIVE_LOCALIZATION_INFO';
    localizationInfo: LocalizationInfoState;
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestLocalizationInfoAction | RequestingLocalizationInfoAction | ReceiveLocalizationInfoAction;

// ----------------
// SAGAS - The declarative transaction flows for interacting with the store.

// TODO: const getAuthToken = (state: ApplicationState) => state.user.signInUserSession.accessToken.jwtToken;

function* handleLocalizationInfoRequest(request: RequestLocalizationInfoAction) {
    yield put({type: 'REQUESTING_LOCALIZATION_INFO', langId: request.langId});
    let localizations: LocalizationInfoState = yield call(getLocalization(request.langId));
    yield put({type: 'RECEIVE_LOCALIZATION_INFO', localizationInfo: localizations });
}

export const localizationSagas = {
    watchLocalizationInfoRequests: function*() {
        yield takeLeading('REQUEST_LOCALIZATION_INFO', handleLocalizationInfoRequest)
    }
};

// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestLocalization: (langId: string) => ({ type: 'REQUEST_LOCALIZATION_INFO', langId } as RequestLocalizationInfoAction)
};


// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: LocalizationInfoState = { isLoading: false, langId: "", direction: "ltr", localizations: {} };

export const reducer: Reducer<LocalizationInfoState> = (state: LocalizationInfoState | undefined, incomingAction: Action): LocalizationInfoState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUESTING_LOCALIZATION_INFO':
            return {
                langId: action.langId,
                direction: state.direction,
                localizations: state.localizations,
                isLoading: true
            };
        case 'RECEIVE_LOCALIZATION_INFO':
            // Only accept the incoming data if it matches the most recent request. This ensures we correctly
            // handle out-of-order responses.
            const receive = action as ReceiveLocalizationInfoAction;
            return {
                langId: receive.localizationInfo.langId,
                direction: receive.localizationInfo.direction,
                localizations: receive.localizationInfo.localizations,
                isLoading: false
            }
    }

    return state;
};
