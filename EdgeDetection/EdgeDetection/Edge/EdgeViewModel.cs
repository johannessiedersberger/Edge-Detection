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
      Image = Image.FromFile(@"C:\Users\jsiedersberger\Pictures\Camera Roll\001.jpg");
      SetUIControls();
    }

    private void SetUIControls()
    {
      int[] imageLine = Image.KernelEdge().LineValues(SelectedLine);
      LinePixelValues = new int[][] { imageLine };
      Distances = imageLine.Edges().Distances();
    }


    public void CalculateDistanceButtonClicked()
    {
      SetUIControls();
    }

    private int _selectedLine = 300;
    public int SelectedLine
    {
      get
      {
        return _selectedLine;
      }
      set
      {
        _selectedLine = value;
        NotifyOfPropertyChange(() => SelectedLine);
      }
    }

    private List<int> _distances;
    public List<int> Distances
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
