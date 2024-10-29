using UnityEngine;

public abstract class Item : MonoBehaviour, IInteractiveObject
{
    private string itemName;
    private string itemDescription;

    protected float itemValue;
    protected float itemDuration;
    protected Player player;

    string IInteractiveObject.Title { get { return itemName; } }
    string IInteractiveObject.Description { get { return itemDescription; } }

    protected virtual void Start()
    {
        player = CharacterManager.Instance.Player;
    }

    public virtual void Init(ItemSO so)
    {
        itemName = so.itemName;
        itemDescription = so.itemDescription;
        itemValue = so.value;
        itemDuration = so.duration;
    }

    public abstract void Use();

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(Player.PLAYER_TAG))
        {
            Use();
            Destroy(gameObject);
        }
    }
}
