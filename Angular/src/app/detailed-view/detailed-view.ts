import { Component } from '@angular/core';
import { DataService } from '../data.service';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-detailed-view',
  standalone: false,
  templateUrl: './detailed-view.html',
  styleUrl: './detailed-view.scss'
})
export class DetailedView {

  data: any[] = [];
  selected: any;
  form!: FormGroup;
  selectedProperties : any;

  constructor(private dataService: DataService,private fb: FormBuilder) {}

   ngOnInit(): void {
     this.dataService.getData().subscribe(result => {
       this.data = result.datas;
       if (!this.selected) {
        this.onSelect(this.data[0]);
      }
    });
  }

  onSelect(item: any) {
    this.selected = item;
    this.selectedProperties = item.Properties;

    const group: any = {};
    item.Properties.forEach((prop: any) => {
      console.log("prop", prop);

      group[prop.Label] = [prop.Value];
    });

    console.log("group",group);
    
    this.form = this.fb.group(group);
  }

isTextField(value: any): boolean {
  return typeof value === 'string';
}

isNumberField(value: any): boolean {
  return typeof value === 'number';
}

isBooleanField(value: any): boolean {
  return typeof value === 'boolean';
}
onSave() {
  const updated = this.form.value;

  this.selected.Properties.forEach((prop: any) => {
    prop.Value = updated[prop.Label];
  });

  this.dataService.updateData({ datas: this.data }).subscribe(result => {
    console.log('Update success', result);

    window.location.reload();
  });
}
}
