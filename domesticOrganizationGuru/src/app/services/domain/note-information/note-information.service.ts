import { NotePointsState } from './../../../state/states/notes/notes.inteface';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Store } from '@ngrx/store';
import { getMinutesTillExpireSelector } from 'src/app/state/states/settings/settings.selector';

@Injectable({
  providedIn: 'root'
})
export class NoteInformationService {
  expiriationTime: number = 0;
  expirationDate: string = '';
  private expiriationTimeSpan = new BehaviorSubject<number>(this.expiriationTime);
  private expirationDateFormed = new BehaviorSubject<string>(this.expirationDate);


constructor(private store: Store<NotePointsState>) {
  let intervalId = setInterval(() => {
    this.expiriationTime = this.expiriationTime- 1;
    this.expiriationTimeSpan.next(this.expiriationTime);
    if(this.expiriationTime === 0) clearInterval(intervalId)
  }, 60000)
 }


public getExpirationTime = () => {
  this.store.select(getMinutesTillExpireSelector)
    .subscribe(minutes => {
      if(minutes){
        this.expiriationTime = minutes!;
        this.expiriationTimeSpan.next(this.expiriationTime);
      }
    })
  return this.expiriationTimeSpan;
}

public getExpirationDate = () => {
    this.store.select(getMinutesTillExpireSelector)
    .subscribe(minutes => {
      console.log('bump')
      if(minutes){
        this.expiriationTime = minutes!;
        this.expiriationTimeSpan.next(this.expiriationTime);
        let expirationDate = new Date(Date.now() + this.expiriationTimeSpan.value! * 60000);
        this.expirationDate = `${expirationDate.toLocaleDateString()} ${expirationDate.toLocaleTimeString()}`;
        this.expirationDateFormed.next(this.expirationDate);
        console.log(this.expirationDateFormed.value)
      }
    })
    return this.expirationDateFormed;
  }
}
