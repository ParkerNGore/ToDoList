import { ListItem } from './listItem';

export class ListType {
  id: string;
  createdDate: Date;
  lastUpdatedDate: Date;
  name: string;
  description: string;
  listItems: ListItem[];
  constructor(obj?: ListType) {
    this.id = obj?.id || '';
    this.createdDate = obj?.createdDate || new Date();
    this.lastUpdatedDate = obj?.lastUpdatedDate || new Date();
    this.name = obj?.name || 'Default';
    this.description = obj?.description || '';
    this.listItems = obj?.listItems || [];
  }
}
