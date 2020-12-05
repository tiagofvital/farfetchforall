import React, { Component } from 'react';

export default class YearResult extends Component {
    static displayName = YearResult.name;

    constructor(props) {
        super(props);
    }

    render() {
        let taxationParcels;

        taxationParcels = <tr><td>{JSON.stringify(this.props.taxationDetails)}</td></tr>

        return (
            <div class="container">
                <p><span>Year: {this.props.year} | Balance: </span><span class="ml-2 bold">{this.props.balance}</span></p>
                <table cellPadding="2" class="table table-bordered table-striped text-light">
                    <tbody>
                        <tr>Tax Settlement: {this.props.taxationDetails.taxSettling} &euro;</tr>
                        {taxationParcels}
                    </tbody>
                </table>
            </div>
        );
    }
}