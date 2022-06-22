using UnityEngine;

namespace Project.Visuals.AssetStore.SpecialSkillsEffectsPack.Scripts.ForEffects
{
    public class LookAtTarget : MonoBehaviour
    {
        public Transform Target;

        // Update is called once per frame
        void Update()
        {
            transform.LookAt(Target);
        }
    }
}
