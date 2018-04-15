import { NgModule } from '@angular/core';
import { ServerModule } from '@angular/platform-server';
import { AppModuleShared } from './app.shared.module';
import { AppComponent } from './components/app/app.component';

import { AccountsInfoService } from './accounts-info-service';


@NgModule({
    bootstrap: [ AppComponent ],
    imports: [
        ServerModule,
        AppModuleShared
	],
	providers: [
		AccountsInfoService
	]

})
export class AppModule {
}
