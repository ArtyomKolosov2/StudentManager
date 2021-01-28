import './App.css';
import React, { Component } from 'react';
import Header from './components/show-layout/Header/header';
import Navbar from './components/show-layout/Navbar/navbar';
import Profile from './components/show-layout/Profile/content';
import { Route } from 'react-router-dom';


export default class App extends Component {
  render() {
    return (
      <div className='app-wrapper'>    
        <Header></Header>
        <Navbar></Navbar>
        <div>
          <Route path='/profile' component={Profile}/>
        </div>
      </div>
    );
  }
}
