using ME.ECS;
using Project.Common.Utilities;

namespace Project.Common.Views
{
    using ME.ECS.Views.Providers;
    using Project.Common.Components;
    using Project.Modules.Network;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class NicknameView : MonoBehaviourView
    {
        [SerializeField] private TMP_Text _nick;
        
        public Image Fill;
        public Image Overlay;
        public Image Back;
        
        [SerializeField] private Color _red;
        [SerializeField] private Color _blue;

        public override bool applyStateJob => true;

        public override void OnInitialize() 
        {
            var player = entity.GetParent().Owner().Read<PlayerTag>();

            _nick.text = player.Nickname;

            if (player.Team == TeamTypes.blue)
            {
                _nick.color = _blue;
                Fill.color = _blue;
                Back.color = _blue * 0.4f;

            }
            else if (player.Team == TeamTypes.red)
            {
                _nick.color = _red;
                Fill.color = _red;
                Back.color = _red * 0.4f;
            }
        }

        public override void OnDeInitialize() { }

        public override void ApplyStateJob(UnityEngine.Jobs.TransformAccess transform, float deltaTime, bool immediately) { }

        public override void ApplyState(float deltaTime, bool immediately)
        {
            transform.position = entity.GetPosition() + new Vector3(0f, 1.1f, 0f);
            
            var fill = entity.GetParent().Read<PlayerHealthDefault>().Value;
            
            Fill.fillAmount = entity.GetParent().Read<PlayerHealth>().Value / fill;
            Overlay.fillAmount = entity.Read<PlayerHealthOverlay>().Value / fill;
        }
    }
}