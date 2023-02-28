import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { MdbModalRef, MdbModalService } from 'mdb-angular-ui-kit/modal';
import { CreateListItemDto } from 'src/app/models/createListItemDto';
import { Importance, RepeatFrequency } from 'src/app/models/global/enums';
import { ListService } from 'src/app/services/list-service.service';

@Component({
  selector: 'app-createlistmodal',
  templateUrl: './createlistmodal.component.html',
  styleUrls: ['./createlistmodal.component.css'],
})
export class CreateListModalComponent implements OnInit {
  // @ts-ignore
  modalRef: MdbModalRef<any>;
  constructor(
    private modalService: MdbModalService,
    private listService: ListService,
    private fb: FormBuilder
  ) {}

  frequencies = Object.values(RepeatFrequency).filter((value) =>
    isNaN(parseInt(value as string))
  );
  importanceLevels = Object.values(Importance).filter((value) =>
    isNaN(parseInt(value as string))
  );

  getLocaleDateStringCurrentTimezone = () => {
    const tzoffset = new Date().getTimezoneOffset() * 60000; //offset in milliseconds
    const localISOTime = new Date(Date.now() - tzoffset)
      .toISOString()
      .slice(0, -8);
    return localISOTime;
  };
  createListItemForm = this.fb.group({
    dueDate: [this.getLocaleDateStringCurrentTimezone()],
    isAllDay: [false],
    title: ['New Task'],
    description: [''],
    frequency: [RepeatFrequency.Weekly],
    importance: [Importance.Normal],
    listTypeName: [''],
    isNewListType: [false],
  });

  // dueDate: Date;
  // isAllDay: boolean;
  // title: string;
  // description: string;
  // frequency: RepeatFrequency;
  // importance: Importance;
  // listTypeName: string;
  // isNewListType: boolean;
  ngOnInit(): void {}

  ngOnChanges() {}

  save = () => {
    const dto = new CreateListItemDto();
    dto.dueDate = this.createListItemForm.value.dueDate
      ? new Date(Date.parse(this.createListItemForm.value.dueDate))
      : new Date();
    dto.isAllDay = this.createListItemForm.value.isAllDay || false;
    dto.title = this.createListItemForm.value.title || '';
    dto.description = this.createListItemForm.value.description || '';
    dto.frequency =
      this.createListItemForm.value.frequency || RepeatFrequency.Weekly;
    dto.importance =
      this.createListItemForm.value.importance || Importance.Normal;
    dto.listTypeName = this.createListItemForm.value.listTypeName || 'Default';
    dto.isNewListType = this.createListItemForm.value.isNewListType || false;

    this.listService.create(dto).subscribe((res) => {
      console.log(res);
    });
  };
}
