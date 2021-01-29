import './App.css';
import React, { Component } from 'react';
import Header from './components/show-layout/Header/Header';
import Navbar from './components/show-layout/Navbar/Navbar';
import Profile from './components/show-layout/Profile/Content';
import { BrowserRouter, Route, Switch } from 'react-router-dom';
import UserForm from './components/account/Login';
import Test from './components/show-layout/Test/Test';

export default class App extends Component {
  render() {
    return (
      <div>
        <div className='app-wrapper'>
          <Header />
          <Navbar />
          <Route path='/profile' component={Profile} />
          <Route path='/test' component={Test}/>
          <Route />
        </div>
      </div>
    );
  }
}
