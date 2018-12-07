using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Stemmer.Cvb;

namespace EdgeDetection.Edge
{
  public class EdgeViewModel : PropertyChangedBase
  {
    public EdgeViewModel()
    {
      Image = Image.FromFile(@"C:\Users\jsiedersberger\Pictures\Camera Roll\001.jpg").Edge();
    }

    private Image _image;
    public Image Image
    {
      get
      {
        return _image;
      }
      set
      {
        _image = value;
        NotifyOfPropertyChange(() => Image);
      }
    }
  }
}
