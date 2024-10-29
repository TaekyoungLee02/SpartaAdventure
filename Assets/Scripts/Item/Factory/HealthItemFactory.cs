using UnityEngine;

public class HealthItemFactory : ItemFactory
{
    protected override Item CreateItem()
    {
        var gameObject = Object.Instantiate(so.itemPrefab);
        return gameObject.AddComponent<HealthItem>();
    }
}
