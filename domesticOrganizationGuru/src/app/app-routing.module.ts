import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StartComponent } from './modules/start/start.component';
import { ToDoListComponent } from './modules/to-do-list/to-do-list.component';
import { NoteNameGuard } from './services/guard/note-name.guard';

const routes: Routes = [
  { path: '',
    component: StartComponent
  },
  { path: 'to-do',
    component: ToDoListComponent,
    canActivate: [NoteNameGuard],
    data: {
      noteNameGuardRedirect: '',
    }
   },
  { path: '**',
  redirectTo: '',
  canActivate: [NoteNameGuard],
  pathMatch: 'full' }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
