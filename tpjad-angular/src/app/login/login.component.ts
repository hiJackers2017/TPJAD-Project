import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public email: string;
  public password: string;
  constructor(private router: Router) { }

  ngOnInit() {
  }

  public loginWithCredentials() {
  }

  public keyDownEvent(event: any) {
    if (event.keyCode === 13) {
      this.loginWithCredentials();
    }
  }
}
