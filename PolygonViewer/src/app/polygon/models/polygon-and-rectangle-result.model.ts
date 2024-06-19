import { Point } from "./point.model";
import { Rectangle } from "./rectangle.model";

export interface PolygonAndRectangleResult {
    polygons: Point[][];
    rectangles: Rectangle[];
  }