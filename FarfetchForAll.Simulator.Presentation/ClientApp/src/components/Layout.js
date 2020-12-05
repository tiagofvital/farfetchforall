import React, { Component } from 'react';
import { NavMenu } from './NavMenu';
import { Sidebar } from './SideBar';
import { SimulationResult } from './SimulationResult';

export class Layout extends Component {
    static displayName = Layout.name;

    render() {
        return (
            <div class="h-100">
                <NavMenu />
                <div class="container-fluid h-100">
                    <div class="row h-100">
                        <div class="bg-secondary col-2 h-100 p-0 text-dark">
                            <Sidebar />
                        </div>
                        <div class="col-8">
                            <div class="row m-4">
                                <SimulationResult />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}