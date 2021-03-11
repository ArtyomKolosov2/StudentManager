import React, { Component } from 'react';
import { Link, NavLink, withRouter } from 'react-router-dom';
import { Nav } from 'react-bootstrap';
import s from './NavMenu.module.css'


class NavMenu extends Component {
    render() {
        return (
            <Nav defaultActiveKey="/home" className={`flex-column ${s.nav}`}>
                <NavLink to="/profile" className={s.item} activeClassName={s.activeLink} >profile</NavLink>
                <NavLink to="/test" activeClassName={s.activeLink}>test</NavLink>
            </Nav>
        )
    }
}

export default withRouter(NavMenu);