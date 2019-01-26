import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'romanNumber'
})
export class RomanNumberPipe implements PipeTransform {

  private romanNumerals: string[] = ["M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I"]
  private numerals: number[] = [1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1];

  transform(value: number): string {
    if (value <= 0) {
      return value.toString();
    }

    var result: string = '';

    while(value > 0) {
      // find biggest numeral that is less than equal to number
      var index = this.numerals.findIndex(n => n <= value);

      // subtract it's value from your number
      value -= this.numerals[index];

      // tack it onto the end of your roman numeral
      result = result.concat(this.romanNumerals[index]);
    }

    return result;
  }

}
