using System.Collections.Generic;


namespace WebServer
{
    class JsonData
    {
        public  List<string> Users { get; set; }

        public override string ToString()
        {
            return ($"Invited to the party:{string.Join("\n", Users.ToArray())}");
        }     
    }
}