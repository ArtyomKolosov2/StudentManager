import React from 'react';
import App from './App';
import './index.css';
import ReactDOM from 'react-dom';
import reportWebVitals from './reportWebVitals';
import 'bootstrap/dist/css/bootstrap.min.css';
import { BrowserRouter, Route, Switch } from 'react-router-dom';
import UserForm from './components/account/Login';



ReactDOM.render(
  <BrowserRouter>
      <App/>
  </BrowserRouter>
  ,
  document.getElementById('root')
);

reportWebVitals();
