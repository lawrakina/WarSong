using UnityEngine;


namespace Unit
{
    public interface IBaseUnitView
    {
        Transform Transform { get; }
        Collider Collider { get; }
        Rigidbody Rigidbody { get; }
        MeshRenderer MeshRenderer { get; }
        Animator Animator { get; }
        AnimatorParameters AnimatorParameters { get; }
        ICharAttributes CharAttributes { get; set; }
    }
}