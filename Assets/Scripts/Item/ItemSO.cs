using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName ="ItemSO/CreateItem")]
public class ItemSO : ScriptableObject
{
    [Header("Item Info")]
    public ItemType itemType;
    public string itemName;
    public string itemDescription;
    public float value;
    public float duration;

    [Header("Prefab")]
    public GameObject itemPrefab;
}

public enum ItemType
{
    HEAL,
    HUNGER,
    STAMINA,
    BOOST
}
