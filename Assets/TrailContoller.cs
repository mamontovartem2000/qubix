using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailContoller : MonoBehaviour
{
    [SerializeField] private ParticleSystem _ps;
    public void TrailOn()
    {
        _ps.Play();
    }
    public void TrailOff()
    {
        _ps.Stop();
    }
}
