using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Heijden.DNS;
using System.Threading;

namespace NetworkScanner
{
    class DNSScan
    {
        private List<Thread> threads;
        public static void DNS(object args)
        {
            string ip = args as string;
            try
            {
                Resolver resolver = new Resolver(Form1.dnsServerIP);
                resolver.TimeOut = 1;
                IPHostEntry iPHost = resolver.GetHostEntry(ip);  
                if (iPHost.HostName != null)
                {
                    if (!Form1.CURRENT.ContainsKey(ip)) Form1.CURRENT.Add(ip, new Dictionary<string, SortedSet<dynamic>>());
                    if (!Form1.CURRENT[ip].ContainsKey("Hostname")) Form1.CURRENT[ip].Add("Hostname", new SortedSet<dynamic>());
                    Form1.CURRENT[ip]["Hostname"].Add(iPHost.HostName);
                    Form1.DisplayTips(ip);
                } 
            }
            catch
            {

            }
        }
        public DNSScan()
        {
            Form1.Status = "Dns";
            threads = new List<Thread>();
            foreach(string ip in Form1.CURRENT.Keys)
            {
                Thread thread = new Thread(DNS);
                threads.Add(thread);
                thread.Start(ip);
            }
            foreach (string ip in PingScan.failIPs)
            {
                Thread thread = new Thread(DNS);
                threads.Add(thread);
                thread.Start(ip);
            }
        }
    }
}
