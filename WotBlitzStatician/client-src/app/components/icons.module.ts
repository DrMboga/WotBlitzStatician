import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { library } from '@fortawesome/fontawesome-svg-core';
import { faUser, faGamepad, faFlag, 
        faSkullCrossbones, faSearch,
        faSignInAlt, faSignOutAlt, faCross,
        faCalculator, faDonate, faCoins,
        faStarHalfAlt, faJedi,
        faCampground, faWarehouse } from '@fortawesome/free-solid-svg-icons';
import { faClock, faStar, faHeart } from '@fortawesome/free-regular-svg-icons';
 
// Add an icon to the library for convenient access in other components
library.add(faUser, 
            faClock, 
            faGamepad, 
            faFlag, 
            faSkullCrossbones, 
            faStar, 
            faHeart,
            faSearch,
            faSignInAlt,
            faSignOutAlt,
            faCross,
            faCalculator,
            faDonate,
            faCoins,
            faStarHalfAlt,
            faJedi,
            faCampground,
            faWarehouse);

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
