using ME.ECS;

namespace Project.Common
{
    public static class Utilitiddies
    {
        public static int SafeCheckIndexByLength(int index, int length)
        {
            return index < length ? index : index - length;
        }
    }
}