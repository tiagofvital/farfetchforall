function send(url, request, callback) {
    return fetch(url, request)
        .then((response) => response.json())
        .then((result) => {
            if (result == undefined) {
                callback(null, []);
            } else {
                callback(null, result);
            }
        }).catch(err => {
            callback(err);
        })
};

export function sendAggregate(value, callback) {
    var url = 'https://localhost:44378/api/simulations';

    var requestOptions = {
        mode: 'same-origin',
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: value
    };

    return fetch(url, requestOptions)
        .then((response) => response.json())
        .then((aggr) => {
            if (aggr == undefined) {
                callback(null, []);
            } else {
                callback(null, aggr);
            }
        }).catch(err => {
            callback(err);
        })
};

export function sendRun(callback) {
    var url = 'https://localhost:44378/api/simulations/1/run';

    var requestOptions = {
        mode: 'same-origin',
        method: 'POST',
        headers: { 'Content-Type': 'application/json' }
    };

    send(url, requestOptions, (err, result) => {
        if (result != undefined) {
            console.log(result);

            callback(result);
        }
    })
};

export function vest(request, callback) {
    var url = 'https://localhost:44378/api/simulations/1/vest';

    var requestOptions = {
        mode: 'same-origin',
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: request
    };

    send(url, requestOptions, (err, result) => {
        if (result != undefined) {
            console.log(result);

            callback(result);
        }
    })
};

export function sell(request, callback) {
    var url = 'https://localhost:44378/api/simulations/1/sell';

    var requestOptions = {
        mode: 'same-origin',
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: request
    };

    send(url, requestOptions, (err, result) => {
        if (result != undefined) {
            console.log(result);

            callback(result);
        }
    })
};

export function undo(callback) {
    var url = 'https://localhost:44378/api/simulations/1/undo';

    var requestOptions = {
        mode: 'same-origin',
        method: 'POST',
        headers: { 'Content-Type': 'application/json' }
    };

    send(url, requestOptions, (err, result) => {
        if (result != undefined) {
            console.log(result);

            callback(result);
        }
    })
};

export function clear(callback) {
    var url = 'https://localhost:44378/api/simulations/1/clear';

    var requestOptions = {
        mode: 'same-origin',
        method: 'POST',
        headers: { 'Content-Type': 'application/json' }
    };

    send(url, requestOptions, (err, result) => {
        if (result != undefined) {
            console.log(result);

            callback(result);
        }
    })
};