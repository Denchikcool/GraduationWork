using UnityEngine;

namespace Denchik.Utilities
{
    public static class LayerMaskUtilities
    {
        public static bool IsLayerInMask(int layer, LayerMask layerMask)
        {
            return ((1 << layer) & layerMask) > 0;
        }

        public static bool IsLayerInMask(RaycastHit2D hit, LayerMask layerMask)
        {
            return IsLayerInMask(hit.collider.gameObject.layer, layerMask);
        }
    }
}
