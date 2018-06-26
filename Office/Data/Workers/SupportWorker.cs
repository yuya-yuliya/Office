using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;

namespace Office
{
  /// <summary>
  /// Класс представляющий сотрудника, занимающего должность Работник поддержки
  /// </summary>
  [Serializable, DataContract]
  public class SupportWorker : Employee, IParametrizable
  {
    /// <summary>
    /// Инициализирует экземпляр класса SupportWorker
    /// </summary>
    public SupportWorker()
      :base()
    {
      IdNumber = 0;
      WorkPhoneNumber = new PhoneNumber();
      EmailAddr = new Email();
    }

    /// <summary>
    /// Инициализация экземпляра класса SupportWorker
    /// </summary>
    /// <param name="name">Имя</param>
    /// <param name="surname">Фамилия</param>
    /// <param name="birthDay">День рождения</param>
    /// <param name="number">Номер телефона(личный)</param>
    /// <param name="period">Период работы</param>
    /// <param name="workNumber">Номер телефона(рабочий)</param>
    /// <param name="id">Идентификационный номер</param>
    public SupportWorker(string name, string surname, DateTime birthDay, PhoneNumber number, Tuple<DateTime, DateTime> period, PhoneNumber workNumber, int id)
      : base(name, surname, birthDay, number, period)
    {
      IdNumber = id;
      WorkPhoneNumber = number;
      EmailAddr = new Email();
    }

    public SupportWorker(string name, string surname, DateTime birthDay, PhoneNumber number, Tuple<DateTime, DateTime> period, Email email, int id)
      : base(name, surname, birthDay, number, period)
    {
      IdNumber = id;
      EmailAddr = email;
      WorkPhoneNumber = new PhoneNumber();
    }

    public SupportWorker(string name, string surname, DateTime birthDay, PhoneNumber number, Tuple<DateTime, DateTime> period, PhoneNumber workNumbet, Email email, int id)
      : base(name, surname, birthDay, number, period)
    {
      IdNumber = id;
      WorkPhoneNumber = number;
      EmailAddr = email;
    }

    /// <summary>
    /// Идентификационный номер
    /// </summary>
    [DataMember]
    public int IdNumber { get; set; }
    /// <summary>
    /// Номер рабочего телефона
    /// </summary>
    [DataMember]
    public PhoneNumber WorkPhoneNumber { get; set; }
    [DataMember]
    public Email EmailAddr { get; set; }

    /// <summary>
    /// Преобразование в строку
    /// </summary>
    /// <returns>Для не заданного сотрудника возвращает название класса, инача - Имя Фамилия</returns>
    public override string ToString()
    {
      if (Name != "")
      {
        return base.ToString();
      }
      else
      {
        return "Support worker";
      }
    }

    public override string Working()
    {
      string workResult = "";
      if (EmailAddr != null && EmailAddr.EmailAddr != "")
      {
        workResult += Answer(new EmailAdapter(EmailAddr)) + " ";
      }
      if (WorkPhoneNumber != null && WorkPhoneNumber.Number != "")
      {
        workResult += Answer(WorkPhoneNumber);
      }

      return workResult;
    }

    public string Answer(ICallee call)
    {
      return call.CallAnswer();
    }

    public override List<Parameter> GetParam(ControlParameters control)
    {
      return control.GetParam(this);
    }

    public override void SetParam(ControlParameters control, List<Parameter> param)
    {
      control.SetParam(param, this);
    }
  }
}
