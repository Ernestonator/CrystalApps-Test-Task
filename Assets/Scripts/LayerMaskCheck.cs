using UnityEngine;

/// <summary>
/// Utillity script.
/// Used for comparing masks.
/// </summary>
public class LayerMaskCheck : MonoBehaviour
{
    /// <summary>
    /// Check if object is in chosen layer mask
    /// </summary>
    /// <param name="gameObject">game object we want to check</param>
    /// <param name="layerMask">layer masks we want to check</param>
    /// <returns></returns>
    public static bool IsInLayerMask(GameObject gameObject, params LayerMask[] layerMask)
    {
        for(int i = 0; i < layerMask.Length; i++)
        {
            if ((layerMask[i].value & (1 << gameObject.layer)) > 0)
            {
                return true;
            }
        }

        return false;
    }
}
