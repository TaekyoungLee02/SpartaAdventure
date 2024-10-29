public class StaminaItem : Item
{
    public override void Use()
    {
        player.status.stats[(int)PlayerStatus.StatusType.STAMINA].Add(itemValue);
    }
}
