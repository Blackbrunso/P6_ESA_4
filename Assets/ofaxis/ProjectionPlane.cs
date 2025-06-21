using UnityEngine;

/**
 * <summary>Projection Plane is a 2D plane in 3D space that can be used to project a texture or image onto.</summary>
 * <remarks>Original Source: https://medium.com/try-creative-tech/off-axis-projection-in-unity-1572d826541e</remarks>
 */
[ExecuteInEditMode]
public class ProjectionPlane : MonoBehaviour
{
    [Header("Projection Plane")] [SerializeField]
    private Vector2 size = new(2, 1.125f);

    private Vector2 _previousSize;
    [SerializeField] private Vector2 aspectRatio = new(16, 9);
    private Vector2 _previousAspectRatio;
    [SerializeField] private bool lockAspectRatio = true;

#if UNITY_EDITOR
    [Header("Visualization")] [SerializeField]
    private bool drawGizmos = true;
#endif
    
    [HideInInspector] public Vector3 bottomLeft, bottomRight, topLeft, topRight;
    [HideInInspector] public Vector3 dirRight, dirUp, dirNormal;
    public Matrix4x4 m;

    private void Update()
    {
        if (lockAspectRatio)
        {
            if (!Mathf.Approximately(aspectRatio.x, _previousAspectRatio.x))
            {
                size.y = size.x / aspectRatio.x * aspectRatio.y;
                _previousAspectRatio.y = aspectRatio.y;
            }

            if (!Mathf.Approximately(aspectRatio.y, _previousAspectRatio.y))
                size.x = size.y / aspectRatio.y * aspectRatio.x; //X takes precedence
            if (!Mathf.Approximately(size.x, _previousSize.x))
            {
                size.y = size.x / aspectRatio.x * aspectRatio.y;
                _previousSize.y = size.y;
            }

            if (!Mathf.Approximately(size.y, _previousSize.y))
                size.x = size.y / aspectRatio.y * aspectRatio.x; //X takes precedence
        }

        // Safety precautions
        size.x = Mathf.Max(.1f, size.x);
        size.y = Mathf.Max(.1f, size.y);
        aspectRatio.x = Mathf.Max(1, aspectRatio.x);
        aspectRatio.y = Mathf.Max(1, aspectRatio.y);
        _previousSize = size;
        _previousAspectRatio = aspectRatio;
        // Update the projection plane
        bottomLeft = transform.TransformPoint(new Vector3(-size.x, -size.y) * .5f);
        bottomRight = transform.TransformPoint(new Vector3(size.x, -size.y) * .5f);
        topLeft = transform.TransformPoint(new Vector3(-size.x, size.y) * .5f);
        topRight = transform.TransformPoint(new Vector3(size.x, size.y) * .5f);
        dirRight = (bottomRight - bottomLeft).normalized;
        dirUp = (topLeft - bottomLeft).normalized;
        dirNormal = -Vector3.Cross(dirRight, dirUp).normalized;

        m = new Matrix4x4(
            new Vector4(dirRight.x, dirRight.y, dirRight.z, 0),
            new Vector4(dirUp.x, dirUp.y, dirUp.z, 0),
            new Vector4(dirNormal.x, dirNormal.y, dirNormal.z, 0),
            new Vector4(0, 0, 0, 1)
        );
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!drawGizmos) return;
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, (size.x + size.y) * .01f);
        Gizmos.color = Color.white;
        Gizmos.DrawLineStrip(new[] { bottomLeft, bottomRight, topRight, topLeft }, true);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(bottomLeft + (topRight - bottomLeft) * 0.5f, dirNormal * (size.x + size.y) * .1f);
    }
#endif
}