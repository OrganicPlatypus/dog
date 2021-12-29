import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StartComponent } from './modules/start/start.component';
import { ToDoListComponent } from './modules/to-do-list/to-do-list.component';
import { JoinLandingGuard } from './services/guard/join-landing.guard';
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
   { path: 'join/:name',
    component: ToDoListComponent,
    canActivate: [JoinLandingGuard],
    data: {
      joinSessionRedirect: 'to-do'
    }
   },
  { path: '**',
  redirectTo: '',
  pathMatch: 'full' }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
