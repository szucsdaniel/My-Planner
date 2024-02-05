import { TestBed } from '@angular/core/testing';

import { UpdateSelectedBranchService } from './update-selected-branch.service';

describe('UpdateSelectedBranchService', () => {
  let service: UpdateSelectedBranchService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UpdateSelectedBranchService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
