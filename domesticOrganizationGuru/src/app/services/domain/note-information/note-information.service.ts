import { NotePointsState } from './../../../state/states/notes/notes.inteface';
import { Injectable, enableProdMode } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Store } from '@ngrx/store';
import { getExpirationDateSelector, getMinutesTillExpireSelector } from 'src/app/state/states/settings/settings.selector';

@Injectable({
  providedIn: 'root'
})
export class NoteInformationService {
  expiriationTimeMinutes: number = 0;
  expiriationTimeSeconds: number = 0;
  expirationDate: string = '';
  private expiriationTimeMinutesSpan = new BehaviorSubject<number>(this.expiriationTimeMinutes);
  private expiriationTimeSecondsSpan = new BehaviorSubject<number>(this.expiriationTimeMinutes);
  private expirationDateFormed = new BehaviorSubject<string>(this.expirationDate);


  constructor(private store: Store<NotePointsState>) {
    let intervalId = setInterval(() => {
      let timeSpan = 0;
      let minutes = 0
      let seconds = 0
      this.store.select(getExpirationDateSelector)
      .subscribe(noteExpirationDate => {
        if(noteExpirationDate){
          const start = new Date().getTime();
          const end = new Date(noteExpirationDate).getTime();
          let difference = end - start;
          minutes = Math.floor(difference / 60000);
          seconds = Math.floor(difference / 1000 % 60);
          this.expiriationTimeMinutes = minutes;
          this.expiriationTimeMinutesSpan.next(minutes);
          this.expiriationTimeSeconds = seconds;
          this.expiriationTimeSecondsSpan.next(seconds);
          timeSpan = 60* minutes + seconds;
        }
      })
      if (--timeSpan < 0) {
        clearInterval(intervalId)
      }
    }, 1000);
  }

  public getExpirationTimeMinutes = () => {
    this.store.select(getExpirationDateSelector)
    .subscribe(noteExpirationDate => {
      if(noteExpirationDate){
        const start = new Date().getTime();
        const end = new Date(noteExpirationDate).getTime();
        const expiriationSpan = end - start;
        const minutes = Math.floor(expiriationSpan / 60000);
        this.expiriationTimeMinutes = minutes;
        this.expiriationTimeMinutesSpan.next(minutes);
        }
      })
    return this.expiriationTimeMinutesSpan;
  }

  public getExpirationTimeSeconds = () => {
    this.store.select(getExpirationDateSelector)
    .subscribe(noteExpirationDate => {
      if(noteExpirationDate){
        const expiriationSpan = this.expiriationSpan(noteExpirationDate);
        const seconds = Math.floor(expiriationSpan / 1000 % 60);
        this.expiriationTimeSeconds = seconds;
        this.expiriationTimeSecondsSpan.next(seconds);
        }
      })
    return this.expiriationTimeSecondsSpan;
  }

  public getExpirationDate = () => {
      this.store.select(getExpirationDateSelector)
      .subscribe(noteExpirationDate => {
        if(noteExpirationDate){
          this.expirationDate = `${noteExpirationDate.toLocaleDateString()} ${noteExpirationDate.toLocaleTimeString()}`;
          this.expirationDateFormed.next(this.expirationDate);
        }
      })
      return this.expirationDateFormed;
    }

  private expiriationSpan(noteExpirationDate: Date) {
    const start = new Date().getTime();
    const end = new Date(noteExpirationDate).getTime();
    const expiriationSpan = end - start;
    return expiriationSpan;
  }
}
