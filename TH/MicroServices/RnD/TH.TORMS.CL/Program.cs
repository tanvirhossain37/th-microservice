using System.Diagnostics;
using PuppeteerSharp;

namespace TH.TORMS.CL
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //await ConnectToVpn();
            await ConnectToTor();
            Console.WriteLine("Hello, World!");
        }

        private static async Task ConnectToVpn()
        {
            var _desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string src = $"C:\\Program Files\\Proton\\VPN\\ProtonVPN.Launcher.exe";

            //var process = Process.Start(src);
            var process = Process.Start(src, "VPNConnectionName tanvir.hossain37@gmail.com T@nv!r.2022");
            
            Thread.Sleep(10000);
            process?.Kill(true);
            process?.Dispose();
        }

        private static async Task ConnectToTor()
        {
            try
            {
                string target = "https://www.youtube.com/watch?v=yjeWKoZv-QU";
                var _desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string src = $"{_desktopPath}\\Tor Browser\\Browser\\firefox.exe";

                try
                {
                    int count = 1;
                    while (true)
                    {
                        var randomTime = Util.GetRandomTime(60000, 300000);//1min-5mins 60000, 300000

                        Console.WriteLine($"Waiting: {(randomTime/60000).ToString()} mins till {DateTime.Now.AddMilliseconds(randomTime)}");
                        Thread.Sleep(randomTime);

                        Console.WriteLine("Connecting to Tor");
                        var process = Process.Start(src, target);
                        Thread.Sleep(180000);//3min

                        process?.Kill(true);
                        Console.WriteLine("Killed the Tor");
                        
                        var dest = $"{_desktopPath}\\TorResult";


                        Util.CreateDirectories(dest);
                        Util.Write($"{dest}\\log.txt",$"Hit {count.ToString()} times");
                        count++;
                    }
                }
                catch (System.ComponentModel.Win32Exception noBrowser)
                {
                    if (noBrowser.ErrorCode == -2147467259)
                        Console.WriteLine(noBrowser.Message);
                }
                catch (System.Exception other)
                {
                    Console.WriteLine(other.Message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
