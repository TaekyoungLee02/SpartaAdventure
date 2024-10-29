using UnityEngine;

public class JumpPlatform : MonoBehaviour, IInteractiveObject
{
    string IInteractiveObject.Title { get { return "������"; } }

    string IInteractiveObject.Description { get { return "������ ���� ������ �� �ִ�."; } }

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
