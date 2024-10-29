public abstract class ItemFactory
{
    public ItemSO so;

    public Item Create()
    {
        Item item = CreateItem();
        item.Init(so);
        return item;
    }

    protected abstract Item CreateItem();
}
