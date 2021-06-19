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
        List<Thread> threads;
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
                    if (Form1.CURRENT.FirstOrDefault(x => x.IP == ip) == null) Form1.CURRENT.Add(new Form1.JsonClass(ip));
                    Form1.CURRENT.First(x => x.IP == ip).Hostname = iPHost.HostName;
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
            Parallel.ForEach(Form1.CURRENT.Select(x => x.IP).ToList(), ip =>
            {
                Thread thread = new Thread(DNS);
                threads.Add(thread);
                thread.Start(ip);
            });
            Parallel.ForEach(PingScan.failIPs, ip =>
            {
                Thread thread = new Thread(DNS);
                threads.Add(thread);
                thread.Start(ip);
            });
        }
    }
}
