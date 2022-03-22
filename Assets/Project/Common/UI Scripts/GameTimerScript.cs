using ME.ECS;
using UnityEngine;

// using TMPro;

namespace Project.Common.UI_Scripts
{
	public class GameTimerScript : MonoBehaviour
	{

		public GlobalEvent RunTimerEvent;
		// public TextMeshProUGUI TimerText;

		private void Start()
		{
			InvokeRepeating(nameof(ChangeTimer), 1f, 1f);
		}

		private void ChangeTimer()
		{
			// TimerText.SetText(Worlds.current.GetFeature<SceneBuilderFeature>().TimerEntity.Read<GameTimer>().Value.ToString("###"));
		}
	}
}
