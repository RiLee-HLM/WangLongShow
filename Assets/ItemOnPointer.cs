using System.Collections.Generic;
using UICore;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemOnPointer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    Text text_name, text_item, text_introduce, text_sell;

    private Color startColor = Color.white;
    private Color eneterColor = Color.gray;

    Good good;
    List<Good> goodList;


    Button btn_sell;
    private void OnEnable()
    {
        goodList = new List<Good>();

        btn_sell = transform.Find("Sell").gameObject.GetComponent<Button>();
        btn_sell.onClick.AddListener(() => SellGoods());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(Input.mousePosition);
        eventData.Reset();

        UIManager.Instance.ShowUI(E_UiId.ItemUI);

        //显示UI
        GameObject.Find("ItemBox").transform.position = new Vector3(Input.mousePosition.x + 200, Input.mousePosition.y - 200, Input.mousePosition.z);

        //获取当前的Good信息
        /* text_name = GameTool.GetTheChildComponent<Text>(gameObject, "ItemName");
         text_introduce = GameTool.GetTheChildComponent<Text>(gameObject, "ItemIntroduce");
         text_sell = GameTool.GetTheChildComponent<Text>(gameObject, "SellPrice");*/

        text_name = GameObject.Find("ItemName").GetComponent<Text>();
        text_introduce = GameObject.Find("ItemIntroduce").GetComponent<Text>();
        text_sell = GameObject.Find("SellPrice").GetComponent<Text>();


        good = Goods.goods.Find(item => item.ID == BagDataManager.GetID(gameObject));
        Debug.Log("测试Goods：" + good);
        text_name.text = good.Name;
        text_introduce.text = "   物品描述：" + good.Introduce;
        text_sell.text = "   出售金额：" + good.SellPrice.ToString();


        goodList.Add(good);
        Debug.Log("鼠标移入：" + goodList.Count);
        //出售按钮
        transform.Find("Sell").gameObject.SetActive(true);



        /*改变颜色*/
        gameObject.GetComponent<Image>().color = eneterColor;
        /* gameObject.transform.position = Input.mousePosition;*/
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.Find("Sell").gameObject.SetActive(false);
        /*改变颜色*/
        gameObject.GetComponent<Image>().color = startColor;
        UIManager.Instance.HideSingleUI(E_UiId.ItemUI);
        goodList.Clear();
    }

    private void SellGoods()
    {
        Good sellGood = goodList[0];
        Debug.Log("传入：" + sellGood.SellPrice);
        UserData.SellAddCoin(sellGood.SellPrice);
        Debug.Log("传入类型：" + goodList[0].Type);
        //摧毁全部里面的Item然后隐藏
        if (goodList[0].Type != 1 && goodList[0].Type != 2 && goodList[0].Type != 3)
        {
            Debug.Log("没有类型");
            Destroy(btn_sell.transform.parent.gameObject);
        }
        UIManager.Instance.HideSingleUI(E_UiId.ItemUI);


        //摧毁或者减少对应的Item
        if (goodList[0].Type == 1)
        {
            GameObject objectData = BagDataManager.GetEquie(goodList[0].ID);
            /* if (int.Parse(objectData.transform.Find("ItemCount").GetComponent<Text>().text) == 1)
             {*/
            Destroy(objectData);
            //移除这个物体的ID
            BagDataManager.RemoveID(btn_sell.transform.parent.gameObject);
            Goods.equipmentGoods.Remove(Goods.goods.Find(x => x.ID == goodList[0].ID));
            BagDataManager.RemoveItem(goodList[0].ID);
            Goods.equipmentGoods.Remove(Goods.equipmentGoods.Find(x => x.ID == goodList[0].ID));
            Goods.equipmentGoods.Remove(Goods.equipmentGoods.Find(x => x.ID == goodList[0].ID));
            BagDataManager.RemoveID(btn_sell.transform.parent.gameObject);
            BagUI.items.Remove(goodList[0].ID);
            /*Goods.equipmentGoods.Remove(Goods.goods.Find(x => x.ID == goodList[0].ID));
            Goods.goods.Remove(Goods.goods.Find(x => x.ID == goodList[0].ID));
            BagDataManager.RemoveID(btn_sell.transform.parent.gameObject);*/
            /* }
             if (int.Parse(objectData.transform.Find("ItemCount").GetComponent<Text>().text) >= 1)
             {
                 int count = int.Parse(objectData.transform.Find("ItemCount").GetComponent<Text>().text);
                 objectData.transform.Find("ItemCount").GetComponent<Text>().text = (count - 1).ToString();
             }*/
        }
        if (goodList[0].Type == 2)
        {
            GameObject objectData = BagDataManager.GetMaterial(goodList[0].ID);
            if (int.Parse(objectData.transform.Find("ItemCount").GetComponent<Text>().text) == 1)
            {
                /*Destroy(objectData);*/
                objectData.SetActive(false);
                Goods.materialGoods.Remove(Goods.goods.Find(x => x.ID == goodList[0].ID));
                BagDataManager.RemoveItem(goodList[0].ID);
                Goods.goods.Remove(Goods.goods.Find(x => x.ID == goodList[0].ID));
                Goods.materialGoods.Remove(Goods.materialGoods.Find(x => x.ID == goodList[0].ID));
                BagDataManager.RemoveID(btn_sell.transform.parent.gameObject);
                BagUI.items.Remove(goodList[0].ID);
            }
            if (int.Parse(objectData.transform.Find("ItemCount").GetComponent<Text>().text) >= 1)
            {
                int count = int.Parse(objectData.transform.Find("ItemCount").GetComponent<Text>().text);
                objectData.transform.Find("ItemCount").GetComponent<Text>().text = (count - 1).ToString();
                //当Text =1的时候隐藏
                if (int.Parse(objectData.transform.Find("ItemCount").GetComponent<Text>().text) == 1)
                {
                    objectData.transform.Find("ItemCount").gameObject.SetActive(false);
                }
            }
        }
        if (goodList[0].Type == 3)
        {
            GameObject objectData = BagDataManager.GetSplinters(goodList[0].ID);
            Debug.Log(int.Parse(objectData.transform.Find("ItemCount").GetComponent<Text>().text));
            if (int.Parse(objectData.transform.Find("ItemCount").GetComponent<Text>().text) == 1)
            {
                /*Destroy(objectData);*/
                objectData.SetActive(false);

                Goods.splintersGoods.Remove(Goods.goods.Find(x => x.ID == goodList[0].ID));
                BagDataManager.RemoveItem(goodList[0].ID);
                Goods.goods.Remove(Goods.goods.Find(x => x.ID == goodList[0].ID));
                Goods.splintersGoods.Remove(Goods.splintersGoods.Find(x => x.ID == goodList[0].ID));
                BagDataManager.RemoveID(btn_sell.transform.parent.gameObject);
                BagUI.items.Remove(goodList[0].ID);
            }
            else
            if (int.Parse(objectData.transform.Find("ItemCount").GetComponent<Text>().text) >= 1)
            {
                Debug.Log("测试是否进入3");
                int count = int.Parse(objectData.transform.Find("ItemCount").GetComponent<Text>().text);
                objectData.transform.Find("ItemCount").GetComponent<Text>().text = (count - 1).ToString();
                //当Text =1的时候隐藏
                if (int.Parse(objectData.transform.Find("ItemCount").GetComponent<Text>().text) == 1)
                {
                    objectData.transform.Find("ItemCount").gameObject.SetActive(false);
                }
            }
        }

        //清理集合
        goodList.Clear();
    }
}
