using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office.Plugins
{
  public interface ITransformPlugin
  {
    string Ext { get; }

    string Name { get; }
    void Transform(List<Employee> workers, string filePath);
    List<Employee> Detransform(string filePath);
  }
}
