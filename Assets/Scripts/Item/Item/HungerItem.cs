using System.Collections;
public class HungerItem : Item
{
    public override void Use()
    {
        player.status.stats[(int)PlayerStatus.StatusType.HUNGER].Add(itemValue);
    }
}