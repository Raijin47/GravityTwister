public class Data
{
    public int[] CurrentEquip = new int[11];
    public bool[,] IsPurchased = new bool[11, 80];

    public Data()
    {
        IsPurchased[0, 0] = true;
        IsPurchased[1, 0] = true;
        IsPurchased[2, 0] = true;
        IsPurchased[3, 0] = true;
        IsPurchased[4, 0] = true;
        IsPurchased[5, 0] = true;
        IsPurchased[6, 0] = true;
        IsPurchased[7, 0] = true;
        IsPurchased[8, 0] = true;
        IsPurchased[9, 0] = true;
        IsPurchased[10, 0] = true;
    }
}