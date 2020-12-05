import React from 'react';
import { useForm } from 'react-hook-form';
import PubSub from 'pubsub-js';
import * as EventTypes from '../constants/event-types';
import * as Controller from '../controllers/simulationController';
import * as Adapter from '../shared/adapters';

export default function ShareVesting() {
    const { register, handleSubmit } = useForm();

    const onSubmit = data => {
        var request = Adapter.transformVest(data);

        Controller.vest(request, (err, value) => {
            PubSub.publish(EventTypes.SHARES_CHANGED, value);
            console.log(err);
        });
    };

    return (
        <form onSubmit={handleSubmit(onSubmit)}>
            <div class="form-group">
                <label>Amount: </label>
                <input class="form-control" type="number" placeholder="1000" name="amount" ref={register({ required: true, maxLength: 80 })} />
            </div>
            <div class="form-group">

                <label>Market Value (EUR): </label>
                <input type="number" class="form-control" step="0.01" placeholder="50" name="shareValue" ref={register({ required: true, maxLength: 100 })} />
            </div>
            <div class="form-group">

                <label>Exercise price (EUR): </label>
                <input type="number" class="form-control" step="0.01" placeholder="10.82" name="exerciseCost" ref={register({ required: true })} />
            </div>
            <div class="form-group">

                <label>Year: </label>
                <input type="number" class="form-control" placeholder="1" name="year" ref={register({ required: true })} />
            </div>

            <input type="submit" className="btn btn-primary" />
        </form>
    );
}