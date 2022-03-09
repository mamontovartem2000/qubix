using ME.ECS;

namespace Project.Features 
{
	#region usage
    using Components; using Modules; using Systems; using Features; using Markers;
    using Weapon.Components; using Weapon.Modules; using Weapon.Systems; using Weapon.Markers;
    
    namespace Weapon.Components {}
    namespace Weapon.Modules {}
    namespace Weapon.Systems {}
    namespace Weapon.Markers {}
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
	#endregion
    public sealed class WeaponFeature : Feature 
    {
	    protected override void OnConstruct()
	    {
		    
	    }

	    protected override void OnDeconstruct()
	    {
		    
	    }
    }
}