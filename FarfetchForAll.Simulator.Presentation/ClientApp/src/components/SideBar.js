import React, { Component } from 'react';

import FamilyAggregate from './FamilyAggregate';
import ShareVesting from './ShareVesting';
import ShareSelling from './ShareSelling';

export class Sidebar extends Component {
    static displayName = Sidebar.name;

    constructor(props) {
        super(props);
    }
    render() {
        return (
            <nav>
                <div class="bd-toc-item" >
                    <a class="btn btn-secondary w-100 border-bottom-1" data-toggle="collapse" href="#familyAggregate" role="button" aria-expanded="false" aria-controls="familyAggregate">Aggregate Tax Info</a>
                    <ul class="nav bd-sidenav">
                        <li class="nav-item w-100 accordion">
                            <div class="card w-100 bg-dark text-white">
                                <div id="familyAggregate">
                                    <div class="card-body w-100">
                                        <FamilyAggregate />
                                    </div>
                                </div>
                            </div>
                        </li>
                    </ul>
                </div>
                <div class="bd-toc-item">
                    <a class="btn btn-secondary w-100 border-bottom-1" data-toggle="collapse" href="#shareVesting" role="button" aria-expanded="false" aria-controls="shareVesting">Vest</a>
                    <ul class="nav bd-sidenav">
                        <li class="nav-item w-100">
                            <div class="card  w-100 bg-dark text-white">
                                <div class="collapse" id="shareVesting">
                                    <div class=" card-body">
                                        <ShareVesting />
                                    </div>
                                </div>
                            </div>
                        </li>
                    </ul>
                </div>
                <div class="bd-toc-item">
                    <a class="btn btn-secondary w-100 border-bottom-1" data-toggle="collapse" href="#shareSelling" role="button" aria-expanded="false" aria-controls="shareSelling">Sell</a>
                    <ul class="nav bd-sidenav">
                        <li class="nav-item w-100">
                            <div class="card w-100 bg-dark text-white">
                                <div class="collapse" id="shareSelling">
                                    <div class=" card-body">
                                        <ShareSelling />
                                    </div>
                                </div>
                            </div>
                        </li>
                    </ul>
                </div>
            </nav>
        );
    }
}