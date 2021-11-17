import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StartComponent } from './modules/start/start.component';
import { ToDoListComponent } from './modules/to-do-list/to-do-list.component';

const routes: Routes = [
  // { path: '', component: ShellComponent},
  { path: '', component: StartComponent },
  { path: 'to-do', component: ToDoListComponent },
  { path: '**',
  redirectTo: '',
  //ToDo: canActivate: [NoteNameGuard]
  pathMatch: 'full' }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
