using System;
using System.Threading;

namespace SpaceOnLine
{
    class Program
    {
        private static Game game = new Game();

        public static void Main(string[] args)
        {
            game.Run();
            
            //AsyncClient.StartClient();

            //while(true)
            //{
            //    Thread.Sleep(100);
            //    AsyncClient.SendPlayerData();
                
            //}

        }

    }
}
