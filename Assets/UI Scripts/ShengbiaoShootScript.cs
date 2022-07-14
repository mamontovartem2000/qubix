using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using ME.ECS;
using Project.Common.Components;
using Project.Features.Player;
using Project.Modules.Network;
using UnityEngine;
using UnityEngine.UI;

public class Shen : MonoBehaviour
{
    public GlobalEvent HealthChangedEvent;
    [SerializeField] private Material _spearChainMateial;

    private void Start()
    {
        HealthChangedEvent.Subscribe(HealthChanged);
    }

    private void HealthChanged(in Entity player)
    {
        Sequence sequence = DOTween.Sequence();
        sequence
            .Append(_spearChainMateial.DOOffset(new Vector2(0, 0.7f), 0.1f))
            .AppendInterval(0)
            .Append(_spearChainMateial.DOOffset(new Vector2(0, 1), 0.8f));
    }

    private void OnDestroy()
    {
        HealthChangedEvent.Unsubscribe(HealthChanged);
    }
}
