using ME.ECS;
using Photon.Pun;
using Project.Common.Components;
using Project.Core;
using Project.Core.Features;
using Project.Core.Features.Player;
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
			if(entity != Worlds.current.GetFeature<PlayerFeature>().GetPlayer(PhotonNetwork.LocalPlayer.ActorNumber)) return;

			CounterText.SetText(entity.Read<PlayerScore>().Deaths.ToString());
		}

		private void OnDestroy()
		{
			DeathEvent.Unsubscribe(UpdateCounter);
		}
	}
}
