import React, {Component} from 'react';

export default class UserForm extends Component {
    constructor(props) {
        super(props);
        this.state = { name: "" };

        this.onChange = this.onChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }
    onChange(e) {
        var val = e.target.value;
        this.setState({ name: val });
    }

    handleSubmit(e) {
        e.preventDefault();
        alert("Имя: " + this.state.name);
    }

    render() {
        return (
            <form onSubmit={this.handleSubmit}>
                <p>
                    <label>Имя:</label><br />
                    <input type="text" value={this.state.name} onChange={this.onChange} />
                </p>
                <input type="submit" value="Отправить" />
            </form>
        );
    }
}