import { Action, Reducer } from 'redux';
import { call, put, takeLeading } from 'redux-saga/effects';
import { Channel, getChannel, Space } from '../api/howlerApi';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.
export interface ChannelState {
    isLoading: boolean;
    channel: Channel | undefined;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.
interface RequestChannelAction {
    type: 'REQUEST_CHANNEL';
    serverId: string;
    spaceId: string;
    channelId: string;
    token: string;
}

interface RequestingChannelAction {
    type: 'REQUESTING_CHANNEL';
    spaceId: string;
    channelId: string;
}

interface ReceiveChannelAction {
    type: 'RECEIVE_CHANNEL';
    channel: Channel;
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestChannelAction | RequestingChannelAction | ReceiveChannelAction;

// ----------------
// SAGAS - The declarative transaction flows for interacting with the store.
function* handleChannelRequest(request: RequestChannelAction) {
    yield put({type: 'REQUESTING_CHANNEL', spaceId: request.spaceId, channelId: request.channelId});
    let channel: Space = yield call(getChannel(request.spaceId, request.channelId)(request.token));
    yield put({type: 'RECEIVE_CHANNEL', channel });
}

export const channelSagas = {
    watchChannelRequests: function*() {
        yield takeLeading('REQUEST_CHANNEL', handleChannelRequest)
    }
};

// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestChannel: (serverId: string, spaceId: string, channelId: string) => ({ type: 'REQUEST_CHANNEL', serverId, spaceId, channelId } as RequestChannelAction)
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.
export const unloadedState: ChannelState = { channel: undefined, isLoading: false };

export const channelReducer: Reducer<ChannelState> = (state: ChannelState | undefined, incomingAction: Action): ChannelState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUESTING_CHANNEL':
            return {
                channel: undefined,
                isLoading: true
            };
        case 'RECEIVE_CHANNEL':
            // Only accept the incoming data if it matches the most recent request. This ensures we correctly
            // handle out-of-order responses.
            const receive = action as ReceiveChannelAction;
            return {
                channel: state.channel,
                isLoading: false
            };
    }

    return state;
};
