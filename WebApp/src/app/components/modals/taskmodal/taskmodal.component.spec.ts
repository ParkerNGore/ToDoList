import { HttpClientModule } from '@angular/common/http';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MDBBootstrapModule } from 'angular-bootstrap-md';
import { MdbModalModule, MdbModalService } from 'mdb-angular-ui-kit/modal';
import { ListService } from 'src/app/services/list-service.service';

import { TaskModalComponent } from './taskmodal.component';
import { MdbFormsModule } from 'mdb-angular-ui-kit/forms';

describe('TaskModalComponent', () => {
  let component: TaskModalComponent;
  let fixture: ComponentFixture<TaskModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TaskModalComponent],
      imports: [
        MDBBootstrapModule.forRoot(),
        MdbModalModule,
        HttpClientModule,
        FormsModule,
        ReactiveFormsModule,
      ],
      providers: [MdbModalService, ListService],
    }).compileComponents();

    fixture = TestBed.createComponent(TaskModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
