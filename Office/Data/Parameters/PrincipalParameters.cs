using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office
{
  public class PrincipalParameters : ManagerParameters<Employee>, IParameters<Principal>
  {
    public List<Parameter> getParameters(Principal obj)
    {
      if (obj.Name == "")
      {
        obj = new Principal();
      }
      List<Parameter> paramList = base.getParameters(obj);
      paramList.Remove(paramList.Find(i => i.Name == "Department name"));
      paramList.Add(new Parameter("Work phone number", Parameter.Types.TextBox, 1).setStartValues(new string[] { obj.WorkPhoneNumber.ToString() }));

      return paramList;
    }

    public void setParameters(List<Parameter> paramList,Principal prototype)
    {
      if (prototype == null)
      {
        throw new ArgumentNullException("Prototype mustn't be null");
      }
      base.setParameters(paramList,prototype);

      Parameter param = paramList.Find(i => i.Name == "Work phone number");
      if (param != null)
      {
        prototype.WorkPhoneNumber = new PhoneNumber(param.ReturnValues[0]);
      }
    }
  }
}
