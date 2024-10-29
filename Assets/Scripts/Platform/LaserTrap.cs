using UnityEngine;

public class LaserTrap : MonoBehaviour
{
    public Transform reciever;

    private Ray ray;
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        ray = new(transform.position, (reciever.position - transform.position).normalized);

        Debug.Log(transform.position);
        Debug.Log(reciever.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(ray, out hit))
        {
            if(hit.transform.CompareTag(Player.PLAYER_TAG))
            {
                Debug.Log("TRAP!");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray);
    }
}
