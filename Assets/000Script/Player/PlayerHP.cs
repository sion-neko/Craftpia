using System;


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

    public bool ConsumeHP(int amount)
    {

        if (this.playerHP > amount)
        {
            this.playerHP -= amount;
            return true;

        }
        else
        {
            this.playerHP = 0;
            return false;
        }
    }
}
