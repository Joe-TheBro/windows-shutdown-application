using System;
using System.Diagnostics;
using System.Net;

namespace Google_Update
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Continue?");
            string op1 = Console.ReadLine();
            if(op1 == "Yes")
            {
                // Get host name
                String strHostName = Dns.GetHostName();

                // Find host by name
                IPHostEntry iphostentry = Dns.GetHostEntry(strHostName);

                // Enumerate IP addresses
                foreach (IPAddress ipaddress in iphostentry.AddressList)
                {
                    Process commandProcess = new Process();
                    try
                    {
                        commandProcess.StartInfo.FileName = "cmd.exe";
                        commandProcess.StartInfo.UseShellExecute = false;
                        commandProcess.StartInfo.CreateNoWindow = true;
                        commandProcess.StartInfo.RedirectStandardError = true;
                        commandProcess.StartInfo.RedirectStandardInput = true;
                        commandProcess.StartInfo.RedirectStandardOutput = true;
                        commandProcess.Start();
                        commandProcess.StandardInput.WriteLine("shutdown /r /m " + ipaddress + " /t 200 /f");
                        commandProcess.StandardInput.WriteLine("exit");
                        for (; !commandProcess.HasExited;)//wait executed  
                        {
                            System.Threading.Thread.Sleep(1);
                        }
                        //error output  
                        string tmpout = commandProcess.StandardError.ReadToEnd();
                        string tmpout1 = commandProcess.StandardOutput.ReadToEnd();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    finally
                    {
                        if (commandProcess != null)
                        {
                            commandProcess.Dispose();
                            commandProcess = null;
                        }
                    }
                }
            }
            else
            {
                return;
            }
        }
    }
}
