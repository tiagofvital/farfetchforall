import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import PubSub from 'pubsub-js';
import * as EventTypes from '../constants/event-types';
import * as Controller from '../controllers/simulationController';
import { MarketQuotes } from './MarketQuotes';
import './NavMenu.css';

export class NavMenu extends Component {
    static displayName = NavMenu.name;

    constructor(props) {
        super(props);

        this.toggleNavbar = this.toggleNavbar.bind(this);
        this.state = {
            collapsed: true
        };
    }

    handleUndo() {
        Controller.undo((err, value) => {
            PubSub.publish(EventTypes.SHARES_CHANGED, value);
            console.log(err);
        });
    }

    handleClear() {
        Controller.clear((err, value) => {
            console.log('publishing event..');
            PubSub.publish(EventTypes.SHARES_CHANGED, value);
            console.log(err);
        });
    }

    toggleNavbar() {
        this.setState({
            collapsed: !this.state.collapsed
        });
    }

    render() {
        return (
            <header>
                <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-0" light>
                    <div class="ml-0 mr-0 float-left container-fluid h-100">
                        <div class="row w-100 text-center">
                            <div class="col text-center">
                                <NavbarBrand tag={Link} to="/">
                                    <span class=" text-white mr-4">Farfetch For All</span>
                                    <img height="40" src="https://media.giphy.com/media/9d8QrFmgC21ry/giphy.gif" />
                                </NavbarBrand>
                                <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
                                <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
                                </Collapse>
                            </div>
                            <div class="col-8">
                                <MarketQuotes className="m-auto" />
                            </div>
                            <div class="col text-primary">
                                <button class="btn btn-dark" onClick={this.handleUndo}>Undo</button>
                                <button class="btn btn-dark ml-3" onClick={this.handleClear}>Clear</button>
                            </div>
                        </div>
                    </div>
                </Navbar >
            </header >
        );
    }
}