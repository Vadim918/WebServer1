using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebServer
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpListener listener = new HttpListener();
            string index = "http://*:8881/";
            listener.Prefixes.Add(index);
            var json = JsonData.DataObject();
            Vote vote = new Vote();

            while (true)
            {
                Console.WriteLine("Ожидание подключений...");
                listener.Start();

                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;

                if (request.RawUrl.Contains("/participants.html"))
                {
                    string create = JsonConvert.SerializeObject(json);
                    File.WriteAllText(@"participants.json", create);
                    create = File.ReadAllText(@"participants.json");
                    JsonData resultJson = JsonConvert.DeserializeObject<JsonData>(create);
                    string content = resultJson.ToString();
                    byte[] buffer = System.Text.Encoding.Default.GetBytes(content);
                    response.ContentLength64 = buffer.Length;
                    Stream output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);
                    output.Close();
                }
                else if (request.RawUrl.Contains("/vote.html"))
                {
                    using (StreamReader fstream = new StreamReader(@"vote.html"))
                    {
                        string content = fstream.ReadToEnd();
                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(content);
                        response.ContentLength64 = buffer.Length;
                        Stream output = response.OutputStream;
                        output.Write(buffer, 0, buffer.Length);
                        vote.Post(listener, request);
                        output.Close();
                    }
                }
                else
                {
                    using (StreamReader fstream = new StreamReader(@"index.html"))
                    {
                        string content = fstream.ReadToEnd();
                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(content);
                        response.ContentLength64 = buffer.Length;
                        Stream output = response.OutputStream;
                        output.Write(buffer, 0, buffer.Length);
                        output.Close();
                    }
                }
                listener.Stop();
                Console.WriteLine("Обработка подключений завершена");
            }
        }
    }
}
