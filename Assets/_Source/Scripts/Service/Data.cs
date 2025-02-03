public class Data
{
    public int[] CurrentEquip = new int[20];
    public bool[,] IsPurchased = new bool[20, 20];

    public Data()
    {
        IsPurchased[0, 0] = true;
        IsPurchased[1, 0] = true;
        IsPurchased[2, 0] = true;
        CurrentEquip[0] = 0;
        CurrentEquip[1] = 0;
        CurrentEquip[2] = 0;
    }
}