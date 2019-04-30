import { BrowserModule } from '@angular/platform-browser';
import { NgModule, LOCALE_ID} from '@angular/core';
import { DatePipe, CommonModule } from '@angular/common';

import { registerLocaleData } from '@angular/common';
import localeRu from '@angular/common/locales/ru';

import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';

import { AppComponent } from './app.component';
import { getBaseUrl } from './services/services.module';
import { RomanNumberPipe } from './shared/pipes/roman-number.pipe';
import { AppRoutingModule } from './app-routing.module';
import { AuthGuard } from './auth.guard';
import { BlitzStaticianService } from './shared/services/blitz-statician.service';
import { AccountAuthenticationService } from './shared/services/account-authentication.service';
import { AccountInfoService } from './shared/services/account-info.service';
import { FormsModule } from '@angular/forms';
import { CookieService } from 'ngx-cookie-service';
import { SharedComponentModule } from './shared/shared-component.module';
import { NavMenuComponent } from './home/nav-menu/nav-menu.component';
import { SplashScreenComponent } from './home/splash-screen/splash-screen.component';
import { AccountSearchComponent } from './home/account-search/account-search.component';
import { HttpClientModule } from '@angular/common/http';
import { appReducer } from './state/app.reducer';

registerLocaleData(localeRu);

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    SplashScreenComponent,
    AccountSearchComponent

  ],
  imports: [
    BrowserModule,
    FormsModule,
    CommonModule,
    SharedComponentModule,
    AppRoutingModule,
    HttpClientModule,
    StoreModule.forRoot({}),
    StoreModule.forFeature('homeState', appReducer),
    StoreDevtoolsModule.instrument({
      name: 'WotblitzStatician App DevTools',
      maxAge: 25,
      // logOnly: environment.production,
    }),
    EffectsModule.forRoot([])
  ],
  providers: [
    DatePipe,
    { provide: LOCALE_ID, useValue: 'ru' },
    RomanNumberPipe,
    // {provide: AccountGlobalInfo, useValue: new AccountGlobalInfo(90277267, 'DummyAccount') }, // ToDo: Change dummy data later
    AuthGuard,
    CookieService,
    BlitzStaticianService,

    AccountAuthenticationService, // ToDo: Obsolete
    AccountInfoService, // ToDo: Obsolete
    {
      provide: 'BASE_URL', useFactory: getBaseUrl
    },

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

