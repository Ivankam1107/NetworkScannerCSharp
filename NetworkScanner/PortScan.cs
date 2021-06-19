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
            string lockO= "";
            using (var client = new TcpClient())
            {
                var result = client.BeginConnect(ip, port, null, null);
                var success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromMilliseconds(Form1.PortTimeout));
                try
                {
                    if (success)
                    {
                        if (Form1.CURRENT.FirstOrDefault(x => x.IP == ip) == null) Form1.CURRENT.Add(new Form1.JsonClass(ip));
                        Form1.CURRENT.First(x => x.IP == ip).AlivePort.Add(port);
                        //client.EndConnect(result);
                    }
                }
                catch
                {

                }
                
            }           
        }
        private void Ports(object args)
        {
            string ip = args as string;
            List<Thread> threads = new List<Thread>();
            Parallel.ForEach(Form1.portList, port =>
            {
                IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
                Thread thread = new Thread(Port);
                threads.Add(thread);
                thread.Start(iPEndPoint);
            });
            foreach (Thread thread in threads)
            {
                thread.Join();
            }
            Form1.DisplayTips(ip);
        }
        public PortScan()
        {
            Form1.Status = "Port";
            List<Thread> threads = new List<Thread>();
            Parallel.ForEach(Form1.CURRENT.Select(x => x.IP).ToList(), ip =>
            {
                Thread thread = new Thread(Ports);
                threads.Add(thread);
                thread.Start(ip);
            });
            foreach (Thread thread in threads)
            {
                thread?.Join();
            }
            threads.Clear();
            Console.WriteLine(JsonConvert.SerializeObject(PingScan.failIPs));
            Parallel.ForEach(PingScan.failIPs, ip =>
            {
                Thread thread = new Thread(Ports);
                threads.Add(thread);
                thread.Start(ip);
            });
            foreach (Thread thread in threads)
            {
                thread?.Join();
            }
            PingScan.failIPs = PingScan.failIPs.Except(Form1.CURRENT.Select(x => x.IP).ToList()).ToList();
        }
    }
}
