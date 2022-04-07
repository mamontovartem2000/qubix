using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    [SerializeField] private ParticleSystem _ps;

    public void Play()
    {
        _ps.Play();
    }

    public void Stop()
    {
        _ps.Stop();
    }
}
