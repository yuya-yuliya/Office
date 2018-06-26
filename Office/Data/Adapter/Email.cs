using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Office
{
  [Serializable, DataContract]
  public class Email
  {
    public Email()
    {
      emailAddr = "";
    }

    public Email(string email)
    {
      EmailAddr = email;
    }

    [DataMember]
    private string emailAddr;
    public string EmailAddr
    {
      get
      {
        return emailAddr;
      }
      set
      {
        emailAddr = value;
      }
    }

    public override string ToString()
    {
      return EmailAddr;
    }

    public string MessageAnswer()
    {
      return "Response sent";
    }
  }
}
