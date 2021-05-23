import { Action, Reducer } from 'redux';
import { call, put, select, takeEvery, takeLatest, takeLeading } from 'redux-saga/effects';
import { ApplicationState, AppThunkAction } from './';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface WeatherForecastsState {
    isLoading: boolean;
    startDateIndex?: number;
    forecasts: WeatherForecast[];
}

export interface WeatherForecast {
    date: string;
    temperatureC: number;
    temperatureF: number;
    summary: string;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestWeatherForecastsAction {
    type: 'REQUEST_WEATHER_FORECASTS';
    startDateIndex: number;
}

interface RequestingWeatherForecastsAction {
    type: 'REQUESTING_WEATHER_FORECASTS';
    startDateIndex: number;
}

interface ReceiveWeatherForecastsAction {
    type: 'RECEIVE_WEATHER_FORECASTS';
    startDateIndex: number;
    forecasts: WeatherForecast[];
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestWeatherForecastsAction | RequestingWeatherForecastsAction | ReceiveWeatherForecastsAction;

// ----------------
// SAGAS - The declarative transaction flows for interacting with the store.
const getWeatherForecasts = (state: ApplicationState) => state.weatherForecasts

function* handleWeatherRequest(request: RequestWeatherForecastsAction) {
    let weatherForecasts: WeatherForecastsState = yield select(getWeatherForecasts);
    // console.log(weatherForecasts);
    console.log(request);
    // Only load data if it's something we don't already have (and are not already loading)
    if (weatherForecasts && request.startDateIndex !== weatherForecasts.startDateIndex) {
        yield put({type: 'REQUESTING_WEATHER_FORECASTS', startDateIndex: request.startDateIndex })
        let forecast: Body = yield call(fetch, `weatherforecast`);
        let data: WeatherForecast = yield call(() => forecast.json());
        yield put({type: 'RECEIVE_WEATHER_FORECASTS', forecasts: data, startDateIndex: request.startDateIndex });
    }
}

export const weatherSagas = {
    watchWeatherRequests: function*() {
        yield takeLeading('REQUEST_WEATHER_FORECASTS', handleWeatherRequest)
    }
};

// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestWeatherForecasts: (startDateIndex: number) => ({ type: 'REQUEST_WEATHER_FORECASTS', startDateIndex } as RequestWeatherForecastsAction)
};


// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: WeatherForecastsState = { forecasts: [], isLoading: false };

export const reducer: Reducer<WeatherForecastsState> = (state: WeatherForecastsState | undefined, incomingAction: Action): WeatherForecastsState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUESTING_WEATHER_FORECASTS':
            console.log(action);
            return {
                startDateIndex: action.startDateIndex,
                forecasts: state.forecasts || [],
                isLoading: true
            };
        case 'RECEIVE_WEATHER_FORECASTS':
            // Only accept the incoming data if it matches the most recent request. This ensures we correctly
            // handle out-of-order responses.
            console.log(action);
            if (action.startDateIndex === state.startDateIndex) {
                return {
                    startDateIndex: action.startDateIndex,
                    forecasts: action.forecasts,
                    isLoading: false
                };
            }
            break;
    }

    return state;
};
