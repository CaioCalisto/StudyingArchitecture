import React, { Component } from "react";
import ReactDOM from "react-dom";

class Customer extends Component {
    constructor() {
        super();
      }

    render(){
        return (
            <div>
                <h2>Customer Content</h2>
            </div>
        );
    }
}

export default Customer;
const wrapper = document.getElementById("customer-body");
wrapper ? ReactDOM.render(<Customer />, wrapper) : false;