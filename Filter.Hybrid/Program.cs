using System;
using System.Collections.Generic;


namespace Filter.Hybrid
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            FH1 fH1 = new FH1();
            fH1.Run();
        }
    }
}
