using UnityEngine;

namespace MING
{
    public class BashUtils
    {
        public static Vector3 V2ToV3(Vector2 v)
        {
            return new Vector3(v.x, 0, v.y);
        }
    }
}
