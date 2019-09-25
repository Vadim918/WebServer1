using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;


namespace WebServer
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpListener listener = new HttpListener();
            string index = "http://*:8881/";
            listener.Prefixes.Add(index);
            string user = "";            
            Vote vote = new Vote();

            while (true)
            {
                var json = File.Exists("participants.json") ? JsonConvert.DeserializeObject<JsonData>
                       (File.ReadAllText("participants.json")) : new JsonData
                       {

                       };

                Console.WriteLine("Ожидание подключений...");
                listener.Start();

                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;

                if (request.RawUrl.Contains("/participants.html"))
                {                   
                    JsonData resultJson = JsonConvert.DeserializeObject<JsonData>(File.ReadAllText(@"participants.json"));
                    string content = resultJson.ToString();
                    byte[] buffer = System.Text.Encoding.Default.GetBytes(content);
                    response.ContentLength64 = buffer.Length;
                    Stream output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);
                    output.Close();
                }
                else if (request.RawUrl.Contains("/vote.html"))
                {
                    user = vote.Post(request, response);
                    if (request.HttpMethod == "POST")
                    {
                        json.Users.Add(user);
                        File.WriteAllText(@"participants.json", JsonConvert.SerializeObject(json));
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