using System;
using UnityEngine;


namespace Code.Unit
{
    [Serializable] public class AttachPoint
    {
        public string Name;
        public string Path;
        public Vector3 LocalPosition;
        public Vector3 LocalRotation;
        public Vector3 LocalScale = new Vector3(100.0f, 100.0f, 100.0f);
    }
}