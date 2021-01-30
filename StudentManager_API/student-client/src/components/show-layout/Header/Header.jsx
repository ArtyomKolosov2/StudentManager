import React, { Component } from 'react';
import NavHeader from '../Navbar/Navbar';
import s from './Header.module.css';

export default class Header extends Component {
    render() {
        return (
            <header className={s.header}>
                <NavHeader />
            </header>
        )
    }
}