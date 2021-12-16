import { NgModule } from '@angular/core';
import * as ApiServiceProxies from './service-proxy'

@NgModule({
  providers: [
    ApiServiceProxies.Client
  ],
})
export class ServiceProxyModule { }
