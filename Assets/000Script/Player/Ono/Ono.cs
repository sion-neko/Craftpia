public class Ono : IOno
{
    private int lv;
    private int atk;

    public Ono(int lv, int atk)
    {
        this.lv = lv;
        this.atk = atk;
    }

    //void setLv(int lv)
    //{
    //    this.lv = lv;
    //}
    //void setAtk(int atk)
    //{
    //    this.atk = atk;
    //}

    public int getLv()
    {
        return this.lv;
    }

    public int getAtk()
    {
        return this.atk;
    }

}
