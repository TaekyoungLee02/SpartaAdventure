using UnityEngine;

public class StaminaItemFactory : ItemFactory
{
    protected override Item CreateItem()
    {
        var gameObject = Object.Instantiate(so.itemPrefab);
        return gameObject.AddComponent<StaminaItem>();
    }
}
