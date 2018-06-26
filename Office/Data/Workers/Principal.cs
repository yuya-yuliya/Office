using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;

namespace Office
{
  /// <summary>
  /// Класс представляющий сотрудника, занимающего должность Директор
  /// </summary>
  [Serializable, DataContract]
  public class Principal : Manager<Employee>, IParametrizable
  {
    /// <summary>
    /// Инициализация экземпляра класса Principal
    /// </summary>
    public Principal()
      :base()
    {
      DepartmentName = "General";
      WorkPhoneNumber = new PhoneNumber();
    }

    /// <summary>
    /// Инициализация экземпляра объекта Principal
    /// </summary>
    /// <param name="name">Имя</param>
    /// <param name="surname">Фамилия</param>
    /// <param name="birthDay">День рождения</param>
    /// <param name="number">Номер телефона(дичный)</param>
    /// <param name="period">Период работы</param>
    /// <param name="workNumber">Рабочий номер телефона</param>
    public Principal(string name, string surname, DateTime birthDay, PhoneNumber number, Tuple<DateTime, DateTime> period, PhoneNumber workNumber)
      : base(name, surname, birthDay, number, period, "General")
    {
      WorkPhoneNumber = workNumber;
    }

    /// <summary>
    /// Рабочий номер телефона
    /// </summary>
    [DataMember]
    public PhoneNumber WorkPhoneNumber { get; set; }

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
        return "Principal";
      }
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
