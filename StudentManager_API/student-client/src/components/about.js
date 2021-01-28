import React from 'react';

export class About extends React.Component{
    constructor (){
        super();
        this.state = {someText: "Some about boring text!"}
        this.clicked = this.clicked.bind(this);
    }
    clicked(){
        this.setState({someText : "Clicked!"});
    }

    render(){
    return <div>
            <p>This is a simple example of a React component.</p>
            <p aria-live="polite">Current text: <strong>{this.state.someText}</strong></p>
            <button className="btn btn-danger" onClick={this.clicked} />
           </div>
    }
}