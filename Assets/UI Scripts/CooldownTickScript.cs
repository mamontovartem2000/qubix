using System.Collections;
using System.Collections.Generic;
using ME.ECS;
using Project.Common.Components;
using Project.Core.Features.Player;
using Project.Modules.Network;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CooldownTickScript : MonoBehaviour
{
    public GlobalEvent CooldownChangedEvent;
    public TextMeshProUGUI[] SkillCooldownTimer;
    private void Start()
    {
        CooldownChangedEvent.Subscribe(CooldownTimeChanged);
    }

    private void CooldownTimeChanged(in Entity entity)
    {
        if (entity.Read<Owner>().Value != Worlds.current.GetFeature<PlayerFeature>().GetPlayerByID(NetworkData.SlotInRoom)) return;
        var cooldownLeft = entity.Read<Cooldown>().Value;
        SkillCooldownTimer[entity.Read<SkillTag>().id].SetText(((int)cooldownLeft).ToString());
        if(Mathf.RoundToInt(cooldownLeft) < 1) SkillCooldownTimer[entity.Read<SkillTag>().id].SetText("");
    }

    private void OnDestroy()
    {
        CooldownChangedEvent.Unsubscribe(CooldownTimeChanged);
    }
}
