using ME.ECS;
using Project.Common.Components;
using Project.Core;
using Project.Core.Features;
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
			KillEvent.Subscribe(IncreaseCounter);
		}

		private void IncreaseCounter(in Entity entity)
		{
			if(!SceneUtils.CheckLocalPlayer(entity)) return;
			CounterText.SetText(entity.Read<PlayerScore>().Kills.ToString());
		}

		private void OnDestroy()
		{
			KillEvent.Unsubscribe(IncreaseCounter);
		}
	}
}
