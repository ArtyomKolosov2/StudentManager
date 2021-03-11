import React, {Component} from 'react';
import './../../App.css';

export default class UserForm extends Component {
    constructor(props) {
        super(props);
        this.state = { name: "", password: "" };

        this.onChangeLogin = this.onChangeLogin.bind(this);
        this.onChangePassword = this.onChangePassword.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }
    onChangeLogin(e) {
        var val = e.target.value;
        this.setState({ name: val });
    }

    onChangePassword(e) {
        var val = e.target.value;
        this.setState({ password: val });
    }

    handleSubmit(e) {
        e.preventDefault();
        alert("Login: " + this.state.name + "\nPassword: " + this.state.password);
    }

    render() {
        return (
            <form onSubmit={this.handleSubmit} className='content'>
                <p>
                    <label>Login:</label><br />
                    <input type="text" value={this.state.name} onChange={this.onChangeLogin} />
                    <br /><label>Password:</label><br />
                    <input type="password" value={this.state.password} onChange={this.onChangePassword} />
                </p>
                <input type="submit" value="Отправить" />
            </form>
        );
    }
}