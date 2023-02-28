import { Importance, RepeatFrequency } from './global/enums';
import { ListType } from './listType';

export class ListItem {
  id: string;
  createdDate: string;
  lastUpdatedDate: string;
  dueDate: string;
  title: string;
  description: string;
  frequency: RepeatFrequency;
  importance: Importance;
  isAllDay: boolean;
  isCompleted: boolean;
  listTypeName: string;
  type: ListType;

  constructor(obj?: ListItem) {
    this.id = obj?.id || '';
    this.createdDate = obj?.createdDate || new Date().toString();
    this.lastUpdatedDate = obj?.lastUpdatedDate || new Date().toString();
    this.dueDate = obj?.dueDate || new Date().toString();
    this.title = obj?.title || '';
    this.description = obj?.description || '';
    this.frequency = obj?.frequency || RepeatFrequency.Weekly;
    this.importance = obj?.importance || Importance.Normal;
    this.isAllDay = obj?.isAllDay || false;
    this.isCompleted = obj?.isCompleted || false;
    this.listTypeName = obj?.listTypeName || 'Default';
    this.type = obj?.type || new ListType();
  }
}
