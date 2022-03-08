using DG.Tweening;
using UnityEngine;

public class DisplaTween : MonoBehaviour
{
    private void Awake()
    {
        transform.DORotate(new Vector3(0f, -360f, 0f), 5f, RotateMode.WorldAxisAdd).SetEase(Ease.Linear).SetLoops(-1);
    }
}
