
public class PlayerHP
{
    int playerHP;

    public PlayerHP(int hp)
    {
        this.playerHP = hp;
    }

    public int getHP()
    {
        return playerHP;
    }

    public void ConsumeHP(int amount)
    {
        this.playerHP -= amount;
    }

}
