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
    this.expiriationTimeMinutes = this.expiriationTimeMinutes- 1;
    this.expiriationTimeMinutesSpan.next(this.expiriationTimeMinutes);
    if(this.expiriationTimeMinutes === 0) clearInterval(intervalId)
  }, 60000)
 }


public getExpirationTimeMinutes = () => {
  this.store.select(getExpirationDateSelector)
  .subscribe(noteExpirationDate => {
    if(noteExpirationDate){
      console.log('getExpirationTimeMinutes noteExpirationDate', noteExpirationDate)
      const start = new Date().getTime();
      const end = new Date(noteExpirationDate).getTime();
      const diff = end - start;
      const minutes = Math.floor(diff / 60000);
      const seconds = Math.floor(diff / 1000 % 60);
      this.expiriationTimeMinutes = minutes;
      this.expiriationTimeMinutesSpan.next(minutes);
      }
    })
  console.log('this.expiriationTimeMinutesSpan', this.expiriationTimeMinutesSpan.value)
  return this.expiriationTimeMinutesSpan;
}

public getExpirationDate = () => {
    this.store.select(getExpirationDateSelector)
    .subscribe(noteExpirationDate => {
      if(noteExpirationDate){
        const start = new Date().getTime();
        const end = new Date(noteExpirationDate).getTime();
        const diff = end - start;
        const minutes = Math.floor(diff / 60000);
        const seconds = Math.floor(diff / 1000 % 60);
        this.expiriationTimeMinutes = minutes;
        this.expiriationTimeMinutesSpan.next(minutes);
        this.expiriationTimeSeconds = seconds;
        this.expiriationTimeSecondsSpan.next(seconds);
        console.log('noteExpirationDate noteExpirationDate noteExpirationDate noteExpirationDate', noteExpirationDate)
        this.expirationDate = `${noteExpirationDate.toLocaleDateString()} ${noteExpirationDate.toLocaleTimeString()}`;
        this.expirationDateFormed.next(this.expirationDate);
      }
    })

  console.log('this.expirationDateFormed', this.expirationDateFormed.value)
    return this.expirationDateFormed;
  }
}
