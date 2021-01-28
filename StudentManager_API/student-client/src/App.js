import './App.css';
import React, {Component} from 'react';
import { Route } from 'react-router';
import { About } from './components/about';

export default class App extends Component {
  render (){ return (
    <div className="App">
      <Route path='/about' component={About} />
      <header className="App-header">
        
      </header>
    </div>
  );
  }
}
