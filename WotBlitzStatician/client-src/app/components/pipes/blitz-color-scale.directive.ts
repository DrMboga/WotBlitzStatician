import { Directive, ElementRef, Renderer2, Input } from '@angular/core';

@Directive({
  selector: '[appBlitzColorScale]'
})
export class BlitzColorScaleDirective {
  private _scaleValue: number;
  private _scaleClassColors: ScaleColor[];

  constructor(private renderer: Renderer2, private element: ElementRef) {
    this._scaleClassColors = new Array(
      /*Winrate*/
      { scaleMinValue: 0, scaleMaxValue: 0.4499, scaleClass: 'scale-very-bad' },
      { scaleMinValue: 0.45, scaleMaxValue: 0.4699, scaleClass: 'scale-bad' },
      {
        scaleMinValue: 0.47,
        scaleMaxValue: 0.4899,
        scaleClass: 'scale-below-average'
      },
      {
        scaleMinValue: 0.49,
        scaleMaxValue: 0.5199,
        scaleClass: 'scale-average'
      },
      { scaleMinValue: 0.52, scaleMaxValue: 0.5399, scaleClass: 'scale-good' },
      {
        scaleMinValue: 0.54,
        scaleMaxValue: 0.5599,
        scaleClass: 'scale-very-good'
      },
      { scaleMinValue: 0.56, scaleMaxValue: 0.5999, scaleClass: 'scale-great' },
      { scaleMinValue: 0.6, scaleMaxValue: 0.6499, scaleClass: 'scale-unicum' },
      {
        scaleMinValue: 0.65,
        scaleMaxValue: 1,
        scaleClass: 'scale-super-unicum'
      },
      /*Wn7*/
      { scaleMinValue: 1.1, scaleMaxValue: 500, scaleClass: 'scale-very-bad' },
      { scaleMinValue: 500, scaleMaxValue: 699, scaleClass: 'scale-bad' },
      {
        scaleMinValue: 700,
        scaleMaxValue: 899,
        scaleClass: 'scale-below-average'
      },
      { scaleMinValue: 900, scaleMaxValue: 1099, scaleClass: 'scale-average' },
      { scaleMinValue: 1100, scaleMaxValue: 1349, scaleClass: 'scale-good' },
      {
        scaleMinValue: 1350,
        scaleMaxValue: 1549,
        scaleClass: 'scale-very-good'
      },
      { scaleMinValue: 1550, scaleMaxValue: 1849, scaleClass: 'scale-great' },
      { scaleMinValue: 1850, scaleMaxValue: 2049, scaleClass: 'scale-unicum' },
      {
        scaleMinValue: 2050,
        scaleMaxValue: 10000,
        scaleClass: 'scale-super-unicum'
      }
    );
  }

  @Input() inverse: boolean;

  @Input('appBlitzColorScale')
  set scaleValue(value: number) {
    this._scaleValue = value;
    for (const scaleClassColor of this._scaleClassColors) {
      if (
        value >= scaleClassColor.scaleMinValue &&
        value <= scaleClassColor.scaleMaxValue
      ) {
        this.renderer.addClass(
          this.element.nativeElement,
          `${scaleClassColor.scaleClass}${this.inverse ? '-inverse' : ''}`
        );
        return;
      }
    }
  }

  get scaleValue(): number {
    return this._scaleValue;
  }
}

class ScaleColor {
  public scaleMinValue: number;
  public scaleMaxValue: number;
  public scaleClass: string;
}
