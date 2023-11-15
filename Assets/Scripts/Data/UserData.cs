using UICore;
using UnityEngine;

public class UserData
{
    public static int coin = 0;
    public static int Coin { get => coin; set => coin = value; }

    public static void SellAddCoin(int addCoin)
    {
        coin += addCoin;
        object[] data;
        data = new object[] { coin };
        Debug.Log("发送：" + data[0]);
        //传递一个金币过去
        MessageCenter.SendMessage(E_MessageType.ItemBeSell, data);
    }
}