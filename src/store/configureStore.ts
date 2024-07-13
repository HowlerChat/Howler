import { applyMiddleware, combineReducers, StoreEnhancer } from 'redux';
import { ApplicationState, reducers, rootSaga } from './';
import createSagaMiddleware from 'redux-saga';
import { configureStore } from '@reduxjs/toolkit';
import * as Spaces from './Spaces';
import * as Localization from './Localization';
import * as Servers from './Servers';

export default function createStore(initialState?: ApplicationState) {
    const sagaMiddleware = createSagaMiddleware();

    const rootReducer = combineReducers({
        ...reducers,
    });

    const enhancers: StoreEnhancer[] = [];
    const windowIfDefined = typeof window === 'undefined' ? null : window as any; // eslint-disable-line @typescript-eslint/no-explicit-any
    if (windowIfDefined && windowIfDefined.__REDUX_DEVTOOLS_EXTENSION__) {
      enhancers.push(windowIfDefined.__REDUX_DEVTOOLS_EXTENSION__());
    }

    const store = configureStore({
      reducer: rootReducer,
      preloadedState: {
        spaces: Spaces.unloadedState,
        localizations: Localization.unloadedState,
        servers: Servers.unloadedState,
        ...initialState,
      },
      middleware: (gDW) => gDW({serializableCheck: false}).concat(sagaMiddleware),
      enhancers: (gDE) => gDE().concat(...enhancers),
    });

    sagaMiddleware.run(rootSaga);

    return store;
}
