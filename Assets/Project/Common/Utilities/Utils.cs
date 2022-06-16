using ME.ECS;
using Project.Common.Components;

namespace Project {

    public static class Utils {

        public static Entity Owner(this in Entity entity) {

            return entity.Read<Owner>().Value;

        }

        public static Entity Avatar(this in Entity entity) {

            return entity.Read<PlayerAvatar>().Value;

        }

        public static Entity Owner(this in Entity entity, out Entity owner) {

            owner = entity.Read<Owner>().Value;
            return owner;

        }

    }

}