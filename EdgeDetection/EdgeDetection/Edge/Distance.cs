using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stemmer.Cvb;

namespace EdgeDetection.Edge
{
  public static class Distance
  {
    public unsafe static int[] LineValues(this Image image, int line)
    {
      if (line < 0 || line > image.Height)
        throw new ArgumentException("That line is not possible");

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

      return lineValues;
    }

    public static List<int> Distances(this List<List<int>> edges)
    {
      List<int> edgePositions = new List<int>();
      foreach(var list in edges)
      {
        edgePositions.Add(Middle(list));
      }
      List<int> distances = new List<int>();
      for (int i = 0; i < edgePositions.Count(); i++)
      {
        if (i == 0)
        {
          distances.Add(edgePositions[i]-0);
        }
        else
        {
          distances.Add(edgePositions[i] - edgePositions[i-1]);
        }
      }
      return distances;
    }

    private static int Middle(List<int> positions)
    {
      return positions[positions.Count / 2];
    }

    public unsafe static List<List<int>> Edges(this int[] pixelLineValues)
    {
      int threshold = 50;

      var positionsOverThreshold = new List<List<int>>();
      positionsOverThreshold.Add(new List<int>());

      int currentListIndex = 0;
      for (int i = 0; i < pixelLineValues.Length; i++)
      {
        if(IsOverThreshold(pixelLineValues[i], threshold))
        {
          if (positionsOverThreshold[positionsOverThreshold.Count() - 1].Count() != 0 &&
            NewEdge(lastIndex: positionsOverThreshold[positionsOverThreshold.Count()-1].Last(), newIndex: i))
          {
            positionsOverThreshold.Add(new List<int>());
            positionsOverThreshold[++currentListIndex].Add(i);
          }
          else
          {
            positionsOverThreshold[currentListIndex].Add(i);
          }
        }
      }
      return positionsOverThreshold;
    }

    private static bool IsOverThreshold(int valueToCheck, int threshold)
    {
      return valueToCheck > threshold;
    }

    private static bool NewEdge(int lastIndex, int newIndex)
    {
      return newIndex - lastIndex != 1;
    }
  }
}
