using UnityEngine;

namespace GMTK
{
    public static class ItemHighlighter
    {
        public static void Highlight(this Transform item, float factor = 1.1f)
        {
            Vector3 scale = item.localScale;
            scale *= factor;
            item.localScale = scale;
        }
        
        public static void Unhighlight(this Transform item, float factor = 1.1f)
        {
            Vector3 scale = item.localScale;
            scale /= factor;
            item.localScale = scale;
        }
    }
}