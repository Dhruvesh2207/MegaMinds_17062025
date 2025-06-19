  import { Component } from '@angular/core';
  import { DataService } from '../data.service';

  @Component({
    selector: 'app-summary-view',
    standalone: false,
    templateUrl: './summary-view.html',
    styleUrl: './summary-view.scss'
  })
  export class SummaryView {
  data: any[] = [];
   displayedColumns: string[] = [
  'SamplingTime',
  'Project Name',
  'Construction Count',
  'Is Construction Completed',
  'Length of the road'
];
  constructor(private dataService: DataService) {}

  ngOnInit(): void {
  this.dataService.getData().subscribe(result => {
    const rawData = result.datas;
    this.data = rawData.map((item: any) => {
      const flat: any = {
        SamplingTime: item.SamplingTime
      };

      item.Properties.forEach((prop: any) => {
        flat[prop.Label] = prop.Value;
      });

      return flat;
    });

    console.log('Table Data:', this.data);
  });
}

}
