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
           /* lock (s_lockObject)
            {
                pingThreads.Add(Thread.CurrentThread);
            } */
            string ip = args as string;
            Ping ping = new Ping();
            PingReply pingReply = ping.Send(ip, Form1.PingTimeout);
            if (pingReply!=null && pingReply.Status == IPStatus.Success)
            {
                try
                {
                    lock (s_lockObject)
                    {
                        Form1.CURRENT.Add(new Form1.JsonClass(ip));
                    }
                }
                catch
                {

                }
            }
            Form1.DisplayTips(ip);
            /*lock (s_lockObject)
            {
                pingThreads.Remove(Thread.CurrentThread);
            }*/
        }
        
        public PingScan()
        {
            pingThreads = new List<Thread>();
            Parallel.ForEach(failIPs, ip =>
            {
                Thread thread = new Thread(Ping);
                pingThreads.Add(thread);
                thread.Start(ip.ToString());
            });
            foreach (Thread t in pingThreads)
            {
                t?.Join();
            }
            failIPs = failIPs.Except(Form1.CURRENT.Select(x => x.IP).ToList()).ToList();
            Console.WriteLine(JsonConvert.SerializeObject(Form1.CURRENT));
            /*while (true)
            {
                Console.WriteLine(pingThreads.Count);
            }*/
            
        }
    }
}
