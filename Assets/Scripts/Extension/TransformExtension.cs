using UnityEngine;


namespace Extension
{
    public static class TransformExtension
    {
        public static Transform Change(this Transform original, object position = null, object rotation = null)
        {
            original.position = position == null ? original.position : (Vector3) position;
            original.rotation = rotation == null ? original.rotation : (Quaternion) rotation;
            return original;
        }
        public static Transform Change(this Transform original,Transform newValue)
        {
            original.position = newValue.position;
            original.rotation = newValue.rotation;
            return original;
        }
    }
}