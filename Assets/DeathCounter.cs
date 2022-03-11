using System;
using ME.ECS;
using Project.Features.Player.Components;
using Project.Utilities;
using TMPro;
using UnityEngine;

public class DeathCounter : MonoBehaviour
{
	public GlobalEvent DeathEvent;
	public TextMeshProUGUI CounterText;

	private void Start()
	{
		DeathEvent.Subscribe(IncreaseCounter);
	}

	private void IncreaseCounter(in Entity entity)
	{
		if(!Utilitiddies.CheckLocalPlayer(entity)) return;

		CounterText.SetText(entity.Read<PlayerScore>().Deaths.ToString());
	}

	private void OnDestroy()
	{
		DeathEvent.Unsubscribe(IncreaseCounter);
	}
}
