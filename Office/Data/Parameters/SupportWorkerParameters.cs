using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office
{
  public class SupportWorkerParameters : EmployeeParameters, IParameters<SupportWorker>
  {
    public List<Parameter> getParameters(SupportWorker obj)
    {
      if (obj.Name == "")
      {
        obj = new SupportWorker();
      }
      List<Parameter> paramList = base.getParameters(obj);
      paramList.Add(new Parameter("Work phone number", Parameter.Types.TextBox, 1).setStartValues(new string[] { obj.WorkPhoneNumber.ToString() }));
      paramList.Add(new Parameter("Work email", Parameter.Types.TextBox, 1).setStartValues(new string[] { obj.EmailAddr.ToString() }));
      paramList.Add(new Parameter("ID number", Parameter.Types.TextBox, 1).setStartValues(new string[] { obj.IdNumber.ToString() }));

      return paramList;
    }

    public void setParameters(List<Parameter> paramList, SupportWorker prototype)
    {
      if (prototype == null)
      {
        throw new ArgumentNullException("Prototype mustn't be null");
      }
      ParseEmployeeParam(paramList, prototype);
      Parameter param = paramList.Find(i => i.Name == "Work phone number");
      if (param != null)
      {
        prototype.WorkPhoneNumber = new PhoneNumber(param.ReturnValues[0]);
      }
      param = paramList.Find(i => i.Name == "ID number");
      if (param != null)
      {
        prototype.IdNumber = int.Parse(param.ReturnValues[0]);
      }
      param = paramList.Find(i => i.Name == "Work email");
      if (param != null)
      {
        prototype.EmailAddr = new Email(param.ReturnValues[0]);
      }
    }
  }
}
