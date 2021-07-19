import { Action, Reducer } from 'redux';
import { call, put, select, takeEvery, takeLatest, takeLeading } from 'redux-saga/effects';
import { getServerToken } from '../api/gatewayApi';
import { ApplicationState, AppThunkAction } from './';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface ServersState {
    authToken: string | null;
    tokens: { [serverId: string]: string };
    lastRefreshedSpaceId: string | null;
    isLoading: boolean;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestServerTokenAction {
    type: 'REQUEST_SERVER_TOKEN';
    spaceId: string;
    token: string;
}

interface RequestingServerTokenAction {
    type: 'REQUESTING_SERVER_TOKEN';
    spaceId: string;
}

interface ReceiveServerTokenAction {
    type: 'RECEIVE_SERVER_TOKEN';
    spaceId: string;
    token: string;
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestServerTokenAction | RequestingServerTokenAction | ReceiveServerTokenAction;

// ----------------
// SAGAS - The declarative transaction flows for interacting with the store.

function* handleServerTokenRequest(request: RequestServerTokenAction) {
    let state: ServersState = yield select(s => s.servers as ServersState);
    
    if (state.tokens[request.spaceId] == null)
    {
        yield put({type: 'REQUESTING_SERVER_TOKEN', spaceId: request.spaceId});
        let space: Body = yield call(getServerToken(request.spaceId, request.token));
        let data: string = yield call(() => space.json());
        yield put({type: 'RECEIVE_SERVER_TOKEN', spaceId: request.spaceId, token: data });
    }
}

export const spaceSagas = {
    watchServerTokenRequests: function*() {
        yield takeLeading('REQUEST_SERVER_TOKEN', handleServerTokenRequest)
    }
};

// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestServerToken: (spaceId: string, token: string) => ({ type: 'REQUEST_SERVER_TOKEN', spaceId, token } as RequestServerTokenAction)
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: ServersState = { tokens: {}, authToken: null, isLoading: false, lastRefreshedSpaceId: null };

export const reducer: Reducer<ServersState> = (state: ServersState | undefined, incomingAction: Action): ServersState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUESTING_SERVER_TOKEN':
            return {
                ...state,
                lastRefreshedSpaceId: action.spaceId,
                isLoading: true
            };
        case 'RECEIVE_SERVER_TOKEN':
            // Only accept the incoming data if it matches the most recent request. This ensures we correctly
            // handle out-of-order responses.
            const receive = action as ReceiveServerTokenAction;
            return {
                authToken: state.authToken,
                lastRefreshedSpaceId: state.lastRefreshedSpaceId,
                tokens: {...state.tokens, [receive.spaceId]: receive.token},
                isLoading: false,
            };
    }

    return state;
};
