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

  labelMapping: { [key: string]: string } = {
    'Road Construction 1': 'Project Name',
    'Construction Count': 'Construction Count',
    'Is Construction Completed': 'Is Construction Completed',
    'Road Length (KM)': 'Length of the road'
  };

  constructor(private dataService: DataService) {}

  ngOnInit(): void {
    this.dataService.getData().subscribe(res => {
      console.log('API Response:', res);

      if (Array.isArray(res)) {
        const flattened: any[] = [];

        res.forEach((observation: any) => {
          if (Array.isArray(observation.datas)) {
            observation.datas.forEach((data: any) => {
              const obj: any = {
                SamplingTime: data.samplingTime
              };

              if (Array.isArray(data.properties)) {
                data.properties.forEach((prop: any) => {
                  const friendlyLabel = this.labelMapping[prop.value] || prop.value;
                  obj[friendlyLabel] = prop.label;
                });
              }

              flattened.push(obj);
            });
          }
        });

        this.data = flattened;
        console.log('Flattened data:', this.data);
      } else {
        console.warn('Unexpected response format:', res);
      }
    });
  }
}
