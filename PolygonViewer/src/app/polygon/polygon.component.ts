import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { PolygonService } from './service/polygon.service';
import * as d3 from 'd3';
import { Point  } from './models/point.model';
import { PolygonAndRectangleResult  } from './models/polygon-and-rectangle-result.model';
import { Rectangle } from './models/rectangle.model';
@Component({
  selector: 'app-polygon',
  templateUrl: './polygon.component.html',
  styleUrls: ['./polygon.component.css']
})
export class PolygonComponent implements OnInit {
  polygons: Point[][] = [];
  rectangles: Rectangle[] = [];
  selectedPolygonIndex: number = 0;
  selectedPolygon: Point[] = [];

  @ViewChild('svg', { static: true }) svg!: ElementRef<SVGElement>;

  constructor(private polygonService: PolygonService) { }

  ngOnInit(): void {
    this.polygonService.getPolygonAndRectangleData().subscribe(data => {
      this.polygons = data.polygons;
      this.rectangles = data.rectangles;
      this.selectedPolygon = this.polygons[this.selectedPolygonIndex];
      this.drawPolygonAndRectangle();
    });
  }

  selectPolygon(): void {
    this.selectedPolygon = this.polygons[this.selectedPolygonIndex];
    this.drawPolygonAndRectangle();
  }

  private drawPolygonAndRectangle(): void {
    const svg = d3.select(this.svg.nativeElement);
    
    // Clear previous polygons and rectangles
    svg.selectAll("*").remove();
    
    if (this.selectedPolygon && this.selectedPolygon.length > 0) {
      // Create line function
      const lineFunction = d3.line<Point>()
        .x(d => d.x)
        .y(d => d.y)
        .curve(d3.curveLinearClosed);

      // Draw polygon
      svg.append("path")
        .attr("d", lineFunction(this.selectedPolygon))
        .attr("stroke", "black")
        .attr("stroke-width", 2)
        .attr("fill", "none");

      // Draw corresponding rectangle if exists
      const correspondingRectangle = this.rectangles[this.selectedPolygonIndex];
      if (correspondingRectangle) {
        svg.append("rect")
          .attr("x", correspondingRectangle.x)
          .attr("y", correspondingRectangle.y)
          .attr("width", correspondingRectangle.width)
          .attr("height", correspondingRectangle.height)
          .attr("stroke", "red")
          .attr("stroke-width", 2)
          .attr("fill", "none");
      }
    }
  }
}