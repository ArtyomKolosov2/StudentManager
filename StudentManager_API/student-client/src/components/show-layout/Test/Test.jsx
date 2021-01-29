import React from 'react';

export default class Test extends React.Component{
    constructor(props) {
        super(props);
        this.state = { testData: [], loading: true };
    }
    componentDidMount(){
        this.getTestData();
    }
    
    render(){
        let content = this.state.loading
        ? <p><em>Loading...</em></p>
        : Test.renderTestList(this.state.testData);

        return(
            <div>
                <h1 id="strings">Test string list</h1>
                {content}
            </div>
        );
    }

    static renderTestList(testData){
        return(
            <ul>
                {testData.map(data => <li>{data}</li>)}
            </ul>
        );
    }

    async getTestData(){
        const response = await fetch('api/Test/test_arr');
        if (response.ok === true){
            const data = await response.json();
            this.setState({testData: data, loading:false});
        }
    }
}