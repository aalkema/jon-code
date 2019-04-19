using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace jon_code
{
    class Program
    {
        static void Main(string[] args)
        {
            var players = new List<Player>();

            if (args.Length != 1) {
                Console.Error.WriteLine("Please pass in the path to the players file");
                Environment.Exit(0);
            }

            players = JsonConvert.DeserializeObject<List<Player>>(File.ReadAllText(args[0]));

            int highestInitiative = 0;
            Player winningPlayer = null;
            while (AnyPlayersLeft(players)) {
                foreach (Player player in players) {
                    if (!player.Counted) {
                        if (player.Initiative > highestInitiative) {
                            winningPlayer = player;
                            highestInitiative = player.Initiative;
                        }
                    }
                }
                Console.WriteLine($"{winningPlayer.Name} with initiative {winningPlayer.Initiative}");
                winningPlayer.Counted = true;
                highestInitiative = 0;
            }
        }

        static bool AnyPlayersLeft(List<Player> players) {
            for (int i = 0; i < players.Count; i++) {
                if (players[i].Counted == false){
                    return true;
                }
            }
            return false;
        }
    }
}
