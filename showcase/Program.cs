using System;
using System.Threading.Tasks;

namespace showcase
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var albumService = new AlbumService();
            var input = "";
            Console.WriteLine("Please enter an album to lookup.  e.g. photo-album 1");

            while (true)
            {
                input = Console.ReadLine();

                if (input == "exit")
                    break;

                var output = await albumService.ProcessInput(input);
                Console.WriteLine(output);
            }
        }
    }
}