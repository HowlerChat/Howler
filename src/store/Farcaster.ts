import { push } from 'connected-react-router';
import { Action, Reducer } from 'redux';
import { call, put, select, takeEvery, takeLatest, takeLeading } from 'redux-saga/effects';
import * as ethers from 'ethers';

import { MerkleAPIClient, Cast, User } from "../api/farcaster-js";
import { ApplicationState, AppThunkAction } from './';
import { userInfo } from 'os';


// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface FarcasterState {
    casts: Cast[];
    user: User | undefined;
    connection: MerkleAPIClient | undefined;
    isLoading: boolean;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestConnectionAction {
    type: 'REQUEST_CONNECTION';
    wallet: ethers.Wallet;
}

interface RequestingConnectionAction {
    type: 'REQUESTING_CONNECTION';
    wallet: ethers.Wallet;
}

interface ReconnectingAction {
    type: 'RECONNECTING';
}

interface FailedConnectionAction {
    type: 'FAILED_CONNECTION';
    error: string;
}

interface ReceiveConnectionAction {
    type: 'RECEIVE_CONNECTION';
    connection: MerkleAPIClient;
    user: User;
    casts: Cast[];
}

interface SendCastAction {
    type: 'SEND_CAST';
    message: string;
}

interface SubmittingCastAction {
    type: 'SUBMITTING_CAST';
    message: string;
}

interface SubmittedCastAction {
    type: 'SUBMITTED_CAST';
    casts: Cast[];
}

interface FailedCastAction {
    type: 'FAILED_CAST';
    message: string;
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestConnectionAction | RequestingConnectionAction | ReconnectingAction | ReceiveConnectionAction | FailedConnectionAction | SendCastAction | SubmittingCastAction | SubmittedCastAction | FailedCastAction;

// ----------------
// SAGAS - The declarative transaction flows for interacting with the store.

async function grabPages(client: MerkleAPIClient) {
    let frc = client.fetchRecentCasts();
    let count = 0;
    let casts: Cast[] = [];

    for await (const cast of frc) {
        casts.unshift(cast);
        count++;
        if (count > 100) break;
    }

    return casts;
}

async function publishCast(client: MerkleAPIClient, message: string) {
    try {
        await client.publishCast(message);
    } catch (e) {
        console.log(e);
    }
}

async function fetchCurrentUser(client: MerkleAPIClient) {
    try {
        return await client.fetchCurrentUser();
    } catch (e) {
        console.log(e);
    }
}

function* handleConnectionRequest(request: RequestConnectionAction) {
    yield put({type: 'REQUESTING_CONNECTION', wallet: request.wallet});
    
    try {
        let state = new MerkleAPIClient(request.wallet);
        let user: User = yield call(fetchCurrentUser, state);
        let casts: Cast[] = yield call(grabPages, state);
        yield put({type: 'RECEIVE_CONNECTION', connection: state, user: user, casts: casts });
    } catch (e) {
        yield put({type: 'FAILED_CONNECTION', wallet: request.wallet, error: e });
    }
}

function* handleSubmitCast(request: SendCastAction) {
    let state: FarcasterState = yield select(s => s.farcaster as FarcasterState);
    yield put({type: 'SUBMITTING_CAST', message: request.message});
    
    try {
        yield call(publishCast, state.connection, request.message);
        let casts: Cast[] = yield call(grabPages, state.connection!);
        yield put({type: 'SUBMITTED_CAST', casts: casts });
    } catch (e) {
        yield put({type: 'FAILED_CAST', message: request.message, error: e });
    }
}


export const farcasterSagas = {
    watchConnectionRequests: function*() {
        yield takeLeading('REQUEST_CONNECTION', handleConnectionRequest)
        yield takeLeading('SEND_CAST', handleSubmitCast)
    }
};

// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestConnection: (wallet: ethers.Wallet) => ({ type: 'REQUEST_CONNECTION', wallet } as RequestConnectionAction),
    submitCast: (message: string) => ({ type: 'SEND_CAST', message } as SendCastAction),
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: FarcasterState = { casts: [], user: undefined, isLoading: false, connection: undefined };

export const reducer: Reducer<FarcasterState> = (state: FarcasterState | undefined, incomingAction: Action): FarcasterState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUESTING_CONNECTION':
            const requestingConnection = action as RequestingConnectionAction;
            return {
                ...state,
                isLoading: true,
            };
        case 'RECEIVE_CONNECTION':
            // Only accept the incoming data if it matches the most recent request. This ensures we correctly
            // handle out-of-order responses.
            const receiveConnection = action as ReceiveConnectionAction;
            return {
                ...state,
                isLoading: false,
                connection: receiveConnection.connection,
                user: receiveConnection.user,
                casts: receiveConnection.casts,
            };
        case 'FAILED_CONNECTION':
            const failedConnection = action as FailedConnectionAction;
            return {
                ...state,
                isLoading: true,
                connection: undefined,
            }
        case 'SUBMITTING_CAST':
            const submittingCast = action as SubmittingCastAction;
            return {
                ...state,
            };
        case 'SUBMITTED_CAST':
            const submittedCast = action as SubmittedCastAction;
            return {
                ...state,
                casts: action.casts
            };
        case 'FAILED_CAST':
            return {
                ...state,
            };
        case 'REQUEST_CONNECTION':
            return {
                ...state,
            };
        case 'SEND_CAST':
            return {
                ...state,
            };
    }

    return state;
};
