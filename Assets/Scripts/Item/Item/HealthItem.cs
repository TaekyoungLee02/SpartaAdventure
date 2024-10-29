public class HealthItem : Item
{
    public override void Use()
    {
        player.status.stats[(int)PlayerStatus.StatusType.HEALTH].Add(itemValue);
    }
}