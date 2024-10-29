using System.Collections;
using UnityEngine;

public class ShootPlatform : MonoBehaviour, IInteractiveObject
{
    string IInteractiveObject.Title { get { return "�߻��"; } }

    string IInteractiveObject.Description { get { return "������ �ָ� �� �� �ִ�."; } }

    [SerializeField] private Vector3 shootDirection;
    [SerializeField] private float shootPower;
    [SerializeField] private float shootDuration;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Player.PLAYER_TAG))
        {
            Debug.Log("Waiting");
            CharacterManager.Instance.Player.controller.StartCoroutine(Shoot(collision.gameObject));
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(Player.PLAYER_TAG))
        {
            CharacterManager.Instance.Player.controller.StopCoroutine();
        }
    }

    private IEnumerator Shoot(GameObject gameObject)
    {
        yield return new WaitForSeconds(shootDuration);
        gameObject.GetComponent<PlayerController>().OnStepPlatform(shootDirection, shootPower);
        CharacterManager.Instance.Player.controller.StopCoroutine();
    }
}
