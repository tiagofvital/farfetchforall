import React, { Component } from 'react';
import { FormattedNumber, IntlProvider } from 'react-intl';

export default class GlobalBalance extends Component {
    static displayName = GlobalBalance.name;

    constructor(props) {
        super(props);
    }

    render() {
        let currency = "EUR";

        let content, banner;

        if (this.props.totalGains == undefined) {
            content = <img class="rounded mx-auto d-block" src="https://media.giphy.com/media/MCWy58eDqChisd5Dp7/giphy.gif" />
        }
        else {
            if (this.props.totalGains < 0) {
                banner = <span><img class="rounded mx-auto d-block" height="120" src="https://media.giphy.com/media/ddQknQgNTkrYN83DZF/giphy.gif" /></span>
            }
            else if (this.props.totalGains < 5000) {
                banner = <span>
                    <img class="rounded mx-auto d-block" height="120" src="https://media.giphy.com/media/QVm8h1Rx5AUxg0X7ku/giphy.gif" />
                    <img class="rounded mx-auto d-block" height="120" src="https://media.giphy.com/media/Ca01L7FtrysqK5OJVl/giphy.gif" />
                </span>
            }
            else if (this.props.totalGains < 15000) {
                banner = <span><img class="rounded mx-auto d-block" height="120" src="https://media.giphy.com/media/XBRhvMps2n9TgUDZi0/giphy.gif" />
                    <img class="rounded mx-auto d-block" height="120" src="https://media.giphy.com/media/VbtB71uYYChnZjGa6Y/giphy.gif" /></span>
            }
            else if (this.props.totalGains < 100000) {
                banner = <img class="rounded mx-auto d-block" height="120" src="https://media.giphy.com/media/WsXDQcxt1puwQdycgj/giphy.gif" />
            }
            else if (this.props.totalGains < 200000) {
                banner = <img class="rounded mx-auto d-block" height="120" src="https://media.giphy.com/media/XHwP5GpcTj6KLiYCNB/giphy.gif" />
            }
            else if (this.props.totalGains < 300000) {
                banner = <div class="d-flex justify-content-center"><img height="120" src="https://media.giphy.com/media/lok9BsSpkVx3outqi3/giphy.gif" />
                    <img height="120" src="https://media.giphy.com/media/gJnm8xJ4McEgs9MBEx/giphy.gif" />
                    <img height="120" src="https://media.giphy.com/media/lMgPCmzonsD4d1E0I0/giphy.gif" />
                    <img height="120" src="https://media.giphy.com/media/XyIsQzFMzZdmDoot9H/giphy.gif" />
                </div>
            }
            else {
                banner = <img class="rounded mx-auto d-block" src="https://media.giphy.com/media/l396JAJ3UH2ikuije/giphy.gif" />
            }

            content = <div class="container">
                {banner}
                <p>
                    <span class="mr-4">Balance: <FormattedNumber
                        value={this.props.totalGains}
                        style="currency"
                        currency={currency}
                        maximumFractionDigits="2" />
                    </span>

                </p>
                <p>Shares Income: {this.props.sharesIncome}</p>
                <p>Taxes: {this.props.totalTaxes}</p>
                <p>Vested Shares: {this.props.vestedShares}</p>
            </div>
        }

        return (

            <IntlProvider locale="pt-PT">
                {content}
            </IntlProvider>
        );
    }
}