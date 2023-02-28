import { Component } from '@angular/core';
import { MdbModalRef, MdbModalService } from 'mdb-angular-ui-kit/modal';
import { CreateListModalComponent } from './components/modals/createlistmodal/createlistmodal.component';
import { calendarOptions } from './models/global/enums';
import { ListService } from './services/list-service.service';
import { CreateListItemDto } from './models/createListItemDto';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  createModalRef: MdbModalRef<CreateListModalComponent> | null = null;

  response = 'No data loaded, yet';
  constructor(
    private modalService: MdbModalService,
    private listService: ListService
  ) {}
  openCreateModal() {
    this.createModalRef = this.modalService.open(CreateListModalComponent);
  }

  calendarSettings = [
    calendarOptions.Day,
    calendarOptions.Month,
    calendarOptions.Week,
  ];
  selectedSetting: calendarOptions = calendarOptions.Day;
  selectedDate: Date = new Date();

  previousButton = () => {
    if (this.selectedSetting == 'Month') {
      this.selectedDate = new Date(
        this.selectedDate.getFullYear(),
        this.selectedDate.getMonth() - 1
      );
    } else if (this.selectedSetting == 'Week') {
      this.selectedDate = new Date(
        this.selectedDate.getFullYear(),
        this.selectedDate.getMonth(),
        this.selectedDate.getDate() - this.selectedDate.getDay() - 7
      );
    } else {
      this.selectedDate = new Date(
        this.selectedDate.getFullYear(),
        this.selectedDate.getMonth(),
        this.selectedDate.getDate() - 1
      );
    }
  };

  nextButton = () => {
    if (this.selectedSetting == 'Month') {
      this.selectedDate = new Date(
        this.selectedDate.getFullYear(),
        this.selectedDate.getMonth() + 1
      );
    } else if (this.selectedSetting == 'Week') {
      this.selectedDate = new Date(
        this.selectedDate.getFullYear(),
        this.selectedDate.getMonth(),
        this.selectedDate.getDate() - this.selectedDate.getDay() + 7
      );
    } else {
      this.selectedDate = new Date(
        this.selectedDate.getFullYear(),
        this.selectedDate.getMonth(),
        this.selectedDate.getDate() + 1
      );
    }
  };

  todayButton = () => {
    this.selectedDate = new Date();
  };
  createTest = () => {
    const dto: CreateListItemDto = new CreateListItemDto();
    dto.isNewListType = true;
    dto.title = 'FirstTask';
    this.listService.create(dto).subscribe((res) => {
      console.log(res);
    });
  };

  ngOnInit(): void {}
}
