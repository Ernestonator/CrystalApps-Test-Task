using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used for comparing masks.
/// </summary>
public class LayerMaskCheck : MonoBehaviour
{
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
