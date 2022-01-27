import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'password-creator',
  templateUrl: './password-creator.component.html',
  styleUrls: ['./password-creator.component.scss']
})
export class PasswordCreatorComponent implements OnInit {
  @Input()
  isOpen: boolean = false;

  isPasswordRequested:boolean = false;

  newPassword = new FormControl('', [Validators.required, Validators.minLength(5), Validators.maxLength(30)]);

  @Output()
  readonly isOpenChange = new EventEmitter<boolean>();

  constructor(
    public router: Router
    ) {}

  ngOnInit() {
  }
  goForward(){
    console.log('newPassword', this.newPassword.value);
    console.log('newPassword.valid', this.newPassword.valid);
    // this.router.navigate(['/to-do']);
  }

  buttonEnabled(): boolean{
    return !this.isPasswordRequested || this.isPasswordRequested && this.newPassword.valid;
  }

  resetPassword(): void {
    this.newPassword.setValue("");
  }
}
