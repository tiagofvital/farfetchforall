import React from "react";
import { useForm } from 'react-hook-form';
import PubSub from 'pubsub-js';
import * as EventTypes from '../constants/event-types';
import * as Controller from '../controllers/simulationController';
import * as Adapter from '../shared/adapters';

export default function FamilyAggregate() {
    const { register, handleSubmit } = useForm();

    const onSubmit = data => {
        var request = Adapter.transformAggregate(data);

        Controller.sendAggregate(request, (err, aggr) => {
            PubSub.publish(EventTypes.AGGREGATE_CHANGED);
            console.log(aggr);
        });
    };

    return (
        <form onSubmit={handleSubmit(onSubmit)}>

            <div class="form-group">
                <label>Annual Gain: </label>
                < input type="number" class="form-control" step="0.01" placeholder="40000" name="annualGain" ref={register({ required: true, maxLength: 80 })} />
            </div >
            <div class="form-group">

                <label>Family Coeficient: </label>
                <input type="number" class="form-control" placeholder="2" step="0.01" name="familyCoeficient" ref={register({ required: true, maxLength: 100 })} />
            </div>
            <div class="form-group">

                <label>Specific Deductions: </label>
                <input type="number" class="form-control" placeholder="1000" step="0.01" name="specificDeductions" ref={register({ required: true })} />
            </div>
            <div class="form-group">

                <label>Tax Payed: </label>
                <input type="number" class="form-control" placeholder="1000" step="0.01" name="taxPayed" ref={register({ required: true })} />
            </div>
            <div class="form-group">

                <label>Tax Deductions: </label>
                <input type="number" class="form-control" placeholder="1000" step="0.01" name="taxDeductions" ref={register({ required: true })} />

            </div>

            <input type="submit" className="btn btn-primary" />
        </form >
    );
}