import { HttpClientModule } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';

import { ListTypeService } from './listtypeservice.service';

describe('ListtypeserviceService', () => {
  let service: ListTypeService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule],
    });
    service = TestBed.inject(ListTypeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
