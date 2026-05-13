import { TestBed } from '@angular/core/testing';

import { ServiceDonorService } from './donorService.service';

describe('ServiceDonorService', () => {
  let service: ServiceDonorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ServiceDonorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
