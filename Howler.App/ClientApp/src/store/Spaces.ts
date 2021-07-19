import { Action, Reducer } from 'redux';
import { call, put, select, takeEvery, takeLatest, takeLeading } from 'redux-saga/effects';
import { getSpace, Space } from '../api/howlerApi';
import { ApplicationState, AppThunkAction, callServer } from './';
import { ServersState } from './Servers';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.
export interface SpacesState {
    isLoading: boolean;
    spaces: Space[];
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.
interface RequestSpaceAction {
    type: 'REQUEST_SPACE';
    serverId: string;
    spaceId: string;
}

interface RequestingSpaceAction {
    type: 'REQUESTING_SPACE';
    spaceId: string;
}

interface ReceiveSpaceAction {
    type: 'RECEIVE_SPACE';
    space: Space;
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestSpaceAction | RequestingSpaceAction | ReceiveSpaceAction;

// ----------------
// SAGAS - The declarative transaction flows for interacting with the store.
function* handleSpaceRequest(request: RequestSpaceAction) {
    yield put({type: 'REQUESTING_SPACE', spaceId: request.spaceId});
    let space: Space = yield callServer(request.serverId, getSpace(request.spaceId))
    yield put({type: 'RECEIVE_SPACE', space });
}

export const spaceSagas = {
    watchSpaceRequests: function*() {
        yield takeLeading('REQUEST_SPACE', handleSpaceRequest)
    }
};

// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestSpace: (serverId: string, spaceId: string) => ({ type: 'REQUEST_SPACE', serverId, spaceId } as RequestSpaceAction)
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
