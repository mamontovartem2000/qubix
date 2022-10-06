namespace Project.Common.Utilities
{
    public static class GameConsts
    {
        public static class Main
        {
            public const float RESPAWN_TIME = 3f;
            public const float AVOID_TELEPORT_SECONDS = 3f;
            public const float DEATHMATCH_TIMER = 150f;
            public const float TEAM_DEATHMATCH_TIMER = 150f;
            public const float DEFAULT_LIFETIME = 4f;
        }

        public static class Movement
        {
            public const float LOCK_SPEED_RATIO = 0.65f;
            public const float MIN_DISTANCE = 0.025f;
            public const float ROTATION_SPEED = 30f;
            public const float SLOWNESS_RATIO = 0.8f;
            public const float DEFAULT_MOVEMENT_SPEED_MODIFIER = 1;
        }
        
        public static class MapBuffs
        {
            public const float POWER_UP_LIFETIME = 60f;
            public const float POWER_UP_SPAWN_DELAY = 3f;
        }

        public static class Weapons
        {
            public static class Shengbiao
            {
                public const float ATTACK_SECONDS = 0.2f;
                public const float MIN_LENGHT = 1f;
                public const float RETURN_SPEED = -3.5f;
                public const float SPEED = 20f;
                public const float VISUAL_SPEED_RATIO = 1.4f;
            }

            public static class Modifiers
            {
                public const float CRIT_CHANCE = 0.3f;
            }
        
        }

        public static class Scene
        {
            public const float HEAL_DISPENSER_VALUE = 15f;
            public const float DESTRUCTUBLE_OBJ_HEALTH = 30f;
            public const float SPIKES_DAMAGE = 30F;
            public const float SPIKES_LIFETIME = 1F;
            public static class Mines
            {
                public const int COUNT = 10;
                public const int DAMAGE_MIN = 30;
                public const int DAMAGE_MAX = 40;
                public const float SPAWN_DELAY_DEFAULT = 3f;
                public const float BLINK_TIME = 0.15f;
                public const float BLINK_FREQUENCY_MIN = 3f;
                public const float BLINK_FREQUENCY_MAX = 6f;
            }
        }

        public static class Skills
        {
            public const int FIRE_RATE_SKILL_AMMO_CAPACITY = 2;
            public const float INSTANT_RELOAD_TIME = 0.1f;
            public const int DASH_LENGTH = 3;
            public const float EMP_DURATION_DEFAULT = 2;
        }
        
        public static class GameModes
        {
            public static class FlagCapture
            {
                public const int TEAM_FLAG_COUNT = 2;
                public const int WIN_FLAG_COUNT = 3;
                public const float DROPPED_FLAG_LIFETIME = 15f;
                public const float FLAG_RESPAWN_TIME = 3f;
                public const float FIRST_GAME_PHASE_TIME = 5 * 60f;
                public const float SECOND_GAME_PHASE_TIME = 3 * 60f;
            }
        }
    }
}