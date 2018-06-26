using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office
{
  public class EconomistParameters : EmployeeParameters, IParameters<Economist>
  {
    public List<Parameter> getParameters(Economist obj)
    {
      if (obj.Name == "")
      {
        obj = new Economist();
      }
      List<Parameter> paramList = base.getParameters(obj);
      paramList.Add(new Parameter("Office number", Parameter.Types.TextBox, 1).setStartValues(new string[] { obj.OfficeNumber.ToString() }));

      return paramList;
    }

    public void setParameters(List<Parameter> paramList, Economist prototype)
    {
      if (prototype == null)
      {
        throw new ArgumentNullException("Prototype mustn't be null");
      }
      ParseEmployeeParam(paramList, prototype);
      Parameter param = paramList.Find(i => i.Name == "Office number");
      if (param != null)
      {
        prototype.OfficeNumber = uint.Parse(param.ReturnValues[0]);
      }
    }
  }
}
