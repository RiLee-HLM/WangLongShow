using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ReadJson
{
    public static void ParseJson()
    {
        TextAsset text = Resources.Load<TextAsset>("Json/Goods");
        string json = text.text;
        Dictionary<string, Good> goodsDict = new Dictionary<string, Good>();
        goodsDict = JsonMapper.ToObject<Dictionary<string, Good>>(json);
        /* Debug.Log(goodsDict["1001"]);
         Debug.Log(goodsDict.Count);*/

        //�����е���Ʒ��ӵ�Goods��list��
        foreach (Good good in goodsDict.Values)
        {
            Goods.goods.Add(good);
        }
        text = Resources.Load<TextAsset>("Json/AllGoods");
        json = text.text;
        goodsDict = new Dictionary<string, Good>();
        goodsDict = JsonMapper.ToObject<Dictionary<string, Good>>(json);
        /* Debug.Log(goodsDict["1001"]);
         Debug.Log(goodsDict.Count);*/

        //�����е���Ʒ��ӵ�Goods��list��
        foreach (Good good in goodsDict.Values)
        {
            Goods.allGoods.Add(good);
        }
        Debug.Log(Goods.goods.Count);
        Debug.Log(Goods.allGoods.Count);
        /* Debug.Log(Goods.goods[0].Name);
         Debug.Log(Goods.goods[0].ID);
         Debug.Log(Goods.goods[0].IconName);
         Debug.Log(Goods.goods[0].SellPrice);
         Debug.Log(Goods.goods[0].Introduce);
 */
        /*  Debug.Log(Goods.allGoods[0].Name);
          Debug.Log(Goods.allGoods[0].ID);
          Debug.Log(Goods.allGoods[0].IconName);
          Debug.Log(Goods.allGoods[0].SellPrice);
          Debug.Log(Goods.allGoods[0].Introduce);*/

        //��ǰ����
        Goods.CurrentCount = Goods.goods.Count;
        Goods.EquipCount = GetJsonValue(0, 1).Count;
        Goods.MaterialCount = GetJsonValue(0, 2).Count;
        Goods.SplintersCount = GetJsonValue(0, 3).Count;
    }

    /// <summary>
    /// ����Goods�����ĳһ��ֵ
    /// </summary>
    /// <param name="id">����ID��ѯ</param>
    /// <param name="type">����Type��ѯ</param>
    /// <returns></returns>
    public static List<Good> GetJsonValue(int id = 0, int type = 0)
    {
        if (id != 0)
        {
            List<Good> findGood = Goods.goods.FindAll((Good item) => item.ID == id);

            return findGood;
        }
        else if (type != 0)
        {
            /* if (type == 1)
             {*/
            List<Good> findGood = Goods.goods.FindAll((Good item) => item.Type == type);
            return findGood;
            /* }
             if (type == 2)
             {
                 List<Good> findGood = Goods.goods.FindAll((Good item) => item.Type == type);
                 return findGood;
             }
             if (type == 3)
             {
                 List<Good> findGood = Goods.goods.FindAll((Good item) => item.Type == type);
                 return findGood;
             }
             return null;*/
        }
        else
        {
            Debug.LogError("Ϊ��������ֵ������");
            return null;
        }
    }

    /// <summary>
    /// �ж��Ƿ��Ѿ�����ͬ����Ʒ
    /// </summary>
    /// <param name="id">����ID��ѯ</param>
    /// <returns></returns>
    public static bool JudgeHasGoods(int id)
    {
        if (Goods.goods.Find(t => t.ID == id) == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
