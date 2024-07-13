import 'bootstrap/dist/css/bootstrap.css';

import * as React from 'react';
import { createRoot } from 'react-dom/client';
import { Provider } from 'react-redux';
import { RouterProvider } from 'react-router';
import { createBrowserRouter } from 'react-router-dom';
import App from './App';
import createStore from './store/configureStore';
import { PasskeysProvider } from './components/context/PasskeysContext';

// Create browser history to use in the Redux store
// const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href') as string;
const router = createBrowserRouter([
  {
    path: "/*",
    element: <App />,
    // errorElement: <Error />,
    children: []
  },
]);

// Get the application-wide store instance, prepopulating with state from the server where available.
const store = createStore();

const container = document.getElementById('root');
const root = createRoot(container!);
root.render(
  <PasskeysProvider>
    <Provider store={store}>
      <RouterProvider router={router}/>
    </Provider>
  </PasskeysProvider>,
);
