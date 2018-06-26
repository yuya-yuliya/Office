using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office
{
  public interface IParameters<T>
  {
    List<Parameter> getParameters(T obj);

    void setParameters(List<Parameter> paramList, T prototype);
  }
}
