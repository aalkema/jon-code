using System;
using System.IO;
using System.Collections.Generic;

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

            try {
                foreach (string line in File.ReadLines(args[0])) {
                    string[] props = line.Split(" ");
                    if (props.Length != 2) {
                        Console.Error.WriteLine($"{line} needs a name with a space then an initiative");
                        continue;
                    }
                    players.Add(new Player() {
                        Name = props[0],
                        Initiative = int.Parse(props[1]),
                        Counted = false
                    });
                }
            } catch (Exception e) {
                Console.Error.WriteLine(e.Message);
                Environment.Exit(0);
            }

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
