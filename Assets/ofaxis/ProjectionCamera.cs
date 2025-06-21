using UnityEngine;

/**
 * <summary>Projection Camera is a camera that uses a projection plane to project a texture or image onto.</summary>
 * <remarks>Original Source: https://medium.com/try-creative-tech/off-axis-projection-in-unity-1572d826541e</remarks>
 */
// ReSharper disable Unity.RedundantAttributeOnTarget MemberCanBePrivate.Global
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class ProjectionCamera : MonoBehaviour
{
    [HideInInspector] public Vector3 ViewDir => projectionScreen.transform.position - EyePos;
    [SerializeField] private ProjectionPlane projectionScreen;
    [SerializeField] private bool clampNearPlane;
    public Camera _cam;
    private Vector3 EyePos => transform.position;
    private Vector3 _bl, _br, _tl; // projection screen corners
    private float _n, _f, _l, _r, _b, _t; // matrix variables

    

    private void LateUpdate()
    {
        if (!projectionScreen) return;
        _bl = projectionScreen.bottomLeft - EyePos;
        _br = projectionScreen.bottomRight - EyePos;
        _tl = projectionScreen.topLeft - EyePos;

        //Distance from eye to projection screen plane
        var d = -Vector3.Dot(_bl, projectionScreen.dirNormal);
        if (clampNearPlane) _cam.nearClipPlane = d;
        _n = _cam.nearClipPlane;
        _f = _cam.farClipPlane;
        var near = _n / d;
        _l = Vector3.Dot(projectionScreen.dirRight, _bl) * near;
        _r = Vector3.Dot(projectionScreen.dirRight, _br) * near;
        _b = Vector3.Dot(projectionScreen.dirUp, _bl) * near;
        _t = Vector3.Dot(projectionScreen.dirUp, _tl) * near;

        //Translation to eye position
        var translate = Matrix4x4.Translate(-EyePos);

        //Projection matrix
        _cam.worldToCameraMatrix = projectionScreen.m * translate;
        _cam.projectionMatrix = Matrix4x4.Frustum(_l, _r, _b, _t, _n, _f);
    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (!projectionScreen) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(EyePos, ViewDir);
    }
#endif
}