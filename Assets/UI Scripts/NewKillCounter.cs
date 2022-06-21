using ME.ECS;
using Project.Common.Components;
using Project.Features.Player;
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
			if(entity != Worlds.current.GetFeature<PlayerFeature>().GetPlayerByID(NetworkData.SlotInRoom)) return;

			CounterText.SetText("KILLS : " + entity.Read<PlayerScore>().Kills);
		}

		private void OnDestroy()
		{
			KillEvent.Unsubscribe(UpdateCounter);
		}
	}
}
