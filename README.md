# Edge-Detection
This application is used for calculating the distances between all edges in a certain row
### Start Image
We use this image as an example <br />
![StartImage](http://johannessiedersberger.com/edge_detection_start_image)
### Calculating Button
Input the Line that you want to use for the calculation into the text field. Than press the calculate button to calculate the distances between the edges <br />
![CalculateDistancesButton](http://johannessiedersberger.com/calculate_distances_button/)
### Distance Calculation
To calculate the distances I count the pixels between every edge. An Edge is recognizable because of its higher color-values.
### Distance Calculation Algorithm
```
      ImagePlane plane = image.Planes[0];

      var size = plane.Parent.Size;
      var access = plane.GetLinearAccess();

      var xInc = access.XInc.ToInt64();
      var yInc = access.YInc.ToInt64();
      var pBase = (byte*)access.BasePtr;

      int y = line;
      int[] lineValues = new int[size.Width];
      for (int x = 0; x < size.Width; x++)
      {
        var pPixel = pBase + x * xInc + y * yInc;
        lineValues[x] = *pPixel;
      }
```
### Diagramm
This diagramm shows the grey-scale values (0-255) of the selected line <br />
![Diagramm](http://johannessiedersberger.com/color_values_row/) <br /> <br />
### The Selected Line
This is the line that we selected to calculate the distances between the edges <br />
![Line](http://johannessiedersberger.com/line/)
### End Image
Here i applied the algorithm that makes the edges visible
![EndImage](http://johannessiedersberger.com/edge_detection_end_image)
### Distances Box
The calculated distances are shown here <br />
![DistanceBox](http://johannessiedersberger.com/distances/)
