using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goods
{
    //������Ʒ��������
    public static int UpdateCount = 0;
    //������Ʒ��ǰ����
    public static int CurrentCount = 0;
    //װ����������
    public static int EquipUpdateCount = 0;
    //װ����ǰ����
    public static int EquipCount = 0;
    //���ϸ�������
    public static int MaterialUpdateCount = 0;
    //���ϵ�ǰ����
    public static int MaterialCount = 0;
    //��Ƭ��������
    public static int SplintersUpdateCount = 0;
    //��Ƭ��ǰ����
    public static int SplintersCount = 0;

    public static List<Good> allGoods = new List<Good>();
    public static List<Good> goods = new List<Good>();
    public static List<Good> equipmentGoods = new List<Good>();
    public static List<Good> materialGoods = new List<Good>();
    public static List<Good> splintersGoods = new List<Good>();
}

public class Good
{
    //��Ʒid
    public int ID { get; set; }
    //��Ʒ����
    public string Name { get; set; }
    //��Ʒ����
    public int Type { get; set; }
    //��Ʒͼ��
    public string IconName { get; set; }
    //���ۼ۸�
    public int SellPrice { get; set; }
    //��Ʒ����
    public string Introduce { get; set; }

}