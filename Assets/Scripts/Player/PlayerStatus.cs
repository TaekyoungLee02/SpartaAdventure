using System.Collections;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public Status[] stats;

    public float noHungerHealthDecay;
    public float staminaRechargeWaitingTime;

    private bool staminaExhausted = false;

    private void Start()
    {
        for(int i = 0; i < stats.Length; i++)
        {
            stats[i].curValue = stats[i].maxValue;
        }
    }

    private void Update()
    {
        if (stats[(int)StatusType.STAMINA].curValue <= 0f && !staminaExhausted)
        {
            OnStaminaExhaust();
        }

        if (stats[(int)StatusType.HUNGER].curValue <= 0f)
        {
            stats[(int)StatusType.HEALTH].Add(noHungerHealthDecay * Time.deltaTime);
        }

        if (stats[(int)StatusType.HEALTH].curValue <= 0f)
        {
            Die();
        }



        for (int i = 0; i < stats.Length; i++)
        {
            stats[i].Add(stats[i].passiveValue * Time.deltaTime);
        }
    }

    private void Die()
    {

    }

    private void OnStaminaExhaust()
    {
        StartCoroutine(StaminaRechargeWait(staminaRechargeWaitingTime));
        staminaExhausted = true;
    }

    private IEnumerator StaminaRechargeWait(float waitingTime)
    {
        float temp = stats[(int)StatusType.STAMINA].passiveValue;
        stats[(int)StatusType.STAMINA].passiveValue = 0;
        yield return new WaitForSeconds(waitingTime);
        stats[(int)StatusType.STAMINA].passiveValue = temp;
        stats[(int)StatusType.STAMINA].Add(stats[(int)StatusType.STAMINA].passiveValue * Time.deltaTime);
        staminaExhausted = false;
    }

    public enum StatusType
    {
        HEALTH,
        HUNGER,
        STAMINA
    }

    [System.Serializable]
    public class Status
    {
        public StatusType type;
        public float curValue;
        public float maxValue;
        public float passiveValue;

        public void Add(float value)
        {
            curValue = Mathf.Clamp(curValue + value, 0, maxValue);
        }
    }
}