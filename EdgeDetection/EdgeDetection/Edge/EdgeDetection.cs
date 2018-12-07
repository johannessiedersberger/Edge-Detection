using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stemmer.Cvb;

namespace EdgeDetection.Edge
{
  public static class EdgeDetection
  {
    public static Image Edge(this Image image)
    {
      var size = image.Size;

      var result = new Image(size);


      result.Initialize(0);

      var startImage = image.Planes[0].GetLinearAccess<byte>();
      var endImage = result.Planes[0].GetLinearAccess<byte>();

      unsafe
      {
        var startImageBase = (byte*)startImage.BasePtr;
        var endImageBase = (byte*)endImage.BasePtr;

        var startImageXInc = startImage.XInc.ToInt32();
        var startImageYInc = startImage.YInc.ToInt32();

        var endImageXInc = endImage.XInc.ToInt32();
        var endImageYInc = endImage.YInc.ToInt32();

        var lines = stackalloc byte*[3];
        var columns = stackalloc byte*[3];

        for (int y = 1; y < (size.Height - 1); y++)
        {
          for (int i = -1; i < 2; i++)
            lines[i + 1] = startImageBase + (y + i) * startImageYInc;
          var endImageLine = endImageBase + y * endImageYInc;

          for (int x = 1; x < (size.Width - 1); x++)
          {
            var offset = (x - 1) * startImageXInc;
            for (int i = 0; i < 3; i++)
              columns[i] = lines[i] + offset;

            int acc = -2 * *columns[0] - (*columns[0] + startImageXInc)
                      - *columns[1] + *(columns[1] + 2 * startImageXInc)
                      + *(columns[2] + startImageXInc) + 2 * *(columns[2] + 2 * startImageXInc);

            *(endImageLine + x * endImageXInc) = (byte)(Math.Abs(acc / 3));
          }

        }
        return result;
      }


    }
  }
}

