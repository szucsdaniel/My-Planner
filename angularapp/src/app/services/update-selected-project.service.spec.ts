import { TestBed } from '@angular/core/testing';

import { UpdateSelectedProjectService } from './update-selected-project.service';

describe('UpdateSelectedProjectService', () => {
  let service: UpdateSelectedProjectService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UpdateSelectedProjectService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
