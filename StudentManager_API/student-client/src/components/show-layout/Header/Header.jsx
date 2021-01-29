import React, { Component } from 'react';
import s from './Header.module.css';

export default class Header extends Component {
    render() {
        return (
            <header className={s.header}>
                <img className={s.img} alt='no content' src='https://vraki.net/sites/default/files/test/fon_94.jpg' />
            </header>
        )
    }
}