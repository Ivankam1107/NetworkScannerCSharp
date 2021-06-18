using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetworkScanner
{
    class PortScan
    {
        private void Port(object args)
        {
            IPEndPoint iPEndPoint = args as IPEndPoint;
            string ip = iPEndPoint.Address.ToString();
            int port = iPEndPoint.Port;
            var client = new TcpClient();
            var result = client.BeginConnect(ip, port, null, null);
            var success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromMilliseconds(Form1.PortTimeout));
            if (success)
            {
                if (!Form1.CURRENT.ContainsKey(ip)) Form1.CURRENT.Add(ip,new Dictionary<string, SortedSet<dynamic>>());
                try
                {
                    if (!Form1.CURRENT[ip].ContainsKey("PortAlive"))
                    {
                        Form1.CURRENT[ip].Add("PortAlive",new SortedSet<dynamic>());
                    }
                    Form1.CURRENT[ip]["PortAlive"].Add(port);
                }
                catch
                {
                    
                }
                //client.EndConnect(result);
            }
            
        }
        private void Ports(object args)
        {
            string ip = args as string;
            List<Thread> threads = new List<Thread>();
            foreach (int port in Form1.portList)
            {
                IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
                Thread thread = new Thread(Port);
                threads.Add(thread);
                thread.Start(iPEndPoint);
            }
            foreach(Thread thread in threads)
            {
                thread.Join();
            }
            Form1.DisplayTips(ip);
        }
        public PortScan()
        {
            Form1.Status = "Port";
            List<Thread> threads = new List<Thread>();
            foreach (string ip in Form1.CURRENT.Keys)
            {
                Thread thread = new Thread(Ports);
                threads.Add(thread);
                thread.Start(ip);
            }
            Console.WriteLine(JsonConvert.SerializeObject(PingScan.failIPs));
            foreach (string ip in PingScan.failIPs)
            {
                Thread thread = new Thread(Ports);
                threads.Add(thread);
                thread.Start(ip);
            }
            foreach (Thread thread in threads)
            {
                thread?.Join();
            }
            
        }
    }
}
