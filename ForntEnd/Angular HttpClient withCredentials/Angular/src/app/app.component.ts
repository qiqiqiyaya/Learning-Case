import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'Angular';

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.
  }

  getCookie() {
    this.http
      .get('https://localhost:7209/getCookie', { withCredentials: true })
      .subscribe((res) => {});
  }

  sendWithCookie() {
    this.http
      .get('https://localhost:7209/containCookie', { withCredentials: true })
      .subscribe((res) => {});
  }
}
