using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using EdgeDetection.Edge;

namespace EdgeDetection
{
  public class HelloBootstrapper : BootstrapperBase
  {
    public HelloBootstrapper()
    {
      Initialize();
    }

    protected override void OnStartup(object sender, StartupEventArgs e)
    {
      base.OnStartup(sender, e);
      DisplayRootViewFor<EdgeViewModel>();
    }
  }
}
