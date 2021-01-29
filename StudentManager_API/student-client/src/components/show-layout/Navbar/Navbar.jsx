import React, { Component } from 'react';
import {NavLink } from 'react-router-dom';
import s from './Navbar.module.css';

export default class Navbar extends Component {
    render() {
        return (
            <nav className={s.nav}>
                <div>
                    <NavLink to='/profile'>Profile</NavLink>
                </div>
                <div>
                    <NavLink to='/login'>Login</NavLink>
                </div>
                <div>
                    <NavLink to='/test'>Test</NavLink>
                </div>
            </nav>
        )
    }
}