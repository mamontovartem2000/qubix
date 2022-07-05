using ME.ECS;
using Project.Common.Components;

namespace Project.Common.Utilities {

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

        public static bool TryReadCollided(this in Entity entity, out Entity from, out Entity to) {

            var col = entity.Read<Collided>();
            to = col.ApplyTo;
            from = col.ApplyFrom;
            if (to.IsAlive() == false || from.IsAlive() == false) return false;

            return true;

        }

    }

}