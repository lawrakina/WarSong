using UnityEngine;


namespace Extension
{
    public static class BuilderExtension
    {
        public static GameObject AddCapsuleCollider(this GameObject gameObject, float radius, bool isTrigger,
            Vector3 center, float height, int numberAxisDirection = 1)
        {
            var component = gameObject.GetOrAddComponent<CapsuleCollider>();
            component.radius = radius;
            component.isTrigger = isTrigger;
            component.center = center;
            component.height = height;
            component.direction = numberAxisDirection;
            return gameObject;
        }

        public static GameObject AddRigidBody(this GameObject gameObject, float mass,
            CollisionDetectionMode collisionDetectionMode, bool isKinematic, bool useGravity,
            RigidbodyConstraints constraints)
        {
            var component = gameObject.GetOrAddComponent<Rigidbody>();
            component.mass = mass;
            component.collisionDetectionMode = collisionDetectionMode;
            component.constraints = constraints;
            component.isKinematic = isKinematic;
            component.useGravity = useGravity;
            return gameObject;
        }

        public static GameObject AddCode<T>(this GameObject gameObject) where T : Component
        {
            var component = gameObject.GetOrAddComponent<T>();
            return gameObject;
        }

        private static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            var result = gameObject.GetComponent<T>();
            if (!result) result = gameObject.AddComponent<T>();

            return result;
        }
    }
}