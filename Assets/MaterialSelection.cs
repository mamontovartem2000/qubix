using UnityEngine;

public class MaterialSelection : MonoBehaviour
{
    [SerializeField] private Material[] _materials;
    [SerializeField] private Renderer[] _rends;
    
    public void SelectMaterial(int index)
    {
        Debug.Log("yeet");
        
        foreach (var rend in _rends)
        {
            rend.sharedMaterial = _materials[index];
        }
    }
}
