using MvcWebLibrary;

namespace Application
{
    class Program
    {
        static void Main(string[] args)
        {
            WebHost.CreateDefault(args)
                .UseStartup<Startup>()
                .Run();

            //.∧＿∧ 
            //( ･ω･｡)つ━☆・*。 
            //⊂　 ノ ・゜+.
            //しーＪ°。+*костыльнуть!
            System.Console.ReadKey();
        }
    }
}