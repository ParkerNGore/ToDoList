import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MDBBootstrapModule } from 'angular-bootstrap-md';

import { DayComponent } from './day.component';
import { MdbModalModule, MdbModalService } from 'mdb-angular-ui-kit/modal';
import { ListService } from 'src/app/services/list-service.service';
import { HttpClientModule } from '@angular/common/http';

describe('DayComponent', () => {
  let component: DayComponent;
  let fixture: ComponentFixture<DayComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MDBBootstrapModule.forRoot(), MdbModalModule, HttpClientModule],
      providers: [MdbModalService, ListService],
      declarations: [DayComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(DayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
  // it('Should have at least 24 rows', () => {
  //   expect(component.data.length == 24);
  // });
});
