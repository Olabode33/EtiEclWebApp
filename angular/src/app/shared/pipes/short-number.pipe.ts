import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'shortNumber'
})
export class ShortNumberPipe implements PipeTransform {

    transform(value: any, args?: any): any {
        let exp;
        const suffixes = ['K', 'M', 'B', 'T', 'P', 'E'];
        const isNegativeValues = value < 0;

        if (Number.isNaN(value) || (value < 1000 && value >= 0) || (value < 0 && value > -1000) || !this.isNumeric(value)) {
            if (!!args && this.isNumeric(value) && !(value < 0) && value !== 0) {
                return value.toFixed(args);
            } else {
                return value;
            }
        }

        if (!isNegativeValues) {
            exp = Math.floor(Math.log(value) / Math.log(1000));

            return (value / Math.pow(1000, exp)).toFixed(args) + suffixes[exp - 1];
        } else {
            value = value * -1;

            exp = Math.floor(Math.log(value) / Math.log(1000));

            return (value * -1 / Math.pow(1000, exp)).toFixed(args) + suffixes[exp - 1];
        }

    }

    isNumeric(value): boolean {
        if (value < 0) { value = value * -1; }
        if (/^-{0,1}\d+$/.test(value)) {
            return true;
        } else if (/^\d+\.\d+$/.test(value)) {
            return true;
        } else {
            return false;
        }
    }

    // https://medium.com/@nimishgoel056/display-number-in-billion-million-thousand-using-custom-pipe-in-angular-b95bf388350a

}
