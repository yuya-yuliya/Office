using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Office
{
  /// <summary>
  /// Класс представляющий список сотрудников (реализован как Singelton)
  /// </summary>
  [Serializable, DataContract]
  public class WorkersList
  {
    /// <summary>
    /// Список сотрудников
    /// </summary>
    [DataMember]
    public List<Employee> Workers { get; set; }

    private static readonly WorkersList instance = new WorkersList();

    static WorkersList() { }

    /// <summary>
    /// Инициализация экземпляра класса WorkersList
    /// </summary>
    private WorkersList()
    {
      Workers = new List<Employee>();
    }

    public static WorkersList Instance
    {
      get
      {
        return instance;
      }
    }

    /// <summary>
    /// Добавление сотрудника в список
    /// </summary>
    /// <param name="worker">Сотрудник</param>
    public void AddWorker(Employee worker)
    {
      if (!Workers.Contains(worker))
      {
        if (worker is SupportWorker)
        {
          if (!CheckPersonalId((SupportWorker)worker))
          {
            throw new Exception("Worker`s ID number was already used");
          }
        }
        else if (worker is ShopAssistant)
        {
          if (!CheckPersonalId((ShopAssistant)worker))
          {
            throw new Exception("Worker`s personal ID was already used");
          }
        }
        Workers.Add(worker);
      }
    }

    /// <summary>
    /// Удаление сотрудника из списка
    /// </summary>
    /// <param name="worker">Сотрудник</param>
    public void DeleteWorker(Employee worker)
    {
      if (Workers.Contains(worker))
      {
        Workers.Remove(worker);
      }
    }

    /// <summary>
    /// Проверка персонального идентификатора работника поддержки
    /// </summary>
    /// <param name="employee">Сотрудник</param>
    /// <returns></returns>
    private bool CheckPersonalId(SupportWorker employee)
    {
      for (int i = 0; i < Workers.Count; i++)
      {
        if (Workers[i] is SupportWorker)
        {
          var worker = (SupportWorker)Workers[i];
          if (worker.IdNumber == employee.IdNumber)
          {
            return false;
          }
        }
      }
      return true;
    }

    /// <summary>
    /// Проверка персонального идентификатора консультанта
    /// </summary>
    /// <param name="employee">Сотрудник</param>
    /// <returns></returns>
    private bool CheckPersonalId(ShopAssistant employee)
    {
      for (int i = 0; i < Workers.Count; i++)
      {
        if (Workers[i] is ShopAssistant)
        {
          var worker = (ShopAssistant)Workers[i];
          if (worker.PersonalId == employee.PersonalId)
          {
            return false;
          }
        }
      }
      return true;
    }

    /// <summary>
    /// Индексация
    /// </summary>
    /// <param name="i">Номер сотрудника в списке</param>
    /// <returns></returns>
    public Employee this[int i]
    {
      get
      {
        return this.Workers[i];
      }
    }
  }
}
