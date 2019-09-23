using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;


namespace WebServer
{
    class Vote
    {
        public string Post(HttpListenerRequest request,HttpListenerResponse response)
        {
            string pairs = "";
            if (request.HttpMethod == "GET")
            {
                using (StreamReader fstream = new StreamReader(@"vote.html"))
                {
                    string content = fstream.ReadToEnd();
                    byte[] buffer = Encoding.UTF8.GetBytes(content);
                    response.ContentLength64 = buffer.Length;
                    Stream output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);
                    output.Close();
                }
            }
            else
            {
                var stream = request.InputStream;              
                var enc = Encoding.UTF8;
                using (var reader = new StreamReader(stream, enc))
                    pairs = reader.ReadToEnd();
                pairs = pairs.Remove(0, 5);
                int ind = pairs.Length - 10;
                pairs = pairs.Remove(ind);    
            }
            Console.WriteLine(pairs);
            return pairs;
        }

    }
}
