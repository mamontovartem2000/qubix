using ME.ECS;

namespace Project.Common.Components
{
	public struct MoveSpeedAffect : IComponent {} //stats buff (stackable) --- DONE
	public struct LifeStealAffect : IComponent {} //component buff (non stackable) --- DONE
	public struct HealingAffect : IComponent {} //self (instant) --- DONE
	public struct StunAffect : IComponent {} //component buff (non stackable) --- DONE
	public struct FireRateAffect : IComponent {} //stats buff (stackable) --- DONE
	public struct ForceShieldAffect : IComponent {} //component buff (non stackable) --- DONE
	public struct InstantReloadAffect : IComponent {} //self (instant) --- DONE
	public struct SkillsResetAffect : IComponent {} //self (instant) --- DONE
	public struct MagneticStormAffect : IComponent {} //target
	public struct WeaponDamageAffect : IComponent {} //stats buff (stackable) --- DONE
	public struct SkillSilenceAffect : IComponent {} //component buff (non stackable) --- DONE
	public struct GrenadeThrowAffect : IComponent {} //target
	public struct LandMineAffect : IComponent {} //self (instant) --- DONE
	public struct FiringRangeAffect : IComponent {} //stats buff (stackable) --- DONE
	public struct SideStepAffect : IComponent {} //self (instant) --- DONE
	public struct QuickStepAffect : IComponent {} //self (instant) --- 
	public struct LinearPowerAffect : IComponent {} //component buff (non stackable) --- DONE
	public struct WormholeHookAffect : IComponent {} //target
	public struct PersonalTeleportAffect : IComponent {} //self (instant)
}