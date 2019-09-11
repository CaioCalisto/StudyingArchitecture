import React, { Component } from "react";
import ReactDOM from "react-dom";

class CarHire extends Component {
    constructor() {
        super();
      }

    render(){
        return (
            <div>
                <h2>Car Hire</h2>
            </div>
        );
    }
}

export default CarHire;
const wrapper = document.getElementById("carhire-body");
wrapper ? ReactDOM.render(<CarHire />, wrapper) : false;