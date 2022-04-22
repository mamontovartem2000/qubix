using ME.ECS;
using Project.Common.Components;
using Project.Core.Features.Player;
using Project.Modules.Network;
using TMPro;
using UnityEngine;

namespace UI_Scripts
{
	public class NewKillCounter : MonoBehaviour
	{
		public GlobalEvent KillEvent;
		public TextMeshProUGUI CounterText;

		private void Start()
		{
			KillEvent.Subscribe(UpdateCounter);
		}

		private void UpdateCounter(in Entity entity)
		{
			if(entity != Worlds.current.GetFeature<PlayerFeature>().GetPlayerByID(NetworkData.PlayerIdInRoom)) return;

			CounterText.SetText(entity.Read<PlayerScore>().Kills.ToString());
		}

		private void OnDestroy()
		{
			KillEvent.Unsubscribe(UpdateCounter);
		}
	}
}
