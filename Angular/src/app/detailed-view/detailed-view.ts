import { Component } from '@angular/core';
import { DataService } from '../data.service';
import { FormBuilder, FormGroup, FormControl } from '@angular/forms';

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
  selectedProperties: any[] = [];

  labelMapping: { [key: string]: string } = {
    'Road Construction 1': 'Project Name',
    'Construction Count': 'Construction Count',
    'Is Construction Completed': 'Is Construction Completed',
    'Length of the road': 'Length of the road'
  };

  constructor(private dataService: DataService, private fb: FormBuilder) {}

  ngOnInit(): void {
    this.dataService.getData().subscribe(result => {
      this.data = result.flatMap((obs: any) => obs.datas || []);

      if (this.data.length > 0 && !this.selected) {
        this.onSelect(this.data[0]);
      }
    });
  }

 onSelect(item: any) {
  this.selected = item;

  this.selectedProperties = (item.properties || []).map((prop: any) => ({
    key: prop.label,          
    label: prop.label,        
    value: prop.value        
  }));
  
  const group: { [key: string]: FormControl } = {};
  this.selectedProperties.forEach(prop => {
    group[prop.key] = new FormControl(prop.value);
  });

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
    this.selected.properties.forEach((prop: any) => {
      prop.value = updated[prop.label];
    });

    this.dataService.updateData(this.data).subscribe(result => {
      console.log('Update success', result);
    });
  }
}
