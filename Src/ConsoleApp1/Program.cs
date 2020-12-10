using Armku.Communication.GPSDecoder;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            GPSDecode gps = new GPSDecode();

            gps.spGps.BaudRate = 9600;
            gps.spGps.PortName = "COM48";
            gps.Open();

            Console.ReadKey();
        }
    }
}
