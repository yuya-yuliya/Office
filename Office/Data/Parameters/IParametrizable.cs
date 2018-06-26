using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office
{
  interface IParametrizable: IEmployee
  {
    List<Parameter> GetParam(ControlParameters control);

    void SetParam(ControlParameters control, List<Parameter> param);
  }
}
