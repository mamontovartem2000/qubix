using ME.ECS;

namespace Project.Common.Components
{
	public struct MoveSpeedAffect : IComponent {}
	public struct HealingAffect : IComponent {} 
	public struct StunAffect : IComponent {} 
	public struct FireRateAffect : IComponent {} 
	public struct ForceShieldAffect : IComponent {} 
	public struct InstantReloadAffect : IComponent {} 
	public struct SkillsResetAffect : IComponent {} 
	public struct GrenadeThrowAffect : IComponent {}
	public struct LandMineAffect : IComponent {} 
	public struct DashAffect : IComponent {} 
	public struct LinearPowerAffect : IComponent {} 
	public struct PersonalTeleportAffect : IComponent {} 

	public struct VFXTag : IComponent {};
}