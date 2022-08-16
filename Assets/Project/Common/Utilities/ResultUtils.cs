using System.Collections.Generic;
using ME.ECS;
using Project.Common.Components;

namespace Project.Common.Utilities
{
    public static class ResultUtils
    {
        public static List<Entity> GetPlayersWithMostKills(Filter players)
        {
            var maxKills = int.MinValue;
            var winners = new List<Entity>();

            foreach (var player in players)
            {
                var kills = player.Read<PlayerScore>().Kills;

                if (kills > maxKills)
                {
                    winners.Clear();
                    winners.Add(player);
                    maxKills = kills;
                }
                else if (kills == maxKills)
                {
                    winners.Add(player);
                }
            }

            return winners;
        }
        
        public static List<Entity> GetPlayersWithMostDamage(List<Entity> playersList)
        {
            var maxDamage = int.MinValue;
            var winners = new List<Entity>();

            foreach (var player in playersList)
            {
                var dealtDamage = player.Read<PlayerScore>().DealtDamage;

                if (dealtDamage > maxDamage)
                {
                    winners.Clear();
                    winners.Add(player);
                    maxDamage = (int)dealtDamage;
                }
                else if (dealtDamage.Equals(maxDamage))
                {
                    winners.Add(player);
                }
            }

            return winners;
        }
        
        public static List<Entity> GetPlayersWithMostHealth(List<Entity> playersList)
        {
            var maxHealth = float.MinValue;
            var winners = new List<Entity>();

            foreach (var player in playersList)
            {
                if (!player.Read<PlayerAvatar>().Value.IsAlive()) continue;
                var health = player.Read<PlayerAvatar>().Value.Read<PlayerHealth>().Value / player.Read<PlayerAvatar>().Value.Read<PlayerHealthDefault>().Value;

                if (health > maxHealth)
                {
                    winners.Clear();
                    winners.Add(player);
                    maxHealth = health;
                }
                else if (health.Equals(maxHealth))
                {
                    winners.Add(player);
                }
            }
            return winners;
        }
        
        public static Team GetTeamFullStats(Filter players, int teamNumber)
        {
            var team = new Team();

            foreach (var player in players)
            {
                if (player.Read<TeamTag>().Value != teamNumber) continue;

                team.Kills += player.Read<PlayerScore>().Kills;
                team.Deaths += player.Read<PlayerScore>().Deaths;
                team.Damage += player.Read<PlayerScore>().DealtDamage;
                
                if (player.Read<PlayerAvatar>().Value.IsAlive())
                    team.Health += player.Read<PlayerAvatar>().Value.Read<PlayerHealth>().Value;
            }

            return team;
        }
        
        public class Team
        {
            public int Kills = 0;
            public int Deaths = 0;
            public float Damage = 0;
            public float Health = 0;
        }
    }
}
