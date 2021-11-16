import { NgModule } from '@angular/core';
import * as ApiServiceProxies from './service-proxy'

@NgModule({
  providers: [
    ApiServiceProxies.OrganizerServiceProxy

  ],
})
export class ServiceProxyModule { }
