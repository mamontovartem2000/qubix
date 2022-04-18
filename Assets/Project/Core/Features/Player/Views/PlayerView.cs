using ME.ECS;
using ME.ECS.Views.Providers;
using Project.Common.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Core.Features.Player.Views
{
    public class PlayerView : MonoBehaviourView
    {
        public bool ShowHealth;
        [SerializeField] private Image Healthbar;
        public override bool applyStateJob => true;
        public override void OnInitialize() {}
        public override void OnDeInitialize() {}

        public override void ApplyState(float deltaTime, bool immediately)
        {
            transform.position = entity.GetPosition();
            transform.rotation = entity.GetRotation();

            if (ShowHealth)
            {
                var fill = entity.Read<PlayerHealth>().Value / 100;

                Healthbar.fillAmount = fill;
                Healthbar.color = Color.Lerp(Color.red, Color.green, fill);
            }
        }
    }
}