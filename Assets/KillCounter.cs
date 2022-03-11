using ME.ECS;
using Project.Features.Player.Components;
using Project.Utilities;
using TMPro;
using UnityEngine;

public class KillCounter : MonoBehaviour
{
	public GlobalEvent KillEvent;
	public TextMeshProUGUI CounterText;

	private void Start()
	{
		KillEvent.Subscribe(IncreaseCounter);
	}

	private void IncreaseCounter(in Entity entity)
	{
		if(!Utilitiddies.CheckLocalPlayer(entity)) return;

		CounterText.SetText(entity.Read<PlayerScore>().Kills.ToString());
	}

	private void OnDestroy()
	{
		KillEvent.Unsubscribe(IncreaseCounter);
	}
}
