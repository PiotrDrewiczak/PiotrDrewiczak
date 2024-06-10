import sys
import json
from polygenerator import random_polygon
import random
import os

def main(num_polygons,num_points, output_file):
    # this is just so that you can reproduce the same results
    random.seed()

    polygons = []
    for _ in range(num_polygons):
      polygon = random_polygon(num_points=num_points)
      polygons.append(polygon)

    # Zapisz wynik w pliku JSON
    with open(output_file, 'w') as f:
        json.dump(polygons, f)

    print(f"{num_polygons} polygons data written to {output_file}")

if __name__ == "__main__":
    num_polygons = int(sys.argv[1])
    num_points = int(sys.argv[2])
    output_file = sys.argv[3]
    main(num_polygons, num_points, output_file)