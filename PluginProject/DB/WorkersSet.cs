using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginProject
{
    class WorkersSet
    {
        public virtual List<Worker> Co_Workers { get; set; }
        public WorkersSet()
        {
            CoWorker db = new CoWorker();
            //получаем объекты из бд и записываем их в List
            var CoWork = db.Co_Workers.ToList();

            Worker[] workers = new Worker[CoWork.Count];
            List<Worker> ListOfWorkers = new List<Worker>();

            foreach (var item in CoWork)
            {
                Worker worker = new Worker
                {
                    ID = item.ID,
                    C_Name = item.C_Name,
                    C_Surname = item.C_Surname,
                    C_email = item.C_email,
                    C_Date_of_Birth = item.C_Date_of_Birth,
                    C_adress = item.C_adress
                };
                ListOfWorkers.Add(worker);
            }
            Co_Workers = ListOfWorkers;
        }
    }
}
