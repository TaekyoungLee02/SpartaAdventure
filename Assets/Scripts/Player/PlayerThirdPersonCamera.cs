using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerThirdPersonCamera : MonoBehaviour
{
    [Header("Look")]
    public Transform cameraOrigin;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;
    private Vector2 mouseDelta;

    public float distance;

    public Vector3 cameraDirection;
    private Ray cameraRay;
    private Ray cameraRenderRay;
    private RaycastHit hit;
    private RaycastHit renderHit;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = cameraOrigin.position + cameraOrigin.TransformDirection(cameraDirection);

        cameraRay = new(cameraOrigin.position, cameraOrigin.TransformDirection(cameraDirection));
        if (Physics.Raycast(cameraRay, out hit, Vector3.Distance(cameraOrigin.position, transform.position)))
        {
            transform.position = hit.point;
        }
        CameraLook();

        cameraRenderRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(cameraRenderRay, out renderHit, Camera.main.nearClipPlane) && renderHit.transform.CompareTag(Player.PLAYER_TAG))
        {
            Camera.main.cullingMask = Camera.main.cullingMask & ~(1 << renderHit.transform.gameObject.layer);
        }
        else
        {
            Camera.main.cullingMask = -1;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(cameraRay);
    }

    private void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        transform.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.parent.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }
}
