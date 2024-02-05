import { Component } from '@angular/core';
import { HttpResponseMessageService } from './services/http-response-message.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  responeMessage: string = "";
  private httpResponseMessageServiceSubscription: Subscription;
  
  constructor(private httpResponseMessageService: HttpResponseMessageService) {
    this.httpResponseMessageServiceSubscription = this.httpResponseMessageService.responseMessage$.subscribe((message) => {
      this.responeMessage = message;
    });
  }

  title = 'myPlannerApp';
  ngOnDestroy() {
    this.httpResponseMessageServiceSubscription.unsubscribe();
  }
}