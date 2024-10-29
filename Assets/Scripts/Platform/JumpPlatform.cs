using UnityEngine;

public class JumpPlatform : MonoBehaviour, IInteractiveObject
{
    string IInteractiveObject.Title { get { return "점프대"; } }

    string IInteractiveObject.Description { get { return "밟으면 높이 점프할 수 있다."; } }

    [SerializeField] private Vector3 jumpDirection;
    [SerializeField] private float jumpPower;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Player.PLAYER_TAG))
        {
            collision.gameObject.GetComponent<PlayerController>().OnStepPlatform(jumpDirection.normalized, jumpPower);
        }
    }
}
