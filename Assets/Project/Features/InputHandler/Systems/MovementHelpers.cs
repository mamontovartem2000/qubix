using UnityEngine;

public static class MovementHelpers
{
    public static bool CheckMovePosition(Vector3 target)
    {
        var layer = 1 << 7;
        return Physics.Raycast(target + new Vector3(0f,1f,0f), Vector3.down, 3f, layer);
    }
    
    public static Vector3 CheckHeight(Vector3 target)
    {
        var layer = 1 << 7;
        
        if (Physics.Raycast(target + new Vector3(0f,1f,0f), Vector3.down, out var hit, 3f, layer))
        {
            Debug.Log(hit.distance);
            return new Vector3(0f,1.4f - hit.distance ,0f);
        }

        return Vector3.zero;
    }
}