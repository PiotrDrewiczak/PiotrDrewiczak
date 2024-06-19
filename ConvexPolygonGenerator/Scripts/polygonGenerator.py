import sys
import json
import numpy as np
import largestinteriorrectangle as lir
from polygenerator import random_polygon
import random

def main(num_polygons, num_points, output_file):
    # this is just so that you can reproduce te same results
    random.seed()

    polygons = []
    for _ in range(num_polygons):
        polygon = random_polygon(num_points=num_points)
        polygons.append(polygon)

    scale_factor = 1000
    polygons_int = [
        np.array(
            [(int(x * scale_factor), int(y * scale_factor)) for x, y in polygon], 
            dtype=np.int32
        )
        for polygon in polygons
    ]

    rectangles = []
    for polygon in polygons_int:
        try:
            rectangle = lir.lir(np.array([polygon], dtype=np.int32))
            rectangles.append(rectangle.tolist())
        except Exception as e:
            print(f"Error finding largest inscribed rectangle: {e}")
            rectangles.append([])

    # Zapisz wynik w pliku JSON
    with open(output_file, 'w') as f:
        json.dump({
            "polygons": [polygon.tolist() for polygon in polygons_int],
            "rectangles": rectangles
        }, f)

    print(f"{num_polygons} polygons data written to {output_file}")

if __name__ == "__main__":
    num_polygons = int(sys.argv[1])
    num_points = int(sys.argv[2])
    output_file = sys.argv[3]
    main(num_polygons, num_points, output_file)