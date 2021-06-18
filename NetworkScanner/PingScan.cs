using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetworkScanner
{
    class PingScan
    {
        private List<Thread> pingThreads;
        public static List<string> failIPs;
        private static object s_lockObject = "";
        private void Ping(object args)
        {
            string ip = args as string;
            Ping ping = new Ping();
            PingReply pingReply = ping.Send(ip, Form1.PingTimeout);
            if (pingReply!=null && pingReply.Status == IPStatus.Success)
            {
                try
                {
                    lock (s_lockObject)
                    {
                        Form1.CURRENT.Add(ip, new Dictionary<string, SortedSet<dynamic>>());
                    }
                }
                catch
                {

                }
            }
            Form1.DisplayTips(ip);
        }
        
        public PingScan()
        {
            Form1.Status = "Ping";
            pingThreads = new List<Thread>();
            foreach(string ip in failIPs)
            {
                Thread thread = new Thread(Ping);
                pingThreads.Add(thread);
                thread.Start(ip.ToString());
            }
            foreach (Thread t in pingThreads)
            {
                t?.Join();
            }
            foreach(string ip in Form1.CURRENT.Keys)
            {
                failIPs.Remove(ip);
            }
            Console.WriteLine(JsonConvert.SerializeObject(Form1.CURRENT));

        }
    }
}
