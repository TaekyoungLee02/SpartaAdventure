using UnityEngine;

public class BoostItemFactory : ItemFactory
{
    protected override Item CreateItem()
    {
        var gameObject = Object.Instantiate(so.itemPrefab);
        return gameObject.AddComponent<BoostItem>();
    }
}
