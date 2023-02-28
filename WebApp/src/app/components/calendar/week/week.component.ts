import { Component, Input, OnInit } from '@angular/core';
import { MdbModalRef, MdbModalService } from 'mdb-angular-ui-kit/modal';
import { GetListDto } from 'src/app/models/getListDto';
import { CalendarView, RepeatFrequency } from 'src/app/models/global/enums';
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
  selector: 'app-week',
  templateUrl: './week.component.html',
  styleUrls: ['./week.component.css'],
})
export class WeekComponent implements OnInit {
  @Input() newDate: Date = new Date();
  constructor(
    private modalService: MdbModalService,
    private listService: ListService
  ) {}
  taskModalRef: MdbModalRef<TaskModalComponent> | null = null;
  listItems: ListItem[] = [];
  listItemReferenceId = '';
  listItemIndexReference = 0;

  openTaskModal(listItem: ListItem) {
    this.listItemReferenceId = listItem.id;
    this.taskModalRef = this.modalService.open(TaskModalComponent, {
      data: { listItem: listItem },
    });
    this.taskModalRef.onClose.subscribe((message: any) => {
      for (var i = 0; i < this.listItems.length; i++) {
        var obj = this.listItems[i];
        if (obj.id == this.listItemReferenceId) {
          this.listItemIndexReference = i;
        }
      }

      const list = this.listItems;
      this.listService
        .getListItem(this.listItemReferenceId)
        .subscribe((res) => {
          list[this.listItemIndexReference] = res;
          this.listItems = list;
        });
      console.log(this.listItems);

      setTimeout(() => {
        this.data = this.createData() as any;
      }, 1000);
    });
  }

  selectedDate: Date = new Date();
  getListDto: GetListDto = new GetListDto({
    date: this.selectedDate,
    calendarView: CalendarView.Week,
    ignoreCompleted: false,
    listTypeName: null,
  });

  displayedColumns: string[] = [
    'Time',
    'Sunday',
    'Monday',
    'Tuesday',
    'Wednesday',
    'Thursday',
    'Friday',
    'Saturday',
  ];

  createData = () => {
    const returnArray: week[] = [];
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
          Sunday: {
            number: '',
            tasks: this.listItems.filter(
              (x) =>
                (!x.isAllDay &&
                  new Date(x.dueDate).getHours() == i &&
                  new Date(x.dueDate).getDate() == firstDayOfThisWeek) ||
                (!x.isAllDay &&
                  new Date(x.dueDate).getHours() == i &&
                  new Date(x.dueDate).getDate() > firstDayOfThisWeek &&
                  !x.isCompleted &&
                  x.frequency == RepeatFrequency.Daily)
            ),
          },
          Monday: {
            number: '',
            tasks: this.listItems.filter(
              (x) =>
                (!x.isAllDay &&
                  new Date(x.dueDate).getHours() == i &&
                  new Date(x.dueDate).getDate() == firstDayOfThisWeek + 1) ||
                (!x.isAllDay &&
                  new Date(x.dueDate).getHours() == i &&
                  new Date(x.dueDate).getDate() > firstDayOfThisWeek + 1 &&
                  !x.isCompleted &&
                  x.frequency == RepeatFrequency.Daily)
            ),
          },
          Tuesday: {
            number: '',
            tasks: this.listItems.filter(
              (x) =>
                (!x.isAllDay &&
                  new Date(x.dueDate).getHours() == i &&
                  new Date(x.dueDate).getDate() == firstDayOfThisWeek + 2) ||
                (!x.isAllDay &&
                  new Date(x.dueDate).getHours() == i &&
                  new Date(x.dueDate).getDate() > firstDayOfThisWeek + 2 &&
                  !x.isCompleted &&
                  x.frequency == RepeatFrequency.Daily)
            ),
          },
          Wednesday: {
            number: '',
            tasks: this.listItems.filter(
              (x) =>
                (!x.isAllDay &&
                  new Date(x.dueDate).getHours() == i &&
                  new Date(x.dueDate).getDate() == firstDayOfThisWeek + 3) ||
                (!x.isAllDay &&
                  new Date(x.dueDate).getHours() == i &&
                  new Date(x.dueDate).getDate() > firstDayOfThisWeek + 3 &&
                  !x.isCompleted &&
                  x.frequency == RepeatFrequency.Daily)
            ),
          },
          Thursday: {
            number: '',
            tasks: this.listItems.filter(
              (x) =>
                (!x.isAllDay &&
                  new Date(x.dueDate).getHours() == i &&
                  new Date(x.dueDate).getDate() == firstDayOfThisWeek + 4) ||
                (!x.isAllDay &&
                  new Date(x.dueDate).getHours() == i &&
                  new Date(x.dueDate).getDate() > firstDayOfThisWeek + 4 &&
                  !x.isCompleted &&
                  x.frequency == RepeatFrequency.Daily)
            ),
          },
          Friday: {
            number: '',
            tasks: this.listItems.filter(
              (x) =>
                (!x.isAllDay &&
                  new Date(x.dueDate).getHours() == i &&
                  new Date(x.dueDate).getDate() == firstDayOfThisWeek + 5) ||
                (!x.isAllDay &&
                  new Date(x.dueDate).getHours() == i &&
                  new Date(x.dueDate).getDate() > firstDayOfThisWeek + 5 &&
                  !x.isCompleted &&
                  x.frequency == RepeatFrequency.Daily)
            ),
          },
          Saturday: {
            number: '',
            tasks: this.listItems.filter(
              (x) =>
                (!x.isAllDay &&
                  new Date(x.dueDate).getHours() == i &&
                  new Date(x.dueDate).getDate() == firstDayOfThisWeek + 6) ||
                (!x.isAllDay &&
                  new Date(x.dueDate).getHours() == i &&
                  new Date(x.dueDate).getDate() > firstDayOfThisWeek + 6 &&
                  !x.isCompleted &&
                  x.frequency == RepeatFrequency.Daily)
            ),
          },
        });
      } else {
        returnArray.push({
          Time: {
            number: 'All Day',
            tasks: [],
          },
          Sunday: {
            number: '',
            tasks: this.listItems.filter(
              (x) =>
                (x.isAllDay &&
                  new Date(x.dueDate).getDate() == firstDayOfThisWeek) ||
                (x.isAllDay &&
                  new Date(x.dueDate).getDate() > firstDayOfThisWeek &&
                  !x.isCompleted &&
                  x.frequency == RepeatFrequency.Daily)
            ),
          },
          Monday: {
            number: '',
            tasks: this.listItems.filter(
              (x) =>
                (x.isAllDay &&
                  new Date(x.dueDate).getDate() == firstDayOfThisWeek + 1) ||
                (x.isAllDay &&
                  new Date(x.dueDate).getDate() > firstDayOfThisWeek + 1 &&
                  !x.isCompleted &&
                  x.frequency == RepeatFrequency.Daily)
            ),
          },
          Tuesday: {
            number: '',
            tasks: this.listItems.filter(
              (x) =>
                (x.isAllDay &&
                  new Date(x.dueDate).getDate() == firstDayOfThisWeek + 2) ||
                (x.isAllDay &&
                  new Date(x.dueDate).getDate() > firstDayOfThisWeek + 2 &&
                  !x.isCompleted &&
                  x.frequency == RepeatFrequency.Daily)
            ),
          },
          Wednesday: {
            number: '',
            tasks: this.listItems.filter(
              (x) =>
                (x.isAllDay &&
                  new Date(x.dueDate).getDate() == firstDayOfThisWeek + 3) ||
                (x.isAllDay &&
                  new Date(x.dueDate).getDate() > firstDayOfThisWeek + 3 &&
                  !x.isCompleted &&
                  x.frequency == RepeatFrequency.Daily)
            ),
          },
          Thursday: {
            number: '',
            tasks: this.listItems.filter(
              (x) =>
                (x.isAllDay &&
                  new Date(x.dueDate).getDate() == firstDayOfThisWeek + 4) ||
                (x.isAllDay &&
                  new Date(x.dueDate).getDate() > firstDayOfThisWeek + 4 &&
                  !x.isCompleted &&
                  x.frequency == RepeatFrequency.Daily)
            ),
          },
          Friday: {
            number: '',
            tasks: this.listItems.filter(
              (x) =>
                (x.isAllDay &&
                  new Date(x.dueDate).getDate() == firstDayOfThisWeek + 5) ||
                (x.isAllDay &&
                  new Date(x.dueDate).getDate() > firstDayOfThisWeek + 5 &&
                  !x.isCompleted &&
                  x.frequency == RepeatFrequency.Daily)
            ),
          },
          Saturday: {
            number: '',
            tasks: this.listItems.filter(
              (x) =>
                (x.isAllDay &&
                  new Date(x.dueDate).getDate() == firstDayOfThisWeek + 6) ||
                (x.isAllDay &&
                  new Date(x.dueDate).getDate() > firstDayOfThisWeek + 6 &&
                  !x.isCompleted &&
                  x.frequency == RepeatFrequency.Daily)
            ),
          },
        });
      }
    }

    return returnArray;
  };

  // createTableColumns = (): string[] => {
  //   const returnArray: string[] = [];

  //   const firstDayOfThisWeek =
  //     this.selectedDate.getDate() - this.selectedDate.getDay();
  //   const lastDayOfTheMonth = new Date(
  //     this.selectedDate.getFullYear(),
  //     this.selectedDate.getMonth() + 1,
  //     0
  //   ).getDate();

  //   const weekStartsInCurrentMonth = firstDayOfThisWeek > 0;
  //   let enteredIntoNextMonth = false;
  //   const lastDayOfPreviousMonth = new Date(
  //     this.selectedDate.getFullYear(),
  //     this.selectedDate.getMonth() - 1,
  //     0
  //   ).getDate();

  //   let numberToUse = weekStartsInCurrentMonth
  //     ? firstDayOfThisWeek
  //     : lastDayOfPreviousMonth + firstDayOfThisWeek;

  //   returnArray.push('Time');

  //   returnArray.push(
  //     'Sunday ' + this.selectedDate.getMonth() + 1 + '/' + numberToUse
  //   );
  //   if (enteredIntoNextMonth) numberToUse++;
  //   else if (weekStartsInCurrentMonth) {
  //     if (
  //       numberToUse + 1 == lastDayOfTheMonth ||
  //       numberToUse + 1 < lastDayOfTheMonth
  //     ) {
  //       numberToUse++;
  //     } else {
  //       numberToUse = 1;
  //       enteredIntoNextMonth = true;
  //     }
  //   } else {
  //     if (numberToUse + 1 >= lastDayOfPreviousMonth) {
  //       numberToUse = 1;
  //       enteredIntoNextMonth = true;
  //     } else {
  //       numberToUse++;
  //     }
  //   }
  //   returnArray.push(
  //     'Monday ' + this.selectedDate.getMonth() + 1 + '/' + numberToUse
  //   );

  //   if (enteredIntoNextMonth) numberToUse++;
  //   else if (weekStartsInCurrentMonth) {
  //     if (
  //       numberToUse + 1 == lastDayOfTheMonth ||
  //       numberToUse + 1 < lastDayOfTheMonth
  //     ) {
  //       numberToUse++;
  //     } else {
  //       numberToUse = 1;
  //       enteredIntoNextMonth = true;
  //     }
  //   } else {
  //     if (numberToUse + 1 >= lastDayOfPreviousMonth) {
  //       numberToUse = 1;
  //       enteredIntoNextMonth = true;
  //     } else {
  //       numberToUse++;
  //     }
  //   }
  //   returnArray.push(
  //     'Tuesday ' + this.selectedDate.getMonth() + 1 + '/' + numberToUse
  //   );
  //   if (enteredIntoNextMonth) numberToUse++;
  //   else if (weekStartsInCurrentMonth) {
  //     if (
  //       numberToUse + 1 == lastDayOfTheMonth ||
  //       numberToUse + 1 < lastDayOfTheMonth
  //     ) {
  //       numberToUse++;
  //     } else {
  //       numberToUse = 1;
  //       enteredIntoNextMonth = true;
  //     }
  //   } else {
  //     if (numberToUse + 1 >= lastDayOfPreviousMonth) {
  //       numberToUse = 1;
  //       enteredIntoNextMonth = true;
  //     } else {
  //       numberToUse++;
  //     }
  //   }
  //   returnArray.push(
  //     'Wednesday ' + this.selectedDate.getMonth() + 1 + '/' + numberToUse
  //   );
  //   if (enteredIntoNextMonth) numberToUse++;
  //   else if (weekStartsInCurrentMonth) {
  //     if (
  //       numberToUse + 1 == lastDayOfTheMonth ||
  //       numberToUse + 1 < lastDayOfTheMonth
  //     ) {
  //       numberToUse++;
  //     } else {
  //       numberToUse = 1;
  //       enteredIntoNextMonth = true;
  //     }
  //   } else {
  //     if (numberToUse + 1 >= lastDayOfPreviousMonth) {
  //       numberToUse = 1;
  //       enteredIntoNextMonth = true;
  //     } else {
  //       numberToUse++;
  //     }
  //   }
  //   returnArray.push(
  //     'Thursday ' + this.selectedDate.getMonth() + 1 + '/' + numberToUse
  //   );
  //   if (enteredIntoNextMonth) numberToUse++;
  //   else if (weekStartsInCurrentMonth) {
  //     if (
  //       numberToUse + 1 == lastDayOfTheMonth ||
  //       numberToUse + 1 < lastDayOfTheMonth
  //     ) {
  //       numberToUse++;
  //     } else {
  //       numberToUse = 1;
  //       enteredIntoNextMonth = true;
  //     }
  //   } else {
  //     if (numberToUse + 1 >= lastDayOfPreviousMonth) {
  //       numberToUse = 1;
  //       enteredIntoNextMonth = true;
  //     } else {
  //       numberToUse++;
  //     }
  //   }
  //   returnArray.push(
  //     'Friday ' + this.selectedDate.getMonth() + 1 + '/' + numberToUse
  //   );
  //   if (enteredIntoNextMonth) numberToUse++;
  //   else if (weekStartsInCurrentMonth) {
  //     if (
  //       numberToUse + 1 == lastDayOfTheMonth ||
  //       numberToUse + 1 < lastDayOfTheMonth
  //     ) {
  //       numberToUse++;
  //     } else {
  //       numberToUse = 1;
  //       enteredIntoNextMonth = true;
  //     }
  //   } else {
  //     if (numberToUse + 1 >= lastDayOfPreviousMonth) {
  //       numberToUse = 1;
  //       enteredIntoNextMonth = true;
  //     } else {
  //       numberToUse++;
  //     }
  //   }
  //   returnArray.push(
  //     'Saturday ' + this.selectedDate.getMonth() + 1 + '/' + numberToUse
  //   );
  //   console.log(returnArray);

  //   return returnArray;
  // };

  data: week[] = [];

  ngOnInit(): void {
    this.selectedDate = this.newDate;
    this.getListDto.date = this.selectedDate;
    this.listService
      .getByViewWithOptions(this.getListDto)
      .subscribe((res: ListItem[]) => {
        this.listItems = res;
      });
    setTimeout(() => {
      this.data = this.createData();
    }, 500);
  }
  ngOnChanges() {
    this.selectedDate = this.newDate;
    this.getListDto.date = this.selectedDate;
    this.listService
      .getByViewWithOptions(this.getListDto)
      .subscribe((res: ListItem[]) => {
        this.listItems = res;
      });
    setTimeout(() => {
      this.data = this.createData();
    }, 500);
  }
}
