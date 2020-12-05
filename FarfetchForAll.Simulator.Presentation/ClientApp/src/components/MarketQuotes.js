import React, { Component } from 'react';
import Loader from './Loader';

export class MarketQuotes extends Component {
    static displayName = MarketQuotes.name;

    constructor(props) {
        super(props);
        this.state = { quotes: [], loading: true };
    }

    componentDidMount() {
        this.fetchMarketQuotes();
    }

    static renderQuotes(quote) {
        return (
            <table class="pl-10 pr-10 text-white m-auto">
                <tbody>
                    <tr>
                        <td class="align-text-bottom pr-2"><h5>{quote.longName}</h5></td>
                        <td class="align-text-bottom pr-2"><h6>{quote.fullExchangeName}:{quote.symbol}</h6></td>
                        <td class="align-text-bottom font-weight-bold ">{quote.regularMarketPrice}</td>
                        <td class="align-text-bottom pr-2">{quote.currency}</td>
                        <td class="align-text-bottom pr-2 text-muted">After hours: {quote.postMarketPrice}</td>
                    </tr>
                </tbody>
            </table>
        );
    }

    render() {
        return (
            <div>
                <Loader height="40" loading={this.state.loading} content={MarketQuotes.renderQuotes(this.state.quotes)} />
            </div>
        );
    }

    async fetchMarketQuotes() {
        var myHeaders = new Headers();
        myHeaders.append('x-rapidapi-key', '93f7ddb1e3mshd84182ec1374bbfp1dd6dajsn39485d5e11ba');
        myHeaders.append('x-rapidapi-host', 'apidojo-yahoo-finance-v1.p.rapidapi.com');

        var requestOptions = {
            method: 'GET',
            headers: myHeaders
        };

        fetch('https://apidojo-yahoo-finance-v1.p.rapidapi.com/market/v2/get-quotes?symbols=FTCH&region=US', requestOptions)
            .then(response => response.json())
            .then(data => {
                console.log(JSON.stringify(data));

                this.setState({ quotes: data.quoteResponse.result[0], loading: false });
            });
    }
}