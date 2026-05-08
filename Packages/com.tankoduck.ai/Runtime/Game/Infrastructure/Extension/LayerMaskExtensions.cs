using UnityEngine;

namespace Game.Infrastructure.Extension
{
    public static class LayerMaskExtensions
    {
        public static bool IsOnLayer(this Component component, int layerMask)
            => layerMask == (layerMask | (1 << component.gameObject.layer));
    }
}