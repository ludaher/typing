import { Component } from '@angular/core';
import { AuthService } from './services/authentication/auth.service';
import { OnInit } from '@angular/core/src/metadata/lifecycle_hooks';
import { PlatformLocation } from '@angular/common';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'app';
  logged = false;
  constructor(private authService: AuthService, private location: PlatformLocation) {
    authService.userLoggedin.subscribe(item => {
      this.logged = this.authService.isLoggedIn();
    });
    location.onPopState(() => {
      console.log('pressed back!');
      return false;
    });
  }

  ngOnInit() {
    this.logged = this.authService.isLoggedIn();
  }
}
