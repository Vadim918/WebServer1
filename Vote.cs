using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebServer
{
    class Vote
    {
        public void Post(HttpListener listener, HttpListenerRequest req)
        {
            HttpListenerContext context = listener.GetContext();
            HttpListenerRequest request = context.Request;
            var response = context.Response;

            if (req.HttpMethod == "POST")
            {
                using (Stream stream = request.InputStream)
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        Console.WriteLine(reader.ReadToEnd());
                    }
                }

            }
            

        }
    }
}
