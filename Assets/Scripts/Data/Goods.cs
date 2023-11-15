using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goods
{
    //所有物品更新数据
    public static int UpdateCount = 0;
    //所有物品当前数据
    public static int CurrentCount = 0;
    //装备更新数据
    public static int EquipUpdateCount = 0;
    //装备当前数据
    public static int EquipCount = 0;
    //材料更新数据
    public static int MaterialUpdateCount = 0;
    //材料当前数据
    public static int MaterialCount = 0;
    //碎片更新数据
    public static int SplintersUpdateCount = 0;
    //碎片当前数据
    public static int SplintersCount = 0;

    public static List<Good> allGoods = new List<Good>();
    public static List<Good> goods = new List<Good>();
    public static List<Good> equipmentGoods = new List<Good>();
    public static List<Good> materialGoods = new List<Good>();
    public static List<Good> splintersGoods = new List<Good>();
}

public class Good
{
    //物品id
    public int ID { get; set; }
    //物品名字
    public string Name { get; set; }
    //物品类型
    public int Type { get; set; }
    //物品图标
    public string IconName { get; set; }
    //出售价格
    public int SellPrice { get; set; }
    //物品描述
    public string Introduce { get; set; }

}