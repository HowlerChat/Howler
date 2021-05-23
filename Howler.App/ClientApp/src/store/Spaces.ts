import { Action, Reducer } from 'redux';
import { call, put, select, takeEvery, takeLatest, takeLeading } from 'redux-saga/effects';
import { getSpace } from '../api/howlerApi';
import { ApplicationState, AppThunkAction } from './';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface SpaceState {
    spaceId: string;
    spaceName: string;
    description: string;
    vanityUrl: string;
    serverUrl: string;
    createdDate: Date;
    modifiedDate: Date;
}

export interface SpacesState {
    isLoading: boolean;
    spaces: SpaceState[];
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestSpaceAction {
    type: 'REQUEST_SPACE';
    spaceId: string;
    token: string;
}

interface RequestingSpaceAction {
    type: 'REQUESTING_SPACE';
    spaceId: string;
}

interface ReceiveSpaceAction {
    type: 'RECEIVE_SPACE';
    space: SpaceState;
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestSpaceAction | RequestingSpaceAction | ReceiveSpaceAction;

// ----------------
// SAGAS - The declarative transaction flows for interacting with the store.

// TODO: const getAuthToken = (state: ApplicationState) => state.user.signInUserSession.accessToken.jwtToken;

function* handleSpaceRequest(request: RequestSpaceAction) {
    yield put({type: 'REQUESTING_SPACE', spaceId: request.spaceId});
    let space: Body = yield call(getSpace(request.spaceId, request.token));
    let data: SpaceState = yield call(() => space.json());
    yield put({type: 'RECEIVE_SPACE', space: data });
}

export const spaceSagas = {
    watchSpaceRequests: function*() {
        yield takeLeading('REQUEST_SPACE', handleSpaceRequest)
    }
};

// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestSpace: (spaceId: string, token: string) => ({ type: 'REQUEST_SPACE', spaceId, token } as RequestSpaceAction)
};


// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: SpacesState = { spaces: [], isLoading: false };

export const reducer: Reducer<SpacesState> = (state: SpacesState | undefined, incomingAction: Action): SpacesState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUESTING_SPACE':
            return {
                spaces: state.spaces || [],
                isLoading: true
            };
        case 'RECEIVE_SPACE':
            // Only accept the incoming data if it matches the most recent request. This ensures we correctly
            // handle out-of-order responses.
            const receive = action as ReceiveSpaceAction;
            return {
                spaces: (state.spaces || []).filter(s => s.spaceId != receive.space.spaceId).concat([receive.space]),
                isLoading: false
            }
            break;
    }

    return state;
};
