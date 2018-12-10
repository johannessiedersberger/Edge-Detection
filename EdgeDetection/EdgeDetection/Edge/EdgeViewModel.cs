using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Stemmer.Cvb;
using Stemmer.Cvb.Wpf;

namespace EdgeDetection.Edge
{
  public class EdgeViewModel : PropertyChangedBase
  {
    public EdgeViewModel()
    {
      Image img = Image.FromFile(@"C:\Users\jsiedersberger\Pictures\Camera Roll\001.jpg");
      Image = img;
      int[] image = img.Edge().LineValues(300);
      var distances = image.Edges().Distances();
      LinePixelValues = new int[][] { image };
      Distances = new ObservableCollection<PixelDistanceView>();
      foreach (int d in distances)
        Distances.Add(new PixelDistanceView() { Distance = d });
   
    }


    private ObservableCollection<PixelDistanceView> _distances;
    public ObservableCollection<PixelDistanceView> Distances
    {
      get
      {
        return _distances;
      }
      set
      {
        _distances = value;
        NotifyOfPropertyChange(() => Distances);
      }
    }
    
    private int[][] _lineData;

    public int[][] LinePixelValues
    {
      get
      {
        return _lineData;
      }
      set
      {
        _lineData = value;
        NotifyOfPropertyChange(() => LinePixelValues);
      }
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
