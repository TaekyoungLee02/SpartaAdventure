using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask layerMask;

    private RaycastHit hit;
    private Ray playerCameraRay;

    [HideInInspector] public GameObject hitObject;

    // Update is called once per frame
    void Update()
    {
        playerCameraRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if(Physics.Raycast(playerCameraRay, out hit, maxDistance, layerMask))
            hitObject = hit.transform.gameObject;
        else hitObject = null;
    }
}
