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

    public unsafe static void Distances(this int[] pixelLineValues)
    {
      int threshold = 50;

      var positionsOverThreshold = new List<Dictionary<int,int>>();
      positionsOverThreshold.Add(new Dictionary<int,int>());

      int currentListIndex = 0;
      for (int i = 0; i < pixelLineValues.Length; i++)
      {
        if(IsOverThreshold(pixelLineValues[i], threshold))
        {
          if (positionsOverThreshold[positionsOverThreshold.Count() - 1].Count() != 0 &&
            NewEdge(lastIndex: positionsOverThreshold[positionsOverThreshold.Count()-1].Last().Key, newIndex: i))
          {
            currentListIndex++;
            positionsOverThreshold.Add(new Dictionary<int, int>());
            positionsOverThreshold[currentListIndex].Add(i, pixelLineValues[i]);
          }
          else
          {
            positionsOverThreshold[currentListIndex].Add(i, pixelLineValues[i]);
          }
        }
      }
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
