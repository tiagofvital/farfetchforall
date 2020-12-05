import React, { Component } from 'react';
import GlobalBalance from './GlobalBalance';
import YearResult from './YearResult';
import PubSub from 'pubsub-js';
import * as EventTypes from '../constants/event-types';

import * as Controller from '../controllers/simulationController';

export class SimulationResult extends Component {
    displayName = SimulationResult.name;

    subscriptions = [
        PubSub.subscribe(EventTypes.AGGREGATE_CHANGED, (msg, data) => {
            Controller.sendRun((result) => {
                this.setState({
                    results: result,
                    vestedShares: result.shares.totalCount
                });
            });
        }),
        PubSub.subscribe(EventTypes.SHARES_CHANGED, (msg, data) => {
            Controller.sendRun((result) => {
                this.setState({
                    results: result,
                    vestedShares: result.shares.totalCount
                });
            });
        })
    ];

    constructor(props) {
        super(props);
        this.state = { results: {} };
    }

    render() {
        let yearResults;

        if (this.state.results.yearResults != undefined) {
            yearResults = this.state.results.yearResults.map(r => <YearResult year={r.year} balance={r.sharesProfit} taxationDetails={r.taxResult} />);
        }
        else {
            yearResults = <span></span>
        }

        return (
            <div class="container-fluid">
                <GlobalBalance totalGains={this.state.results.totalGains} vestedShares={this.state.vestedShares} totalTaxes={this.state.totalTaxes} sharesIncome={this.state.sharesIncome} />
                {yearResults}
            </div>
        );
    }
}