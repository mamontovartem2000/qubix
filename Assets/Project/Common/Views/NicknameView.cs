using ME.ECS;

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
        [SerializeField] private Color _red;
        [SerializeField] private Color _blue;

        public override bool applyStateJob => true;

        public override void OnInitialize() 
        {
            var player = entity.GetParent().Read<Owner>().Value.Read<PlayerTag>();

            _nick.text = player.Nickname;

            if (player.Team == TeamTypes.blue)
            {
                _nick.color = _blue;
            }
            else if (player.Team == TeamTypes.red)
            {
                _nick.color = _red;
            }
        }

        public override void OnDeInitialize() { }

        public override void ApplyStateJob(UnityEngine.Jobs.TransformAccess transform, float deltaTime, bool immediately) { }

        public override void ApplyState(float deltaTime, bool immediately)
        {
            transform.position = entity.GetPosition() + new Vector3(0f, 1.1f, 0f);
        }
    }
}