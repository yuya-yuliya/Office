using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;

namespace Office
{
  /// <summary>
  /// Класс представляющий сотрудника, занимающего должность Консультанта в магазине
  /// </summary>
  [Serializable, DataContract]
  public class ShopAssistant : Employee, IParametrizable
  {
    /// <summary>
    /// Инициализация экземпляра класса ShopAssistant
    /// </summary>
    public ShopAssistant()
      : base()
    {
      PersonalId = 0;
      Department = "";
    }

    /// <summary>
    /// Инициализация экземпляра класса ShopAssistant
    /// </summary>
    /// <param name="name">Имя</param>
    /// <param name="surname">Фамилия</param>
    /// <param name="birthDay">День рождения</param>
    /// <param name="number">Номер телефона(личный)</param>
    /// <param name="period">Период работы</param>
    /// <param name="personalId">Персональный идентификатор</param>
    /// <param name="department">Название отдела работы</param>
    public ShopAssistant(string name, string surname, DateTime birthDay, PhoneNumber number, Tuple<DateTime, DateTime> period, uint personalId, string department) 
      : base(name, surname, birthDay, number, period)
    {
      PersonalId = personalId;
      Department = department;
    }

    /// <summary>
    /// Персональный идентификатор
    /// </summary>
    [DataMember]
    public uint PersonalId { get; set; }

    /// <summary>
    /// Название отдела работы
    /// </summary>
    [DataMember]
    public string Department { get; set; }

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
        return "Shop assistant";
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
