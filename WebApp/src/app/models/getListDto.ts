import { CalendarView } from './global/enums';

export class GetListDto {
  date: Date;
  calendarView: CalendarView;
  ignoreCompleted: boolean;
  listTypeName: string | null;

  constructor(obj?: GetListDto) {
    this.date = obj?.date || new Date();
    this.calendarView = obj?.calendarView || CalendarView.Month;
    this.ignoreCompleted = obj?.ignoreCompleted || false;
    this.listTypeName = obj?.listTypeName || null;
  }
}
