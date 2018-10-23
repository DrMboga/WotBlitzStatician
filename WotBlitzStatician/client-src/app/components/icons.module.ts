import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { library } from '@fortawesome/fontawesome-svg-core';
import { faUser, faClock, } from '@fortawesome/free-solid-svg-icons';
 
// Add an icon to the library for convenient access in other components
library.add(faUser, faClock);

@NgModule({
  imports: [
    CommonModule,
    FontAwesomeModule
  ],
  exports: [
    FontAwesomeModule
  ],
  declarations: []
})
export class IconsModule { }
