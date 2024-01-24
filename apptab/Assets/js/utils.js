function formatCurrency(input, floatSeparator) {
    let res = '';

    const stringParts = input.split(floatSeparator);

    const index = stringParts[0].length % 3;

    const re = /[\d]{3}/gi;

    const substring = stringParts[0].substring(index, input.length);

    const tmp = [...substring.matchAll(re)];

    const arr = tmp.map(x => {
        return x[0];
    });

    for (let i = 0; i < arr.length; i += 1) {
        if (i > 0) {
            res += ' ';
        }

        res += arr[i];
    }

    const x = stringParts[0].substring(0, index);

    if (res === '') {
        return stringParts.length === 1 ? x : x.concat(floatSeparator).concat(stringParts[1]);
    }

    if (x === '') {
        return stringParts.length === 1 ? res : res.concat(floatSeparator).concat(stringParts[1]);
    }

    return stringParts.length === 1 ? x.concat(' ').concat(res) : x.concat(' ').concat(res).concat(floatSeparator).concat(stringParts[1]);
}
