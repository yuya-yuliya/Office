using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using System.Runtime.Serialization;

namespace Office
{
  /// <summary>
  /// Класс представляющий номер телефона
  /// </summary>
  [Serializable, DataContract]
  public class PhoneNumber : ICallee
  {
    /// <summary>
    /// Инициализация экземпляра класса PhoneNumber
    /// </summary>
    public PhoneNumber()
    {
      number = "";
    }

    /// <summary>
    /// Инициализация экземпляра класса PhoneNumber
    /// </summary>
    /// <param name="number">Номер телефона</param>
    public PhoneNumber(string number)
    {
      Number = number;
    }

    [DataMember]
    private string number;
    /// <summary>
    /// Строковое представление номера телефона
    /// </summary>
    public string Number
    {
      get
      {
        return number;
      }
      set
      {
        Regex regEx = new Regex("^[0-9]+$");
        if (regEx.IsMatch(value))
        {
          number = value;
        }
        else
        {
          throw new ArgumentException("Phone number must consists of only digits");
        }
      }
    }

    /// <summary>
    /// Преобразование в строку
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      return Number;
    }

    public string CallAnswer()
    {
      return "Call finished";
    }
  }
}
