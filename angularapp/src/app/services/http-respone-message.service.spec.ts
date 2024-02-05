import { TestBed } from '@angular/core/testing';

import { HttpResponseMessageService } from './http-response-message.service';

describe('HttpResponseMessageService', () => {
  let service: HttpResponseMessageService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HttpResponseMessageService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
