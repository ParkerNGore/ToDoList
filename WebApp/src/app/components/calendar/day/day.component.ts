import { Component, Input, OnInit } from '@angular/core';
import { MdbModalRef, MdbModalService } from 'mdb-angular-ui-kit/modal';
import { GetListDto } from 'src/app/models/getListDto';
import { CalendarView } from 'src/app/models/global/enums';
import { ListItem } from 'src/app/models/listItem';
import { ListService } from 'src/app/services/list-service.service';
import { TaskModalComponent } from '../../modals/taskmodal/taskmodal.component';
type day = {
  number: string;
  tasks: ListItem[];
};
type time = {
  number: string;
  tasks: ListItem[];
};

type week = {
  Time: time;
  Sunday: day;
  Monday: day;
  Tuesday: day;
  Wednesday: day;
  Thursday: day;
  Friday: day;
  Saturday: day;
};

@Component({
  selector: 'app-day',
  templateUrl: './day.component.html',
  styleUrls: ['./day.component.css'],
})
export class DayComponent implements OnInit {
  @Input() newDate: Date = new Date();
  constructor(
    private modalService: MdbModalService,
    private listService: ListService
  ) {}
  selectedDate: Date = new Date();
  taskModalRef: MdbModalRef<TaskModalComponent> | null = null;
  listItems: ListItem[] = [];
  displayedColumns = [
    'Time',
    this.selectedDate.toLocaleString('en-us', { weekday: 'long' }),
  ];
  getListDto: GetListDto = new GetListDto({
    date: this.selectedDate,
    calendarView: CalendarView.Day,
    ignoreCompleted: false,
    listTypeName: null,
  });

  listItemReferenceId = '';
  listItemIndexReference = 0;

  openTaskModal(listItem: ListItem) {
    this.listItemReferenceId = listItem.id;
    this.taskModalRef = this.modalService.open(TaskModalComponent, {
      data: { listItem: listItem },
    });
    this.taskModalRef.onClose.subscribe((message: any) => {
      this.listService
        .getByViewWithOptions(this.getListDto)
        .subscribe((res: ListItem[]) => {
          this.listItems = res;
        });

      setTimeout(() => {
        this.data = this.createData() as any;
      }, 1000);
    });
  }

  createData = () => {
    const returnArray = [];
    const firstDayOfThisWeek =
      this.selectedDate.getDate() - this.selectedDate.getDay();
    const lastDayOfTheMonth = new Date(
      this.selectedDate.getFullYear(),
      this.selectedDate.getMonth() + 1,
      0
    ).getDate();

    const weekStartsInCurrentMonth = firstDayOfThisWeek > 0;
    let enteredIntoNextMonth = false;
    const lastDayOfPreviousMonth = new Date(
      this.selectedDate.getFullYear(),
      this.selectedDate.getMonth() - 1,
      0
    ).getDate();

    let numberToUse = weekStartsInCurrentMonth
      ? firstDayOfThisWeek
      : lastDayOfPreviousMonth + firstDayOfThisWeek;

    for (let i = -1; i < 24; i++) {
      if (i > -1) {
        returnArray.push({
          Time: {
            number: i + ':00',
            tasks: [],
          },
        });
        // @ts-ignore
        returnArray[i + 1][this.displayedColumns[1]] = {
          number: '',
          tasks: this.listItems.filter(
            (x) =>
              !x.isAllDay &&
              new Date(x.dueDate).getHours() == i &&
              new Date(x.dueDate).getDate() == this.selectedDate.getDate()
          ),
        };
      } else {
        returnArray.push({
          Time: {
            number: 'All Day',
            tasks: [],
          },
        });
        // @ts-ignore
        returnArray[0][this.displayedColumns[1]] = {
          number: '',
          tasks: this.listItems.filter(
            (x) =>
              x.isAllDay &&
              new Date(x.dueDate).getDate() == this.selectedDate.getDate()
          ),
        };
      }
    }

    return returnArray;
  };

  data = this.createData();

  ngOnChanges() {
    this.selectedDate = this.newDate;
    this.getListDto.date = this.selectedDate;

    this.listService
      .getByViewWithOptions(this.getListDto)
      .subscribe((res: ListItem[]) => {
        this.listItems = res;
      });
    this.displayedColumns = [
      'Time',
      this.selectedDate.toLocaleString('en-us', { weekday: 'long' }),
    ];
    setTimeout(() => {
      this.data = this.createData() as any;
    }, 500);
  }

  ngOnInit() {
    this.selectedDate = this.newDate;
    this.getListDto.date = this.selectedDate;

    this.listService.getByViewWithOptions(this.getListDto).subscribe((res) => {
      this.listItems = res;
    });
    this.displayedColumns = [
      'Time',
      this.selectedDate.toLocaleString('en-us', { weekday: 'long' }),
    ];
    setTimeout(() => {
      this.data = this.createData() as any;
    }, 500);
  }
}
