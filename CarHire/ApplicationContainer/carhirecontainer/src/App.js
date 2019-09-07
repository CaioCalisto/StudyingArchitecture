import React from 'react';
import { BrowserRouter, Switch, Route } from 'react-router-dom';
import AppHeader from './AppHeader';
import MicroFrontend from './MicroFrontend';
import './App.css';

const {
  REACT_APP_CUSTOMER_HOST: customerhost,
  REACT_APP_VEHICLE_HOST: vehiclehost,
} = process.env;

const VehicleApp = () => (
  <MicroFrontend host={vehiclehost} name="VehicleApp" />
);

const CustomerApp = () => (
  <MicroFrontend host={customerhost} name="CustomerApp" />
);

const App = () => {
  return (
    <BrowserRouter>
        <React.Fragment>
          <AppHeader />
          <Switch>
            <Route exact path="/vehicle" component={VehicleApp} />
            <Route exact path="/customer" component={CustomerApp} />
          </Switch>
         </React.Fragment>
      </BrowserRouter>
  );
}

export default App;
