import React from 'react';
import App from './App';
import './index.css';
import ReactDOM from 'react-dom';
import reportWebVitals from './reportWebVitals';
import 'bootstrap/dist/css/bootstrap.min.css';
import { BrowserRouter, Route} from 'react-router-dom';
import UserForm from './components/account/login';


ReactDOM.render(
  <BrowserRouter>
      <div>
        <Route exact path='/' component={App}/>
        <Route path='/login' component={UserForm}/>
      </div>
      
  </BrowserRouter>
  ,
  document.getElementById('root')
);

reportWebVitals();
