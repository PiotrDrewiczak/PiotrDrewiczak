import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { PolygonService } from './service/polygon.service';
import * as d3 from 'd3';
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

  @ViewChild('svg', { static: true }) svg!: ElementRef<SVGElement>;

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
    const svg = d3.select(this.svg.nativeElement);
  
    // Clear previous polygons
    svg.selectAll("*").remove();
  
    if (this.selectedPolygon && this.selectedPolygon.length > 0) {
      // Find min and max coordinates
      const minX = d3.min(this.selectedPolygon, d => d.x);
      const maxX = d3.max(this.selectedPolygon, d => d.x);
      const minY = d3.min(this.selectedPolygon, d => d.y);
      const maxY = d3.max(this.selectedPolygon, d => d.y);
  
      // Calculate scale factors
      const scaleX = d3.scaleLinear().domain([minX!, maxX!]).range([0, 500]);
      const scaleY = d3.scaleLinear().domain([minY!, maxY!]).range([500, 0]);
  
      // Create line function
      const lineFunction = d3.line<Point>()
        .x(d => scaleX(d.x))
        .y(d => scaleY(d.y))
        .curve(d3.curveLinearClosed);
  
      // Draw polygon
      svg.append("path")
        .attr("d", lineFunction(this.selectedPolygon))
        .attr("stroke", "black")
        .attr("stroke-width", 2)
        .attr("fill", "none");
    }
  }
}