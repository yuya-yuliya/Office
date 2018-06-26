using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office
{
  public class EmailAdapter : ICallee
  {
    private Email email;

    public EmailAdapter(Email email)
    {
      this.email = email;
    }

    public string CallAnswer()
    {
      return email.MessageAnswer();
    }
  }
}
