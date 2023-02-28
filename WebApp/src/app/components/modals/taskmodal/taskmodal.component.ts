import { Component, Input, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { MdbModalRef, MdbModalService } from 'mdb-angular-ui-kit/modal';
import { Importance, RepeatFrequency } from 'src/app/models/global/enums';
import { ListType } from 'src/app/models/listType';
import { ListService } from 'src/app/services/list-service.service';
import { ListItem } from '../../../models/listItem';

@Component({
  selector: 'app-taskmodal',
  templateUrl: './taskmodal.component.html',
  styleUrls: ['./taskmodal.component.css'],
})
export class TaskModalComponent implements OnInit {
  listItem: ListItem | null = null;
  isNewListType: boolean = false;

  keys = Object.keys;
  importances = Importance;
  // @ts-ignore
  // modalRef: MdbModalRef<TaskModalComponent>;

  constructor(
    public modalRef: MdbModalRef<TaskModalComponent>,
    private modalService: MdbModalService,
    private listService: ListService,
    private fb: FormBuilder
  ) {}

  openTaskModal(listItem: ListItem) {
    this.modalRef = this.modalService.open(TaskModalComponent, {
      data: { listItem: listItem },
    });
  }

  closeModal() {
    const test = this.taskForm.value.id;
    this.modalRef.close(test);
  }

  frequencies = Object.values(RepeatFrequency).filter((value) =>
    isNaN(parseInt(value as string))
  );
  importanceLevels = Object.values(Importance).filter((value) =>
    isNaN(parseInt(value as string))
  );

  getLocaleDateStringCurrentTimezone = (
    date: Date,
    tzOffsetExternal: number = 0
  ) => {
    const tzoffset =
      tzOffsetExternal == 0
        ? date.getTimezoneOffset() * 60000
        : tzOffsetExternal; //offset in milliseconds
    const localISOTime = new Date(date.getTime() - tzoffset)
      .toISOString()
      .slice(0, -8);
    return localISOTime;
  };

  taskForm = this.fb.group({
    id: this.listItem ? this.listItem.id : '',
    createdDate: this.getLocaleDateStringCurrentTimezone(
      this.listItem ? new Date(this.listItem.createdDate) : new Date()
    ),
    lastUpdatedDate: this.getLocaleDateStringCurrentTimezone(
      this.listItem ? new Date(this.listItem.lastUpdatedDate) : new Date()
    ),
    dueDate: this.getLocaleDateStringCurrentTimezone(
      this.listItem ? new Date(this.listItem.dueDate) : new Date()
    ),
    isAllDay: this.listItem ? this.listItem.isAllDay : false,
    title: this.listItem ? this.listItem.title : '',
    description: this.listItem ? this.listItem.description : '',
    frequency: this.listItem ? this.listItem.frequency : RepeatFrequency.Weekly,
    importance: this.listItem ? this.listItem.importance : Importance.Normal,
    listTypeName: this.listItem ? this.listItem.listTypeName : '',
    isNewListType: false,
    isCompleted: this.listItem ? this.listItem.isCompleted : false,
    type: this.listItem ? this.listItem.type : new ListType(),
  } as ListItem);

  // id: string;
  // createdDate: Date;
  // lastUpdatedDate: Date;
  // dueDate: string;
  // title: string;
  // description: string;
  // frequency: RepeatFrequency;
  // importance: Importance;
  // isAllDay: boolean;
  // isCompleted: boolean;
  // listTypeName: string;
  // type: ListType;
  cSharpDateToJSDate = (cDate: string) => {
    const year = cDate.toString().substring(0, 4);
    const month = cDate.toString().substring(5, 7);
    const day = cDate.toString().substring(8, 10);
    const hours = cDate.toString().split('T')[1].substring(0, 2);
    const newDate = new Date(
      parseInt(year),
      parseInt(month) - 1,
      parseInt(day),
      parseInt(hours)
    );
    return newDate;
  };
  ngOnInit(): void {
    if (this.listItem != null) {
      this.taskForm.patchValue({
        id: this.listItem.id,
        createdDate: this.getLocaleDateStringCurrentTimezone(
          this.cSharpDateToJSDate(this.listItem.createdDate)
        ),
        lastUpdatedDate: this.getLocaleDateStringCurrentTimezone(
          this.cSharpDateToJSDate(this.listItem.lastUpdatedDate)
        ),
        dueDate: this.getLocaleDateStringCurrentTimezone(
          this.cSharpDateToJSDate(this.listItem.dueDate)
        ),
        isAllDay: this.listItem.isAllDay,
        title: this.listItem.title,
        description: this.listItem.description,
        frequency: this.listItem.frequency,
        importance: this.listItem.importance,
        listTypeName: this.listItem.listTypeName,
        isCompleted: this.listItem.isCompleted,
      });
    } else {
      alert('Task is null when it should not be.');
    }
  }
  onCheckboxClick = (input: any) => {
    this.isNewListType = input.target.checked;
  };
  ngOnChanges() {}
  delete = () => {
    if (this.taskForm.value.id)
      this.listService.delete(this.taskForm.value.id).subscribe((res) => {
        console.log(res);
      });
  };

  save = () => {
    if (this.taskForm.valid)
      if (this.listItem != null)
        this.listService
          .update(this.taskForm.value as ListItem, this.isNewListType)
          .subscribe((res) => {
            this.listItem = res;
          });

    this.closeModal();
  };
}
