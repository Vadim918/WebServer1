using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServer
{
    class JsonData
    {
        public List<string> Users { get; set; }

        public override string ToString()
        {
            return ($"Приглашённые на вееринку:{string.Join("\n", Users.ToArray())}");
        }
        public static object DataObject()
        {
            JsonData json = new JsonData()
            {
                Users = new List<string>
                {
                    "Oleg",
                    "Vadim",
                    "Sasha",
                    "Dima",
                    "James",



                }
            };
            return json;
        }
    }
}
