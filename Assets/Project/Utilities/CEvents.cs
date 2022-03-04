using ME.ECS;
using Project.Features;

namespace Project.Utilities
{

    public static class Utilitiddies
    {
        public static bool CheckLocalPlayer(in Entity entity)
        {
            var result = entity == Worlds.current.GetFeature<PlayerFeature>().GetActivePlayer();
            return result;
        }
        
        public static int CheckIndexByLength(int index, int length)
        {
            return index < length ? index : index - length;
        }
    }
}