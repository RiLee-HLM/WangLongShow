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
        Debug.Log("���ͣ�" + data[0]);
        //����һ����ҹ�ȥ
        MessageCenter.SendMessage(E_MessageType.ItemBeSell, data);
    }
}