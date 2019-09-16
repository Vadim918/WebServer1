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
            if (req.HttpMethod == "POST")
            {
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;
              

                Stream body = request.InputStream;
                Encoding encoding = request.ContentEncoding;
               StreamReader reader = new System.IO.StreamReader(body, encoding);

                Console.WriteLine("Start of client data:");
                // Convert the data to a string and display it on the console.
                string s = reader.ReadToEnd();
                Console.WriteLine(s);
                Console.WriteLine("End of client data:");
                body.Close();
                reader.Close();
            }
        }
    }
}
