import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { PolygonService } from './service/polygon.service';
import { Point } from './models/point.model';

@Component({
  selector: 'app-polygon',
  templateUrl: './polygon.component.html',
  styleUrls: ['./polygon.component.css']
})
export class PolygonComponent implements OnInit {
  polygons!: Point[][];
  selectedPolygonIndex: number = 0;
  selectedPolygon!: Point[];

  @ViewChild('canvas', { static: true }) canvas!: ElementRef<HTMLCanvasElement>;

  constructor(private polygonService: PolygonService) { }

  ngOnInit(): void {
    this.polygonService.getPolygons().subscribe(polygons => {
      this.polygons = polygons;
      this.selectedPolygon = polygons[0];
      this.drawPolygon();
    });
  }

  selectPolygon(): void {
    this.selectedPolygon = this.polygons[this.selectedPolygonIndex];
    this.drawPolygon();
  }

  private drawPolygon(): void {
    const ctx = this.canvas.nativeElement.getContext('2d');
    if(ctx){
      ctx.clearRect(0, 0, this.canvas.nativeElement.width, this.canvas.nativeElement.height);

      if (this.selectedPolygon && this.selectedPolygon.length > 0) {
        ctx.beginPath();
        ctx.moveTo(this.selectedPolygon[0].x, this.selectedPolygon[0].y);
        for (let i = 1; i < this.selectedPolygon.length; i++) {
          ctx.lineTo(this.selectedPolygon[i].x, this.selectedPolygon[i].y);
        }
        ctx.closePath();
        ctx.lineWidth = 2;
        ctx.strokeStyle = '#000';
        ctx.stroke();
      }
    }
  }
}
