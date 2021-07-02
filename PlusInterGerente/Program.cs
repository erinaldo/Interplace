using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PlusInterGerente
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string[] aExe = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.exe");

                foreach (string sExe in aExe)
                {
                    if (sExe.Contains("PlusInterGerente"))
                        continue;

                    //if (!sExe.Contains("exeteste"))
                    //    continue;


                    string sArquivo = Path.GetFileName(sExe);
                    System.Diagnostics.Process.Start(sExe);
                    Console.WriteLine("Executando " + sArquivo + " - " + DateTime.Now.ToString());

                    bool lExecutando = true;
                    while (lExecutando)
                    {
                        Console.WriteLine("Ainda executando " + sArquivo + " - " + DateTime.Now.ToString());
                        Thread.Sleep(5000);

                        lExecutando = false;
                        Process[] processlist = Process.GetProcesses();
                        foreach (Process theprocess in processlist)
                        {
                            if (sArquivo.Contains(theprocess.ProcessName))
                            {
                                lExecutando = true;
                            }
                        }
                    }


                }

            }


        }
    }
}
