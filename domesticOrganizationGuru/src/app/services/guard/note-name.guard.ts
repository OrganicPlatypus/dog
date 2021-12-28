import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { NoteSettingsState } from 'src/app/state/states/settings/settings.inteface';
import { getNoteNameSelector } from 'src/app/state/states/settings/settings.selector';

@Injectable()
export class NoteNameGuard implements CanActivate {

constructor(
  private router: Router,
  private store: Store<NoteSettingsState>
) {
 }

canActivate(routeSnapshot: ActivatedRouteSnapshot) {
  let redirect = routeSnapshot.data['noteNameGuardRedirect'];
  let isThereAName = false;

  this.store.select(getNoteNameSelector)
    .subscribe(
      noteName => {
        if(noteName && noteName !== '') {
          isThereAName = true;
        }
        else {
          this.router.navigate([redirect])
        }
      })
    return isThereAName;
  }

}
