using UnityEngine;

public class HungerItemFactory : ItemFactory
{
    protected override Item CreateItem()
    {
        var gameObject = Object.Instantiate(so.itemPrefab);
        return gameObject.AddComponent<HungerItem>();
    }
}
