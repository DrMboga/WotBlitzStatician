import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IconsModule } from './icons.module';
import { BlitzColorScaleDirective } from './pipes/blitz-color-scale.directive';
import { RomanNumberPipe } from './pipes/roman-number.pipe';

@NgModule({
  imports: [
    CommonModule,

  ],
  exports: [
    IconsModule,
    BlitzColorScaleDirective,
    RomanNumberPipe,

  ],
  declarations: [
    BlitzColorScaleDirective,
    RomanNumberPipe,

  ],
  providers: [
  ],
})
export class SharedComponentModule { }
