using ME.ECS;
using Project.Common.Components;
using Project.Core;
using Project.Core.Features;
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
			DeathEvent.Subscribe(IncreaseCounter);
		}

		private void IncreaseCounter(in Entity entity)
		{
			if(!SceneUtils.CheckLocalPlayer(entity)) return;
			CounterText.SetText(entity.Read<PlayerScore>().Deaths.ToString());
		}

		private void OnDestroy()
		{
			DeathEvent.Unsubscribe(IncreaseCounter);
		}
	}
}
