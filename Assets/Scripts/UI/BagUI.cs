using System.Collections.Generic;
using UICore;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BagUI : BaseUI
{
    Button btn_addItem;
    Toggle[] toggles;
    ScrollRect[] scroll;

    //储存生成过的Item
    public static Dictionary<int, GameObject> items;
    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();

        //添加Tggle数组
        toggles = transform.Find("ToggleGroup").GetComponentsInChildren<Toggle>();
        foreach (Toggle toggle in toggles)
        {
            toggle.onValueChanged.AddListener((bool isOn) => ItemTypeToggle(toggle));
            //默认打开的是全部装备
            if (toggle.name == "AllToggle" && !toggle.isOn)
            {
                toggle.isOn = true;
            }
        }

        btn_addItem = GameTool.GetTheChildComponent<Button>(gameObject, "AddItemButton");
        btn_addItem.onClick.AddListener(AddItem);

        scroll = new ScrollRect[]
        {
            transform.Find("ItemView/AllGoodsScrollView").transform.GetComponent<ScrollRect>(),
        transform.Find("ItemView/EquipGoodsScrollView").transform.GetComponent<ScrollRect>(),
        transform.Find("ItemView/MaterialGoodsScrollView").transform.GetComponent<ScrollRect>(),
        transform.Find("ItemView/SplintersGoodsScrollView").transform.GetComponent<ScrollRect>(),
        };

        InitItemData();

        /* Debug.Log(GetOverUI(gameObject.transform.parent.gameObject).name);*/
    }
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        this.uiId = E_UiId.BagUI;
        this.uiType.showMode = E_ShowUIMode.HideOther;

        items = new Dictionary<int, GameObject>();
        BagDataManager.items = new Dictionary<int, GameObject>();
        BagDataManager.equies = new Dictionary<int, GameObject>();
        BagDataManager.materials = new Dictionary<int, GameObject>();
        BagDataManager.Splinters = new Dictionary<int, GameObject>();
        BagDataManager.ids = new Dictionary<GameObject, int>();

        //初始化所有的数据
        ReadJson.ParseJson();
    }

    /// <summary>
    /// 添加物品
    /// </summary>
    private void AddItem()
    {
        Debug.Log("点击按钮！！！");
        Good goods = Goods.allGoods[Random.Range(0, Goods.allGoods.Count)];

        //添加一个到全部
        GameObject insGood = Instantiate(Resources.Load<GameObject>("Item/GoodItem"));
        insGood.transform.SetParent(scroll[0].transform.Find("Viewport/BagContent").transform, false);
        insGood.transform.Find("GoodItem").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + goods.IconName);
        /* //添加到我拥有的集合
         Goods.goods.Add(goods);*/
        //添加生成过全部的物体
        BagDataManager.AddItem(goods.ID, insGood);
        /*添加生成所有的ID*/
        BagDataManager.AddID(insGood, goods.ID);

        if (goods.Type == 1)
        {
            //添加一个到全部
            GameObject insGood1 = Instantiate(Resources.Load<GameObject>("Item/GoodItem"));
            insGood1.transform.SetParent(scroll[1].transform.Find("Viewport/BagContent").transform, false);
            insGood1.transform.Find("GoodItem").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + goods.IconName);
            Goods.equipmentGoods.Add(goods);
            Goods.goods.Add(goods);
            BagDataManager.AddItem(goods.ID, insGood1);
            BagDataManager.AddID(insGood1, goods.ID);
            //添加到材料
            BagDataManager.AddEquie(goods.ID, insGood1);
        }
        if (goods.Type == 2)
        {
            Debug.Log("判断值：" + ReadJson.JudgeHasGoods(goods.ID) + goods.ID);

            if (ReadJson.JudgeHasGoods(goods.ID))
            {
                Debug.Log("已经存在！！");
                //根据ID查已经有多少个
                Debug.Log(ReadJson.GetJsonValue(goods.ID, 0).Count);
                /*Debug.Log(items[goods.ID].name);
                Debug.Log(items[goods.ID].transform.Find("ItemCount").name);*/
                items[goods.ID].transform.Find("ItemCount").GetComponent<Text>().text = (ReadJson.GetJsonValue(goods.ID, 0).Count + 1).ToString();
                items[goods.ID].transform.Find("ItemCount").gameObject.SetActive(true);
            }
            if (!ReadJson.JudgeHasGoods(goods.ID))
            {
                Debug.Log("正在生成：" + goods.Name);
                //添加一个到全部
                GameObject insGood2 = Instantiate(Resources.Load<GameObject>("Item/GoodItem"));
                insGood2.transform.SetParent(scroll[2].transform.Find("Viewport/BagContent").transform, false);
                insGood2.transform.Find("GoodItem").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + goods.IconName);
                Goods.materialGoods.Add(goods);
                Goods.goods.Add(goods);
                //将物体添加到集合中
                items.Add(goods.ID, insGood2);

                BagDataManager.AddItem(goods.ID, insGood2);
                BagDataManager.AddID(insGood2, goods.ID);
                //添加到材料
                BagDataManager.AddMaterial(goods.ID, insGood2);
            }
        }
        if (goods.Type == 3)
        {
            Debug.Log("判断值：" + ReadJson.JudgeHasGoods(goods.ID) + goods.ID);
            if (ReadJson.JudgeHasGoods(goods.ID))
            {
                Debug.Log("已经存在！！");
                /*ReadJson.GetJsonValue(goods.ID, 0).Count;*/
                items[goods.ID].transform.Find("ItemCount").GetComponent<Text>().text = (ReadJson.GetJsonValue(goods.ID, 0).Count + 1).ToString();
                items[goods.ID].transform.Find("ItemCount").gameObject.SetActive(true);
            }
            if (!ReadJson.JudgeHasGoods(goods.ID))
            {
                Debug.Log("正在生成：" + goods.Name);
                //添加一个到全部
                GameObject insGood3 = Instantiate(Resources.Load<GameObject>("Item/GoodItem"));
                insGood3.transform.SetParent(scroll[3].transform.Find("Viewport/BagContent").transform, false);
                insGood3.transform.Find("GoodItem").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + goods.IconName);
                Goods.splintersGoods.Add(goods);
                Goods.goods.Add(goods);
                //将物体添加到集合中
                items.Add(goods.ID, insGood3);
                BagDataManager.AddItem(goods.ID, insGood3);
                BagDataManager.AddID(insGood3, goods.ID);

                //添加到材料
                BagDataManager.AddSplinters(goods.ID, insGood3);
            }
        }
        Goods.goods.Add(goods);
    }

    /// <summary>
    /// Toggle值变化
    /// </summary>
    /// <param name="toggle">传入Toggle</param>
    private void ItemTypeToggle(Toggle toggle)
    {
        //当值变化的时候
        if (toggle.isOn)
        {
            //判断是那个变化
            if (toggle.name == "AllToggle")
            {
                //显示面板
                transform.Find("ItemView/AllGoodsScrollView").gameObject.SetActive(true);
                if (Goods.UpdateCount == 0)
                {
                    //加载所有的数据
                    for (int i = 0; i < Goods.goods.Count; i++)
                    {
                        GameObject insGood = Instantiate(Resources.Load<GameObject>("Item/GoodItem"));
                        insGood.transform.SetParent(GameObject.Find("BagContent").transform, false);
                        insGood.transform.Find("GoodItem").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + Goods.goods[i].IconName);

                        BagDataManager.AddItem(Goods.goods[i].ID, insGood);
                        BagDataManager.AddID(insGood, Goods.goods[i].ID);
                    }

                    //将更新后数据赋值给当前数量
                    Goods.UpdateCount = Goods.CurrentCount;
                    return;
                }

                //再次读取List里面的数量
                Goods.UpdateCount = Goods.goods.Count;

                Debug.Log("AllToggle:" + Goods.UpdateCount);
                Debug.Log("AllToggle:" + Goods.CurrentCount);
                /* //当添加数据时候
                 if (Goods.UpdateCount >= Goods.CurrentCount)
                 {
                     //加载新的数据
                     for (int i = 0; i < Goods.UpdateCount - Goods.CurrentCount; i++)
                     {
                         Debug.Log("添加数据测试！！！");
                         GameObject insGood = Instantiate(Resources.Load<GameObject>("Item/GoodItem"));
                         insGood.transform.SetParent(GameObject.Find("BagContent").transform, false);
                         insGood.transform.Find("GoodItem").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + Goods.goods[Goods.CurrentCount + i].IconName);
                     }
                     //将更新后数据赋值给当前数量
                     Goods.CurrentCount = Goods.UpdateCount;
                     return;
                 }
    */
                //当没有新的添加或者减少的时候不做处理
                if (Goods.UpdateCount == Goods.CurrentCount) { return; }
            }
            if (toggle.name == "EquipToggle")
            {
                transform.Find("ItemView/EquipGoodsScrollView").gameObject.SetActive(true);

                /* if (Goods.EquipUpdateCount == 0)
                 {
                     //加载所有的数据
                     */
                /*for (int i = 0; i < ReadJson.GetJsonValue(0, 1).Count; i++)*//*
                     for (int i = 0; i < 4; i++)
                     {
                         GameObject insGood = Instantiate(Resources.Load<GameObject>("Item/GoodItem"));
                         insGood.transform.SetParent(GameObject.Find("BagContent").transform, false);
                         insGood.transform.Find("GoodItem").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + ReadJson.GetJsonValue(0, 1)[i].IconName);
                     }
                     //将更新后数据赋值给当前数量
                     Goods.EquipUpdateCount = Goods.EquipCount;
                     return;
                 }*/

                //再次读取List里面的数量
                Goods.EquipUpdateCount = ReadJson.GetJsonValue(0, 1).Count;
                Debug.Log("EquipUpdateCount:" + Goods.EquipUpdateCount);
                Debug.Log("EquipUpdateCount:" + Goods.EquipCount);

                /* //当添加数据时候
                 if (Goods.EquipUpdateCount >= Goods.EquipCount)
                 {
                     //加载新的数据
                     for (int i = 0; i < Goods.EquipUpdateCount - Goods.EquipCount; i++)
                     {
                         Debug.Log("添加数据测试！！！");

                         GameObject insGood = Instantiate(Resources.Load<GameObject>("Item/GoodItem"));
                         insGood.transform.SetParent(GameObject.Find("BagContent").transform, false);
                         insGood.transform.Find("GoodItem").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + ReadJson.GetJsonValue(0, 1)[Goods.EquipCount + i].IconName);
                     }
                     //将更新后数据赋值给当前数量
                     Goods.EquipCount = Goods.EquipUpdateCount;
                     return;
                 }*/
                //当没有新的添加或者减少的时候不做处理
                if (Goods.EquipUpdateCount == Goods.EquipCount) { return; }
            }
            if (toggle.name == "MaterialToggle")
            {
                transform.Find("ItemView/MaterialGoodsScrollView").gameObject.SetActive(true);

                /* if (Goods.MaterialUpdateCount == 0)
                 {
                     //加载所有的数据
                     */
                /* for (int i = 0; i < ReadJson.GetJsonValue(0, 2).Count; i++)*//*
                     for (int i = 0; i < 4; i++)
                     {
                         GameObject insGood = Instantiate(Resources.Load<GameObject>("Item/GoodItem"));
                         insGood.transform.SetParent(GameObject.Find("BagContent").transform, false);
                         insGood.transform.Find("GoodItem").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + ReadJson.GetJsonValue(0, 2)[i].IconName);
                     }
                     //将更新后数据赋值给当前数量
                     Goods.MaterialUpdateCount = Goods.MaterialCount;
                     return;
                 }*/

                //再次读取List里面的数量
                Goods.MaterialUpdateCount = ReadJson.GetJsonValue(0, 2).Count;
                Debug.Log("MaterialToggle:" + Goods.MaterialUpdateCount);
                Debug.Log("MaterialToggle:" + Goods.MaterialCount);

                /* //当添加数据时候
                 if (Goods.MaterialUpdateCount >= Goods.MaterialCount)
                 {
                     //加载新的数据
                     for (int i = 0; i < Goods.MaterialUpdateCount - Goods.MaterialCount; i++)
                     {
                         GameObject insGood = Instantiate(Resources.Load<GameObject>("Item/GoodItem"));
                         insGood.transform.SetParent(GameObject.Find("BagContent").transform, false);
                         insGood.transform.Find("GoodItem").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + ReadJson.GetJsonValue(0, 2)[Goods.MaterialCount + i].IconName);
                     }

                     //将更新后数据赋值给当前数量
                     Goods.MaterialCount = Goods.MaterialUpdateCount;
                     return;
                 }*/
                //当没有新的添加或者减少的时候不做处理
                if (Goods.MaterialUpdateCount == Goods.MaterialCount) { return; }
            }
            if (toggle.name == "SplintersToggle")
            {
                transform.Find("ItemView/SplintersGoodsScrollView").gameObject.SetActive(true);

                /* if (Goods.SplintersUpdateCount == 0)
                 {
                     //加载所有的数据
                     */
                /*for (int i = 0; i < ReadJson.GetJsonValue(0, 3).Count; i++)*//*
                     for (int i = 0; i < 3; i++)
                     {
                         GameObject insGood = Instantiate(Resources.Load<GameObject>("Item/GoodItem"));
                         insGood.transform.SetParent(GameObject.Find("BagContent").transform, false);
                         insGood.transform.Find("GoodItem").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + ReadJson.GetJsonValue(0, 3)[i].IconName);
                     }
                     //将更新后数据赋值给当前数量
                     Goods.SplintersUpdateCount = Goods.SplintersCount;
                     return;
                 }*/

                Goods.SplintersUpdateCount = ReadJson.GetJsonValue(0, 3).Count;

                Debug.Log("SplintersToggle:" + Goods.SplintersUpdateCount);
                Debug.Log("SplintersToggle:" + Goods.SplintersCount);

                /*  //当添加数据时候
                  if (Goods.SplintersUpdateCount >= Goods.SplintersCount)
                  {
                      //加载新的数据
                      for (int i = 0; i < Goods.SplintersUpdateCount - Goods.SplintersCount; i++)
                      {
                          Debug.Log("添加数据测试！！！");

                          GameObject insGood = Instantiate(Resources.Load<GameObject>("Item/GoodItem"));
                          insGood.transform.SetParent(GameObject.Find("BagContent").transform, false);
                          insGood.transform.Find("GoodItem").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + ReadJson.GetJsonValue(0, 3)[Goods.SplintersCount + i].IconName);
                      }

                      //将更新后数据赋值给当前数量
                      Goods.SplintersCount = Goods.SplintersUpdateCount;
                      return;
                  }*/

                //当没有新的添加或者减少的时候不做处理
                if (Goods.SplintersUpdateCount == Goods.SplintersCount) { return; }
            }
        }
        else
        {
            //判断是那个变化
            if (toggle.name == "AllToggle")
            {
                /*处理对应数据动作*/
                transform.Find("ItemView/AllGoodsScrollView").gameObject.SetActive(false);
            }
            if (toggle.name == "EquipToggle")
            {
                /*处理对应数据动作*/
                transform.Find("ItemView/EquipGoodsScrollView").gameObject.SetActive(false);
            }
            if (toggle.name == "MaterialToggle")
            {
                /*处理对应数据动作*/
                transform.Find("ItemView/MaterialGoodsScrollView").gameObject.SetActive(false);
            }
            if (toggle.name == "SplintersToggle")
            {
                /*处理对应数据动作*/
                transform.Find("ItemView/SplintersGoodsScrollView").gameObject.SetActive(false);
            }
        }
    }
    void InitItemData()
    {
        if (Goods.EquipUpdateCount == 0)
        {
            //加载所有的数据
            /*for (int i = 0; i < ReadJson.GetJsonValue(0, 1).Count; i++)*/
            Goods.equipmentGoods = ReadJson.GetJsonValue(0, 1);
            /*Debug.Log(ReadJson.GetJsonValue(0, 1).Count);*/
            for (int i = 0; i < 4; i++)
            {
                GameObject insGood = Instantiate(Resources.Load<GameObject>("Item/GoodItem"));
                insGood.transform.SetParent(scroll[1].transform.Find("Viewport/BagContent").transform, false);
                insGood.transform.Find("GoodItem").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + ReadJson.GetJsonValue(0, 1)[i].IconName);

                //将物体添加到集合中
                items.Add(ReadJson.GetJsonValue(0, 1)[i].ID, insGood);
                BagDataManager.AddItem(ReadJson.GetJsonValue(0, 1)[i].ID, insGood);
                //添加到单独
                BagDataManager.AddEquie(ReadJson.GetJsonValue(0, 1)[i].ID, insGood);
                BagDataManager.AddID(insGood, ReadJson.GetJsonValue(0, 1)[i].ID);
            }
            //将更新后数据赋值给当前数量
            Goods.EquipUpdateCount = Goods.EquipCount;
        }
        if (Goods.MaterialUpdateCount == 0)
        {
            //加载所有的数据
            /* for (int i = 0; i < ReadJson.GetJsonValue(0, 2).Count; i++)*/
            Goods.materialGoods = ReadJson.GetJsonValue(0, 2);

            for (int i = 0; i < 4; i++)
            {
                GameObject insGood = Instantiate(Resources.Load<GameObject>("Item/GoodItem"));
                insGood.transform.SetParent(scroll[2].transform.Find("Viewport/BagContent").transform, false);
                insGood.transform.Find("GoodItem").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + ReadJson.GetJsonValue(0, 2)[i].IconName);
                //将物体添加到集合中
                items.Add(ReadJson.GetJsonValue(0, 2)[i].ID, insGood);
                BagDataManager.AddItem(ReadJson.GetJsonValue(0, 2)[i].ID, insGood);
                //添加到材料
                BagDataManager.AddMaterial(ReadJson.GetJsonValue(0, 2)[i].ID, insGood);
                BagDataManager.AddID(insGood, ReadJson.GetJsonValue(0, 2)[i].ID);
            }
            //将更新后数据赋值给当前数量
            Goods.MaterialUpdateCount = Goods.MaterialCount;
        }
        if (Goods.SplintersUpdateCount == 0)
        {
            //加载所有的数据
            /*for (int i = 0; i < ReadJson.GetJsonValue(0, 3).Count; i++)*/
            Goods.splintersGoods = ReadJson.GetJsonValue(0, 3);
            for (int i = 0; i < 3; i++)
            {
                GameObject insGood = Instantiate(Resources.Load<GameObject>("Item/GoodItem"));
                insGood.transform.SetParent(scroll[3].transform.Find("Viewport/BagContent").transform, false);
                insGood.transform.Find("GoodItem").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + ReadJson.GetJsonValue(0, 3)[i].IconName);
                //将物体添加到集合中
                items.Add(ReadJson.GetJsonValue(0, 3)[i].ID, insGood);

                //添加到全部
                BagDataManager.AddItem(ReadJson.GetJsonValue(0, 3)[i].ID, insGood);
                //添加到材料
                BagDataManager.AddSplinters(ReadJson.GetJsonValue(0, 3)[i].ID, insGood);
                BagDataManager.AddID(insGood, ReadJson.GetJsonValue(0, 3)[i].ID);
            }
            //将更新后数据赋值给当前数量
            Goods.SplintersUpdateCount = Goods.SplintersCount;
        }
    }

    /// <summary>
    /// 获取鼠标停留处UI
    /// </summary>
    /// <param name="canvas"></param>
    /// <returns></returns>
    public GameObject GetOverUI(GameObject canvas)
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        GraphicRaycaster gr = canvas.GetComponent<GraphicRaycaster>();
        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(pointerEventData, results);
        if (results.Count != 0)
        {
            return results[0].gameObject;
        }

        return null;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("离开UI");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter.name == "GoodItem(Clone)")
        {
            Debug.Log(eventData.pointerEnter.name);
        }
        Debug.Log(eventData.pointerEnter.name);
        Debug.Log("进入UI");
    }
    /* public void OnMouseOver()
     {
         Debug.Log("悬停UI");
     }*/
}
