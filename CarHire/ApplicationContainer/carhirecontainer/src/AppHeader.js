import React from 'react';
import { NavLink } from 'react-router-dom';
import './AppHeader.css';

const AppHeader = () => (
  <header>
    <div className="center-column">
      <h1>Car Hire</h1>
    </div>
    <nav>
      <ol className="center-column">
        <li>
          <NavLink to="/">Home</NavLink>
        </li>
        <li>
          <NavLink to="/vehicle">Vehicles</NavLink>
        </li>
        <li>
          <NavLink to="/customer">Customer</NavLink>
        </li>
      </ol>
    </nav>
  </header>
);

export default AppHeader;