export function transformAggregate(data) {
    var tdata = {
        annualGain: parseInt(data.annualGain, 10),
        familyCoeficient: parseInt(data.familyCoeficient, 10),
        taxPayed: parseInt(data.taxPayed, 10),
        specificDeductions: parseInt(data.specificDeductions, 10),
        taxDeductions: parseInt(data.taxDeductions, 10)
    };

    return JSON.stringify(tdata);
};

export function transformVest(data) {
    var tdata = {
        amount: parseInt(data.amount, 10),
        shareValue: parseInt(data.shareValue, 10),
        exerciseCost: parseInt(data.exerciseCost, 10),
        year: parseInt(data.year, 10)
    };

    return JSON.stringify(tdata);
};

export function transformSell(data) {
    var tdata = {
        amount: parseInt(data.amount, 10),
        shareValue: parseInt(data.shareValue, 10),
        year: parseInt(data.year, 10)
    };

    return JSON.stringify(tdata);
};