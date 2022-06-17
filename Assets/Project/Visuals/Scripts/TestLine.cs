using UnityEngine;

namespace Project.Visuals.Scripts
{
    public class TestLine : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lr;

        [SerializeField] private Transform[] _points;

        private void Start()
        {
            _lr.positionCount = _points.Length;
        }

        private void Update()
        {
            for (int i = 0; i < _points.Length; i++)
            {
                _lr.SetPosition(i, _points[i].position);
            }
        }
    }
}
