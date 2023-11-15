using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UICore;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUI : BaseUI
{
    //�Ƿ��ڷ�Χ��
    private bool isOver = false;//����Ƿ���ͣ

    private Color startColor = Color.white;
    private Color eneterColor = Color.gray;

    Text text_name, text_item, text_introduce, text_sell;

    Good good;

    Button btn_sell;

    List<Good> goodList;

    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();
        /* btn_sell = transform.Find("Sell").gameObject.GetComponent<Button>();
         btn_sell.onClick.AddListener(() => SellGoods());*/
    }
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        this.uiId = E_UiId.ItemUI;
        this.uiType.showMode = E_ShowUIMode.DoNothing;
        this.uiType.uiRootType = E_UIRootType.KeepAbove;

        /* goodList = new List<Good>();*/
    }

    /* public void OnPointerEnter(PointerEventData eventData)
     {
         Debug.Log(Input.mousePosition);
         eventData.Reset();

         UIManager.Instance.ShowUI(uiId);

         //��ʾUI
         GameObject.Find("ItemBox").transform.position = new Vector3(Input.mousePosition.x + 200, Input.mousePosition.y - 200, Input.mousePosition.z);

         //��ȡ��ǰ��Good��Ϣ
         *//* text_name = GameTool.GetTheChildComponent<Text>(gameObject, "ItemName");
          text_introduce = GameTool.GetTheChildComponent<Text>(gameObject, "ItemIntroduce");
          text_sell = GameTool.GetTheChildComponent<Text>(gameObject, "SellPrice");*//*

         text_name = GameObject.Find("ItemName").GetComponent<Text>();
         text_introduce = GameObject.Find("ItemIntroduce").GetComponent<Text>();
         text_sell = GameObject.Find("SellPrice").GetComponent<Text>();


         good = Goods.goods.Find(item => item.ID == BagDataManager.GetID(gameObject));
         text_name.text = good.Name;
         text_introduce.text = "   ��Ʒ������" + good.Introduce;
         text_sell.text = "   ���۽�" + good.SellPrice.ToString();


         goodList.Add(good);
         Debug.Log("������룺" + goodList.Count);
         //���۰�ť
         transform.Find("Sell").gameObject.SetActive(true);



         *//*�ı���ɫ*//*
         gameObject.GetComponent<Image>().color = eneterColor;
         *//* gameObject.transform.position = Input.mousePosition;*//*
     }*/

    /// <summary>
    /// ������Ʒ
    /// </summary>
   /* private void SellGoods()
    {

        Good sellGood = goodList[0];
        Debug.Log("���룺" + sellGood.SellPrice);
        UserData.SellAddCoin(sellGood.SellPrice);

    }*/

    /* public void OnPointerExit(PointerEventData eventData)
     {
         Debug.Log(BagDataManager.GetID(gameObject));


         transform.Find("Sell").gameObject.SetActive(false);
         *//*�ı���ɫ*//*
         gameObject.GetComponent<Image>().color = startColor;
         *//* isOver = false;*//*
         UIManager.Instance.HideSingleUI(uiId);
         goodList.Clear();
         Debug.Log("������գ�" + goodList.Count);
         //eventData.clickCount = 0;
         Debug.Log("eventData.clickCount:" + eventData.clickCount);
     }*/
}