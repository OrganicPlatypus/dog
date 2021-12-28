import { NgModule } from '@angular/core';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatListModule } from '@angular/material/list';

@NgModule({
  declarations: [],
  imports: [
    MatToolbarModule,
    MatListModule
  ],
  exports: [
    MatToolbarModule,
    MatListModule,
  ]
})
export class MaterialModule { }
