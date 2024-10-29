using System.Collections;
using UnityEngine;

public class BoostItem : Item
{
    public override void Use()
    {
        player.controller.StartCoroutine(BoostCoroutine());
    }

    private IEnumerator BoostCoroutine()
    {
        player.controller.additionalMoveSpeed += itemValue;
        yield return new WaitForSeconds(itemDuration);
        player.controller.additionalMoveSpeed -= itemValue;
        player.controller.StopCoroutine();
    }
}
