using ME.ECS;
using Project.Common.Components;
using Project.Features.Player;
using Project.Modules.Network;
using TMPro;
using UnityEngine;

namespace UI_Scripts
{
	public class NewDeathCounter : MonoBehaviour
	{
		public GlobalEvent DeathEvent;
		public TextMeshProUGUI CounterText;

		private void Start()
		{
			DeathEvent.Subscribe(UpdateCounter);
		}

		private void UpdateCounter(in Entity entity)
		{
			if(entity != Worlds.current.GetFeature<PlayerFeature>().GetPlayerByID(NetworkData.SlotInRoom)) return;

			CounterText.SetText(entity.Read<PlayerScore>().Deaths + " : DEATHS");
		}

		private void OnDestroy()
		{
			DeathEvent.Unsubscribe(UpdateCounter);
		}
	}
}
