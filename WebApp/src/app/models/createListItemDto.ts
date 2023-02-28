import { Importance, RepeatFrequency } from './global/enums';

export class CreateListItemDto {
  dueDate: Date;
  isAllDay: boolean;
  title: string;
  description: string;
  frequency: RepeatFrequency;
  importance: Importance;
  listTypeName: string;
  isNewListType: boolean;

  constructor(obj?: CreateListItemDto) {
    this.dueDate = obj?.dueDate || new Date();
    this.isAllDay = obj?.isAllDay || false;
    this.title = obj?.title || '';
    this.description = obj?.description || '';
    this.frequency = obj?.frequency || RepeatFrequency.Weekly;
    this.importance = obj?.importance || Importance.Normal;
    this.listTypeName = obj?.listTypeName || 'Default';
    this.isNewListType = obj?.isNewListType || false;
  }
}
