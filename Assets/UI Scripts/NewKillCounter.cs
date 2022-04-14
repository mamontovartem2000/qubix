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
			if(entity != Worlds.current.GetFeature<PlayerFeature>().GetPlayer(PhotonNetwork.LocalPlayer.ActorNumber)) return;

			CounterText.SetText(entity.Read<PlayerScore>().Kills.ToString());
		}

		private void OnDestroy()
		{
			KillEvent.Unsubscribe(UpdateCounter);
		}
	}
}
