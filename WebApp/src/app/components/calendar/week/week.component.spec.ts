import { HttpClientModule } from '@angular/common/http';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MDBBootstrapModule } from 'angular-bootstrap-md';
import { MdbModalModule, MdbModalService } from 'mdb-angular-ui-kit/modal';
import { ListService } from 'src/app/services/list-service.service';

import { WeekComponent } from './week.component';

describe('WeekComponent', () => {
  let component: WeekComponent;
  let fixture: ComponentFixture<WeekComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [WeekComponent],
      imports: [MDBBootstrapModule.forRoot(), MdbModalModule, HttpClientModule],
      providers: [MdbModalService, ListService],
    }).compileComponents();

    fixture = TestBed.createComponent(WeekComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
