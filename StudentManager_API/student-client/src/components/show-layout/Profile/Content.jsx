import React, { Component } from 'react';
import './../../../App.css'

export default class Profile extends Component {
    render() {
        return (
            <div className='content'>
                <img src='https://a.radikal.ru/a38/1909/b6/1450b7f77d80.png' alt='There is nothing special!'/>
                <div>
                    ava + desc
                </div>
                <div>
                    Others
                </div>
            </div>
        )
    }
}