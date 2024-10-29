using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    [HideInInspector] public PlayerStatus.Status status;
    public PlayerStatus.StatusType statusType;
    public Image uiBar;


    private void Start()
    {
        status = CharacterManager.Instance.Player.status.stats[(int)statusType];
    }

    private void Update()
    {
        uiBar.fillAmount = GetPercentage();
    }

    private float GetPercentage()
    {
        return status.curValue / status.maxValue;
    }
}
