using ME.ECS;
using Project.Common.Components;

namespace Project.Mechanics.Features.Avatar.Systems
{

#pragma warning disable
    using Project.Modules;
    using Project.Markers;
    using Modules;
    using Systems;
    using Markers;
#pragma warning restore

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class PlaySoundSystem : ISystemFilter
    {

        private AvatarFeature _feature;

        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {

            this.GetFeature(out this._feature);

        }

        void ISystemBase.OnDeconstruct() { }

#if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
#endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter()
        {
            return Filter.Create("Filter-PlaySoundSystem")
                .With<SoundPlay>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            entity.Remove<SoundPlay>();
            if (!entity.Has<SoundEffect>()) return;
            
            var randomSound = world.GetRandomRange(0, 4);
            entity.Get<LifeTimeLeft>().Value = 3;
            switch (randomSound)
            {
                case 0:
                    {
                        var view = world.RegisterViewSource(entity.Read<SoundEffect>().Sound0);
                        entity.InstantiateView(view);
                        break;
                    }
            
                case 1:
                    {
                        var view = world.RegisterViewSource(entity.Read<SoundEffect>().Sound1);
                        entity.InstantiateView(view);
                        break;
                    }
            
                case 2:
                    {
                        var view = world.RegisterViewSource(entity.Read<SoundEffect>().Sound2);
                        entity.InstantiateView(view);
                        break;
                    }
            
                case 3:
                    {
                        var view = world.RegisterViewSource(entity.Read<SoundEffect>().Sound3);
                        entity.InstantiateView(view);
                        break;
                    }
            
                default:
                    {
                        var view = world.RegisterViewSource(entity.Read<SoundEffect>().Sound0);
                        entity.InstantiateView(view);
                        break;
                    }
            }
        }
    }
}