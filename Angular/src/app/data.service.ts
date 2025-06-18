import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor(private http: HttpClient) { 
    
  }
   getData(): Observable<any> {
  return this.http.get('https://localhost:7128/api/Data');
}

updateData(updatedData: any): Observable<any> { 
    return this.http.post('https://localhost:7128/api/Data', updatedData);
  }

  

}