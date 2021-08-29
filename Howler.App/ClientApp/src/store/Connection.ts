import { Action, Reducer } from 'redux';
import { call, put, select, takeEvery, takeLatest, takeLeading } from 'redux-saga/effects';
import { getLocalization, getSpace } from '../api/howlerApi';
import { connect } from '../api/howlerSocket';
import * as signalR from '@microsoft/signalr';
import { ApplicationState, AppThunkAction, callServer } from './';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.
export interface ConnectionInfoState {
    isConnecting: boolean;
    connection?: signalR.HubConnection;
    userSpaces: { [userId: string]: string[] };
    error?: string;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.
interface RequestConnectionAction {
    type: 'REQUEST_CONNECTION';
    serverId: string;
}

interface RequestingConnectionAction {
    type: 'REQUESTING_CONNECTION';
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
    connection: signalR.HubConnection;
}

interface UpdateUserSpacesAction {
    type: 'UPDATE_USER_SPACES';
    userSpaces: { userId: string, spaceIds: string[] };
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestConnectionAction | RequestingConnectionAction | ReconnectingAction | ReceiveConnectionAction | FailedConnectionAction | UpdateUserSpacesAction;

// ----------------
// SAGAS - The declarative transaction flows for interacting with the store.

function* handleConnectionRequest(request: RequestConnectionAction) {
    yield put({type: 'REQUESTING_CONNECTION'});
    let spaces: { [userId: string]: string[] } = yield select(s => s.connections.userSpaces as { [userId: string]: string[] });
    let response: { connection: signalR.HubConnection, error: any } = yield callServer(request.serverId, connect);
    
    if (response.error != null)
    {
        yield put({type: 'FAILED_CONNECTION', error: response.error });
    }
    else
    {
        response.connection.send("SubscribeToSpacesAndChannel", Object.keys(spaces), "", "")
        yield put({type: 'RECEIVE_CONNECTION', connection: response.connection});
    }
}

export const connectionSagas = {
    watchConnectionRequests: function*() {
        yield takeLeading('REQUEST_CONNECTION', handleConnectionRequest)
    }
};

// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestConnection: (serverId: string) => ({ type: 'REQUEST_CONNECTION', serverId } as RequestConnectionAction),
    updateUserSpaces: (userSpaces: { userId: string, spaceIds: string[] }) => ({ type: 'UPDATE_USER_SPACES', userSpaces } as UpdateUserSpacesAction),
};


// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: ConnectionInfoState = { isConnecting: false, userSpaces: {} };

export const reducer: Reducer<ConnectionInfoState> = (state: ConnectionInfoState | undefined, incomingAction: Action): ConnectionInfoState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUESTING_CONNECTION':
            return {
                isConnecting: true,
                userSpaces: state.userSpaces,
            };
        case 'RECEIVE_CONNECTION':
            // Only accept the incoming data if it matches the most recent request. This ensures we correctly
            // handle out-of-order responses.
            const receive = action as ReceiveConnectionAction;
            return {
                isConnecting: false,
                connection: receive.connection,
                userSpaces: state.userSpaces,
            };
        case 'FAILED_CONNECTION':
            const failed = action as FailedConnectionAction;
            return {
                isConnecting: false,
                connection: undefined,
                userSpaces : state.userSpaces,
                error: failed.error,
            }
        case 'UPDATE_USER_SPACES':
            const userSpaceUpdate = action as UpdateUserSpacesAction;
            return {
                ...state,
                userSpaces: {...state.userSpaces, [userSpaceUpdate.userSpaces.userId]: userSpaceUpdate.userSpaces.spaceIds},
            };
    }

    return state;
};
