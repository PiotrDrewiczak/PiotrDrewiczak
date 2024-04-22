import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Point } from '../models/point.model';


@Injectable({
  providedIn: 'root'
})
export class PolygonService {

  constructor(private http: HttpClient) { }

  getPolygons(): Observable<Point[][]> {
    return this.http.get<Point[][]>('http://localhost:5136/polygon');
  }
}