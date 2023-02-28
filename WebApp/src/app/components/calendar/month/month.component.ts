import { Component, Input, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { MdbModalRef, MdbModalService } from 'mdb-angular-ui-kit/modal';
import { TaskModalComponent } from '../../modals/taskmodal/taskmodal.component';
import { ListItem } from 'src/app/models/listItem';
import { ListService } from 'src/app/services/list-service.service';
import { GetListDto } from 'src/app/models/getListDto';
import { CalendarView, RepeatFrequency } from '../../../models/global/enums';

type task = {
  title: string;
  isTaskComplete: boolean;
};
type day = {
  number: number;
  tasks: ListItem[];
};
type week = {
  Sunday: day;
  Monday: day;
  Tuesday: day;
  Wednesday: day;
  Thursday: day;
  Friday: day;
  Saturday: day;
};
type monthCalendar = {
  month: string;
  year: number;
  week: week[];
};

type MonthView = {
  weeks: week[];
};

@Component({
  selector: 'app-month',
  templateUrl: './month.component.html',
  styleUrls: ['./month.component.css'],
})
export class MonthComponent implements OnInit {
  @Input() newDate: Date = new Date();
  public show = true;

  constructor(
    private modalService: MdbModalService,
    private listService: ListService
  ) {}
  //@ts-ignore
  taskModalRef: MdbModalRef<TaskModalComponent>;
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

      setTimeout(() => {
        this.dataSource = this.addTasksToDays() as any;
      }, 1000);
    });
  }

  closeModal() {
    this.taskModalRef.close();
  }

  displayedColumns: string[] = [
    'Sunday',
    'Monday',
    'Tuesday',
    'Wednesday',
    'Thursday',
    'Friday',
    'Saturday',
  ];
  selectedDate: Date = new Date();
  getListDto: GetListDto = new GetListDto({
    date: this.selectedDate,
    calendarView: CalendarView.Month,
    ignoreCompleted: false,
    listTypeName: null,
  });

  // Only works if weeks start on Sunday.
  getWeeks = (date: Date) => {
    const firstOfMonth = new Date(date.getFullYear(), date.getMonth(), 1);
    const lastOfMonth = new Date(date.getFullYear(), date.getMonth(), 0);

    const used = firstOfMonth.getDay() + lastOfMonth.getDate();

    return Math.ceil(used / 7);
  };

  createWeeks = (): week[] => {
    const numberOfWeeks = this.getWeeks(this.selectedDate);
    const returnArray: week[] = [];

    let isFirstWeek = true;

    for (let i = 0; i < numberOfWeeks; i++) {
      const weekObject: week = {
        Sunday: { number: 0, tasks: [] },
        Monday: { number: 0, tasks: [] },
        Tuesday: { number: 0, tasks: [] },
        Wednesday: { number: 0, tasks: [] },
        Thursday: { number: 0, tasks: [] },
        Friday: { number: 0, tasks: [] },
        Saturday: { number: 0, tasks: [] },
      };
      const firstDayOfTheMonth = new Date(
        this.selectedDate.getFullYear(),
        this.selectedDate.getMonth(),
        1
      ).getDay();

      const lastDayOfTheMonth = new Date(
        this.selectedDate.getFullYear(),
        this.selectedDate.getMonth() + 1,
        0
      ).getDate();

      if (isFirstWeek && firstDayOfTheMonth != 0) {
        const lastDayOfPreviousMonth = new Date(
          this.selectedDate.getFullYear(),
          this.selectedDate.getMonth(),
          0
        ).getDate();

        for (let i = 0; i < firstDayOfTheMonth; i++) {
          const dayNumber = lastDayOfPreviousMonth - firstDayOfTheMonth + i + 1;

          switch (i) {
            case 0:
              weekObject.Sunday.number = dayNumber;
              break;
            case 1:
              weekObject.Monday.number = dayNumber;
              break;
            case 2:
              weekObject.Tuesday.number = dayNumber;
              break;
            case 3:
              weekObject.Wednesday.number = dayNumber;
              break;
            case 4:
              weekObject.Thursday.number = dayNumber;
              break;
            case 5:
              weekObject.Friday.number = dayNumber;
              break;
          }
        }
        for (let i = 0; i < 7 - firstDayOfTheMonth; i++) {
          switch (firstDayOfTheMonth + i) {
            case 1:
              weekObject.Monday.number = i + 1;
              break;
            case 2:
              weekObject.Tuesday.number = i + 1;
              break;
            case 3:
              weekObject.Wednesday.number = i + 1;
              break;
            case 4:
              weekObject.Thursday.number = i + 1;
              break;
            case 5:
              weekObject.Friday.number = i + 1;
              break;
            case 6:
              weekObject.Saturday.number = i + 1;
              break;
          }
        }
        isFirstWeek = false;
      } else {
        const lastNumber =
          returnArray.length < 1
            ? 0
            : returnArray[returnArray.length - 1].Saturday.number;
        for (let i = lastNumber + 1; i <= lastNumber + 7; i++) {
          switch (i - lastNumber) {
            case 1:
              weekObject.Sunday.number =
                i > lastDayOfTheMonth ? i - lastDayOfTheMonth : i;
              break;
            case 2:
              weekObject.Monday.number =
                i > lastDayOfTheMonth ? i - lastDayOfTheMonth : i;
              break;
            case 3:
              weekObject.Tuesday.number =
                i > lastDayOfTheMonth ? i - lastDayOfTheMonth : i;
              break;
            case 4:
              weekObject.Wednesday.number =
                i > lastDayOfTheMonth ? i - lastDayOfTheMonth : i;
              break;
            case 5:
              weekObject.Thursday.number =
                i > lastDayOfTheMonth ? i - lastDayOfTheMonth : i;
              break;
            case 6:
              weekObject.Friday.number =
                i > lastDayOfTheMonth ? i - lastDayOfTheMonth : i;
              break;
            case 7:
              weekObject.Saturday.number =
                i > lastDayOfTheMonth ? i - lastDayOfTheMonth : i;
              break;
          }
        }
      }
      returnArray.push(weekObject);
    }
    return returnArray;
    // return { weeks: returnArray };
  };

  addTasksToDays = () => {
    const weeks = this.createWeeks();
    const dailyTasks = this.listItems.filter(
      (x) => x.frequency == RepeatFrequency.Daily && !x.isCompleted
    );

    const weeklyTasks = this.listItems.filter(
      (x) => x.frequency == RepeatFrequency.Weekly && !x.isCompleted
    );
    const otherTasks = this.listItems.filter(
      (x) =>
        (x.frequency != RepeatFrequency.Weekly ||
          (x.frequency == RepeatFrequency.Weekly && x.isCompleted)) &&
        (x.frequency != RepeatFrequency.Daily ||
          (x.frequency == RepeatFrequency.Daily && x.isCompleted))
    );

    dailyTasks.forEach((task) => {
      weeks.forEach((week) => {
        Object.entries(week)
          .filter((day) => day[1].number >= new Date(task.dueDate).getDate())
          .map((x) => {
            x[1].tasks.push(task);
          });
      });
    });

    weeklyTasks.forEach((task) => {
      weeks.forEach((week) => {
        Object.entries(week)
          .filter(
            (day) =>
              day[1].number == new Date(task.dueDate).getDate() ||
              (day[1].number - day[1].number) / 7 == 1
          )
          .map((x) => {
            x[1].tasks.push(task);
          });
      });
    });

    otherTasks.forEach((task) => {
      weeks.forEach((week) => {
        Object.entries(week)
          .filter((day) => day[1].number == new Date(task.dueDate).getDate())
          .map((x) => {
            x[1].tasks.push(task);
          });
      });
    });

    return weeks;
  };

  // monthView: MonthView = this.createWeeks();

  // turnWeeksIntoView = (weeks: week[]): MonthView => {
  //   const returnArray: MonthView = {weeks: []};
  //   for (let i = 0; i < weeks.length; i++) {
  //     returnArray.push({
  //       Sunday: {
  //         number: weeks[i].days[0].number + '',
  //         tasks: [
  //           { title: 'Task1', isTaskComplete: true },
  //           { title: 'Task2', isTaskComplete: false },
  //         ],
  //       },
  //       Monday: {
  //         number: weeks[i].days[1].number + '',
  //         tasks: [
  //           { title: 'Task1', isTaskComplete: true },
  //           { title: 'Task2', isTaskComplete: false },
  //         ],
  //       },
  //       Tuesday: {
  //         number: weeks[i].days[2].number + '',
  //         tasks: [
  //           { title: 'Task1', isTaskComplete: true },
  //           { title: 'Task2', isTaskComplete: false },
  //         ],
  //       },
  //       Wednesday: {
  //         number: weeks[i].days[3].number + '',
  //         tasks: [
  //           { title: 'Task1', isTaskComplete: true },
  //           { title: 'Task2', isTaskComplete: false },
  //         ],
  //       },
  //       Thursday: {
  //         number: weeks[i].days[4].number + '',
  //         tasks: [
  //           { title: 'Task1', isTaskComplete: true },
  //           { title: 'Task2', isTaskComplete: false },
  //         ],
  //       },
  //       Friday: {
  //         number: weeks[i].days[5].number + '',
  //         tasks: [
  //           { title: 'Task1', isTaskComplete: true },
  //           { title: 'Task2', isTaskComplete: false },
  //         ],
  //       },
  //       Saturday: {
  //         number: weeks[i].days[6].number + '',
  //         tasks: [
  //           { title: 'Task1', isTaskComplete: true },
  //           { title: 'Task2', isTaskComplete: false },
  //         ],
  //       },
  //     });
  //   }
  //   return returnArray;
  // };
  dataSource = [];

  ngOnChanges() {
    this.selectedDate = this.newDate;
    this.getListDto.date = this.selectedDate;

    this.listService
      .getByViewWithOptions(this.getListDto)
      .subscribe((res: ListItem[]) => {
        this.listItems = res;
      });

    setTimeout(() => {
      this.dataSource = this.addTasksToDays() as any;
    }, 500);
  }

  ngOnInit() {
    this.selectedDate = this.newDate;
    this.getListDto.date = this.selectedDate;
    this.listService.getByViewWithOptions(this.getListDto).subscribe((res) => {
      this.listItems = res;
    });

    setTimeout(() => {
      this.dataSource = this.addTasksToDays() as any;
    }, 500);
  }
}
