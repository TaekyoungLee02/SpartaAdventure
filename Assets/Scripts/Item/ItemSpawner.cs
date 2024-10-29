using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    private List<ItemSO> itemSOs;
    private ItemFactory[] itemFactories;

    private void Awake()
    {
        itemSOs = new List<ItemSO>();

        var so = Resources.LoadAll("ScriptableObjects/");

        for (int i = 0; i < so.Length; i++)
        {
            itemSOs.Add(so[i] as ItemSO);
        }

        itemFactories = new ItemFactory[]
        {
            new HealthItemFactory(),
            new HungerItemFactory(),
            new StaminaItemFactory(),
            new BoostItemFactory()
        };
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnItem();
    }

    public void SpawnItem()
    {
        for(int i = 0; i < itemSOs.Count; i++)
        {
            var type = itemSOs[i].itemType;
            itemFactories[(int)type].so = itemSOs[i];

            var item = itemFactories[(int)type].Create();
            item.transform.position = new(4, 0.5f, i);
        }
    }
}
