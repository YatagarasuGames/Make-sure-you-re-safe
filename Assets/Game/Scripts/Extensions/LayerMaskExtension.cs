using UnityEngine;

public static class LayerMaskExtension
{
    public static bool Contains(this LayerMask mask, int layer)
    {
        return ((1 << layer) & mask) != 0;
    }
}
