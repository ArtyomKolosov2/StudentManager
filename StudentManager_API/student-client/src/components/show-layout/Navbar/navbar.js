import React, { Component } from 'react';
import { NavLink } from 'react-router-dom';
import './../../../App.css'

export default class Navbar extends Component {
    render() {
        return (
            <nav className='nav'>
                <div>
                    <NavLink to='/profile'>profile</NavLink>
                </div>
                <div>
                    <NavLink to='/login'>login</NavLink>
                </div>
            </nav>
        )
    }
}