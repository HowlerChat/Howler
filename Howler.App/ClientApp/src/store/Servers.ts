import { push } from 'connected-react-router';
import { Action, Reducer } from 'redux';
import { call, put, select, takeEvery, takeLatest, takeLeading } from 'redux-saga/effects';
import { getServerToken } from '../api/gatewayApi';
import { connect } from '../api/howlerSocket';
import { ApplicationState, AppThunkAction, callServer } from './';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface ServersState {
    authToken: string;
    servers: { [serverId: string]: ConnectionInfoState };
    lastRefreshedServerId: string | null;
    isLoading: boolean;
}

export interface ConnectionInfoState {
    isConnecting: boolean;
    connection?: signalR.HubConnection;
    token: string;
    userSpaces: { [userId: string]: string[] };
    error?: string;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface SetAuthTokenAction {
    type: 'SET_AUTH_TOKEN';
    token: string;
}

interface RequestServerTokenAction {
    type: 'REQUEST_SERVER_TOKEN';
    serverId: string;
}

interface RequestingServerTokenAction {
    type: 'REQUESTING_SERVER_TOKEN';
    serverId: string;
}

interface ReceiveServerTokenAction {
    type: 'RECEIVE_SERVER_TOKEN';
    serverId: string;
    token: string;
}

interface RequestConnectionAction {
    type: 'REQUEST_CONNECTION';
    serverId: string;
}

interface RequestingConnectionAction {
    type: 'REQUESTING_CONNECTION';
    serverId: string;
}

interface ReconnectingAction {
    type: 'RECONNECTING';
    serverId: string;
}

interface FailedConnectionAction {
    type: 'FAILED_CONNECTION';
    serverId: string;
    error: string;
}

interface ReceiveConnectionAction {
    type: 'RECEIVE_CONNECTION';
    serverId: string;
    connection: signalR.HubConnection;
}

interface UpdateUserSpacesAction {
    type: 'UPDATE_USER_SPACES';
    serverId: string;
    userSpaces: { userId: string, spaceIds: string[] };
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = SetAuthTokenAction | RequestServerTokenAction | RequestingServerTokenAction | ReceiveServerTokenAction | RequestConnectionAction | RequestingConnectionAction | ReconnectingAction | ReceiveConnectionAction | FailedConnectionAction | UpdateUserSpacesAction;

// ----------------
// SAGAS - The declarative transaction flows for interacting with the store.

function* handleServerTokenRequest(request: RequestServerTokenAction) {
    let state: ServersState = yield select(s => s.servers as ServersState);
    
    if (state.servers[request.serverId] == null || state.servers[request.serverId].token == null)
    {
        yield put({type: 'REQUESTING_SERVER_TOKEN', serverId: request.serverId});
        let token: string = yield call(getServerToken(request.serverId, state.authToken));
        yield put({type: 'RECEIVE_SERVER_TOKEN', serverId: request.serverId, token });
    }
}

function* handleConnectionRequest(request: RequestConnectionAction) {
    yield put({type: 'REQUESTING_CONNECTION', serverId: request.serverId});
    yield handleServerTokenRequest({type: 'REQUEST_SERVER_TOKEN', serverId: request.serverId});
    let response: { connection: signalR.HubConnection, error: any } = yield callServer(request.serverId, connect);
    
    if (response.error != null)
    {
        yield put({type: 'FAILED_CONNECTION', serverId: request.serverId, error: response.error });
    }
    else
    {
        yield put({type: 'RECEIVE_CONNECTION', serverId: request.serverId, connection: response.connection});
    }
}


export const serversSagas = {
    watchServerTokenRequests: function*() {
        yield takeLeading('REQUEST_CONNECTION', handleConnectionRequest)
        yield takeLeading('REQUEST_SERVER_TOKEN', handleServerTokenRequest)
    }
};

// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    setAuthToken: (token: string) => ({ type: 'SET_AUTH_TOKEN', token } as SetAuthTokenAction),
    requestServerToken: (serverId: string) => ({ type: 'REQUEST_SERVER_TOKEN', serverId } as RequestServerTokenAction),
    requestConnection: (serverId: string) => ({ type: 'REQUEST_CONNECTION', serverId } as RequestConnectionAction),
    updateUserSpaces: (serverId: string, userSpaces: { userId: string, spaceIds: string[] }) => ({ type: 'UPDATE_USER_SPACES', serverId: serverId, userSpaces } as UpdateUserSpacesAction),
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: ServersState = { servers: {}, authToken: '', isLoading: false, lastRefreshedServerId: null };

export const reducer: Reducer<ServersState> = (state: ServersState | undefined, incomingAction: Action): ServersState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'SET_AUTH_TOKEN':
            return {
                ...state,
                authToken: action.token
            };
        case 'REQUESTING_SERVER_TOKEN':
            return {
                ...state,
                lastRefreshedServerId: action.serverId,
                isLoading: true
            };
        case 'RECEIVE_SERVER_TOKEN':
            // Only accept the incoming data if it matches the most recent request. This ensures we correctly
            // handle out-of-order responses.
            const receiveToken = action as ReceiveServerTokenAction;
            return {
                ...state,
                lastRefreshedServerId: state.lastRefreshedServerId,
                servers: {...state.servers, [receiveToken.serverId]: {...state.servers[receiveToken.serverId], token: receiveToken.token}},
                isLoading: false,
            };
        case 'REQUESTING_CONNECTION':
            const requestingConnection = action as RequestingConnectionAction;
            return {
                ...state,
                servers: {
                    ...state.servers,
                    [requestingConnection.serverId]: {
                        ...state.servers[requestingConnection.serverId], 
                        isConnecting: true,
                    }
                }
            };
        case 'RECEIVE_CONNECTION':
            // Only accept the incoming data if it matches the most recent request. This ensures we correctly
            // handle out-of-order responses.
            const receiveConnection = action as ReceiveConnectionAction;
            return {
                ...state,
                servers: {
                    ...state.servers,
                    [receiveConnection.serverId]: {
                        ...state.servers[receiveConnection.serverId], 
                        isConnecting: false,
                        connection: receiveConnection.connection,
                    }
                }
            };
        case 'FAILED_CONNECTION':
            const failedConnection = action as FailedConnectionAction;
            return {
                ...state,
                servers: {
                    ...state.servers,
                    [failedConnection.serverId]: {
                        ...state.servers[failedConnection.serverId], 
                        isConnecting: false,
                        connection: undefined,
                        error: failedConnection.error,
                    }
                }
            }
        case 'UPDATE_USER_SPACES':
            const userSpaceUpdate = action as UpdateUserSpacesAction;
            return {
                ...state,
                servers: {
                    ...state.servers,
                    [userSpaceUpdate.serverId]: {
                        ...state.servers[userSpaceUpdate.serverId], 
                        userSpaces: {...state.servers[userSpaceUpdate.serverId].userSpaces, [userSpaceUpdate.userSpaces.userId]: userSpaceUpdate.userSpaces.spaceIds},
                    }
                }
            };
    }

    return state;
};
