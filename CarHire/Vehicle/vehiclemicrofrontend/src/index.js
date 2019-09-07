import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import { unregister } from './serviceWorker';

window.renderVehicleApp = (containerId) => {
  ReactDOM.render(
    <App />,
    document.getElementById(containerId),
  );
  unregister();
};

//ReactDOM.render(<App />, document.getElementById('root'));
//serviceWorker.unregister();

window.unmountVehicleApp = containerId => {
  ReactDOM.unmountComponentAtNode(document.getElementById(containerId));
};