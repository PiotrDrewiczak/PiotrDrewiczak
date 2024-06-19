import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import {PolygonAndRectangleResult } from '../models/polygon-and-rectangle-result.model';

@Injectable({
  providedIn: 'root'
})

export class PolygonService {

  private apiUrl = 'http://localhost:5136/polygon';

  constructor(private http: HttpClient) { }

  getPolygonAndRectangleData(): Observable<PolygonAndRectangleResult> {
    return this.http.get<PolygonAndRectangleResult>(this.apiUrl);
  }
}