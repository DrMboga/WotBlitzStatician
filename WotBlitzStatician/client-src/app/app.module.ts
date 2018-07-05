import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';

import { AppComponent } from './components/app/app.component';
import { ComponentsModule } from './components/components.module';
import { ServicesModule } from './services/services.module';

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
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

