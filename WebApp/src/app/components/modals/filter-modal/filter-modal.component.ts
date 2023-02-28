import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MdbModalRef, MdbModalService } from 'mdb-angular-ui-kit/modal';
import { ListType } from 'src/app/models/listType';
import { ListTypeService } from 'src/app/services/listtypeservice.service';

@Component({
  selector: 'app-filter-modal',
  templateUrl: './filter-modal.component.html',
  styleUrls: ['./filter-modal.component.css'],
})
export class FilterModalComponent implements OnInit {
  // @ts-ignore
  modalRef: MdbModalRef<any>;
  constructor(
    private modalService: MdbModalService,
    private listTypeService: ListTypeService,
    private fb: FormBuilder
  ) {}

  listTypes: ListType[] = [];

  filterForm = this.fb.group({
    ignoreCompleted: false,
    listTypeName: 'Default',
    listTypes: [],
  });

  ngOnInit(): void {
    this.listTypeService.getListTypes().subscribe((res) => {
      this.listTypes = res;
    });
  }
  submit = () => {};
  // submit = () =>{
  //   this
  // }
}
