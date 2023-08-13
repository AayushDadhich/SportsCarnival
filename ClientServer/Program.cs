using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ConsoleHandler consoleHandler = new ConsoleHandler();
                consoleHandler.StartApplication();
            }
            catch (ConnectionNotEstablishedException connectionNotEstablishedException)
            {
                System.Console.WriteLine(connectionNotEstablishedException.Message);
                System.Console.ReadLine();
            }
            catch (Exception generalException)
            {
                System.Console.WriteLine(generalException.Message);
                System.Console.ReadLine();
            }
        }
    }
}
