using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office
{
  public class DismissCommand : ICommand
  {
    private Employee employee;

    public DismissCommand(Employee employee)
    {
      this.employee = employee;
    }

    public void Execute()
    {
      employee.Status.NowStatus = WorkStatus.Status.Dismiss;
    }

    public void Undo()
    {
      employee.Status.NowStatus = WorkStatus.Status.Work;
    }
  }
}
