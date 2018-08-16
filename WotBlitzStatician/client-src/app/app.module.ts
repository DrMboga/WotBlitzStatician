import { BrowserModule } from '@angular/platform-browser';
import { NgModule, LOCALE_ID} from '@angular/core';
import { HttpModule } from '@angular/http';

import { registerLocaleData } from '@angular/common';
import localeRu from '@angular/common/locales/ru';

import { AppComponent } from './components/app/app.component';
import { ComponentsModule } from './components/components.module';
import { ServicesModule } from './services/services.module';

registerLocaleData(localeRu);

@NgModule({
  declarations: [
  ],
  imports: [
    BrowserModule,
    HttpModule,
    ComponentsModule,
    ServicesModule
  ],
  providers: [
    { provide: LOCALE_ID, useValue: 'ru' }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

