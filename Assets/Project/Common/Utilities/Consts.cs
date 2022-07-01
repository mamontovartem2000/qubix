namespace Project.Common.Utilities
{
    public static class Consts
    {
        public static class Main
        {
            public const float RESPAWN_TIME = 3f;
            public const float AVOID_TELEPORT_SECONDS = 3f;
            public const float GAME_TIMER_SECONDS = 150f;
        }

        public static class Movement
        {
            public const float LOCK_SPEED_RATIO = 0.65f;
            public const float MIN_DISTANCE = 0.025f;
            public const float ROTATION_SPEED = 30f;
            public const float SLOWNESS_RATIO = 0.8f;
            public const float DEFAULT_MOVEMENT_SPEED_MODIFIER = 1;
        }
        
        public static class Weapons
        {
            public const float GRENADE_EXPLOSION_SQUARED_RADIUS = 10f;
        }
        
        public static class Scene
        {
            public const float MINE_BLINK_TIME = 0.15f;
            public const float HEAL_DISPENSER_VALUE = 15f;
        }

        public static class Skills
        {
            public const int FIRE_RATE_SKILL_AMMO_CAPACITY = 2;
            public const float INSTANT_RELOAD_TIME = 0.1f;
            public const int DASH_LENGTH = 3;
            public const float EMP_DURATION_DEFAULT = 2;
        }
    }
}