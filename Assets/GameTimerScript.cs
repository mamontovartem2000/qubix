using System;
using ME.ECS;
using Project.Features;
using Project.Features.Components;
using Project.Features.SceneBuilder.Components;
using TMPro;
using UnityEngine;

public class GameTimerScript : MonoBehaviour
{

	public GlobalEvent RunTimerEvent;
	public TextMeshProUGUI TimerText;

	private void Start()
	{
		InvokeRepeating(nameof(ChangeTimer), 1f, 1f);
	}

	private void ChangeTimer()
	{
		TimerText.SetText(Worlds.current.GetFeature<SceneBuilderFeature>().TimerEntity.Read<GameTimer>().Value.ToString("###"));
	}
}
