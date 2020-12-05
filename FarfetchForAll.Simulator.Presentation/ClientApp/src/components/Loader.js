import React, { Component } from 'react';

export default class Loader extends Component {
    static displayName = Loader.name;

    constructor(props) {
        super(props);
    }

    render() {
        let contents = this.props.loading
            ? <img height={this.props.height} src="https://media.giphy.com/media/CICgeSq0I8Jg7HmjIp/giphy.gif" alt="Loading..." />
            : this.props.content

        return (
            <div>
                {contents}
            </div>
        );
    }
}