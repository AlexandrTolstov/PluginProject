using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchPluginInterface;
using DBConnect;

namespace SearcheByID
{
    public class SearcheByIDClass : SearchInterface
    {
        public string Name { get; }
        public SearcheByIDClass()
        {
            Name = "Поиск по ID";
        }
        public Worker Search(string input)
        {
            WorkersSet db = new WorkersSet();
            var workers = db.Co_Workers.ToList();
            Worker worker = new Worker();

            foreach (var item in workers)
            {
                if (input == item.ID.ToString())
                {
                    worker.ID = item.ID;
                    worker.C_Name = item.C_Name;
                    worker.C_Surname = item.C_Surname;
                    worker.C_email = item.C_email;
                    worker.C_Date_of_Birth = item.C_Date_of_Birth;
                    worker.C_adress = item.C_adress;
                }
            }
            return worker;
        }
    }
}
