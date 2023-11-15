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

    //�������ɹ���Item
    public static Dictionary<int, GameObject> items;
    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();

        //���Tggle����
        toggles = transform.Find("ToggleGroup").GetComponentsInChildren<Toggle>();
        foreach (Toggle toggle in toggles)
        {
            toggle.onValueChanged.AddListener((bool isOn) => ItemTypeToggle(toggle));
            //Ĭ�ϴ򿪵���ȫ��װ��
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

        //��ʼ�����е�����
        ReadJson.ParseJson();
    }

    /// <summary>
    /// �����Ʒ
    /// </summary>
    private void AddItem()
    {
        Debug.Log("�����ť������");
        Good goods = Goods.allGoods[Random.Range(0, Goods.allGoods.Count)];

        //���һ����ȫ��
        GameObject insGood = Instantiate(Resources.Load<GameObject>("Item/GoodItem"));
        insGood.transform.SetParent(scroll[0].transform.Find("Viewport/BagContent").transform, false);
        insGood.transform.Find("GoodItem").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + goods.IconName);
        /* //��ӵ���ӵ�еļ���
         Goods.goods.Add(goods);*/
        //������ɹ�ȫ��������
        BagDataManager.AddItem(goods.ID, insGood);
        /*����������е�ID*/
        BagDataManager.AddID(insGood, goods.ID);

        if (goods.Type == 1)
        {
            //���һ����ȫ��
            GameObject insGood1 = Instantiate(Resources.Load<GameObject>("Item/GoodItem"));
            insGood1.transform.SetParent(scroll[1].transform.Find("Viewport/BagContent").transform, false);
            insGood1.transform.Find("GoodItem").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + goods.IconName);
            Goods.equipmentGoods.Add(goods);
            Goods.goods.Add(goods);
            BagDataManager.AddItem(goods.ID, insGood1);
            BagDataManager.AddID(insGood1, goods.ID);
            //��ӵ�����
            BagDataManager.AddEquie(goods.ID, insGood1);
        }
        if (goods.Type == 2)
        {
            Debug.Log("�ж�ֵ��" + ReadJson.JudgeHasGoods(goods.ID) + goods.ID);

            if (ReadJson.JudgeHasGoods(goods.ID))
            {
                Debug.Log("�Ѿ����ڣ���");
                //����ID���Ѿ��ж��ٸ�
                Debug.Log(ReadJson.GetJsonValue(goods.ID, 0).Count);
                /*Debug.Log(items[goods.ID].name);
                Debug.Log(items[goods.ID].transform.Find("ItemCount").name);*/
                items[goods.ID].transform.Find("ItemCount").GetComponent<Text>().text = (ReadJson.GetJsonValue(goods.ID, 0).Count + 1).ToString();
                items[goods.ID].transform.Find("ItemCount").gameObject.SetActive(true);
            }
            if (!ReadJson.JudgeHasGoods(goods.ID))
            {
                Debug.Log("�������ɣ�" + goods.Name);
                //���һ����ȫ��
                GameObject insGood2 = Instantiate(Resources.Load<GameObject>("Item/GoodItem"));
                insGood2.transform.SetParent(scroll[2].transform.Find("Viewport/BagContent").transform, false);
                insGood2.transform.Find("GoodItem").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + goods.IconName);
                Goods.materialGoods.Add(goods);
                Goods.goods.Add(goods);
                //��������ӵ�������
                items.Add(goods.ID, insGood2);

                BagDataManager.AddItem(goods.ID, insGood2);
                BagDataManager.AddID(insGood2, goods.ID);
                //��ӵ�����
                BagDataManager.AddMaterial(goods.ID, insGood2);
            }
        }
        if (goods.Type == 3)
        {
            Debug.Log("�ж�ֵ��" + ReadJson.JudgeHasGoods(goods.ID) + goods.ID);
            if (ReadJson.JudgeHasGoods(goods.ID))
            {
                Debug.Log("�Ѿ����ڣ���");
                /*ReadJson.GetJsonValue(goods.ID, 0).Count;*/
                items[goods.ID].transform.Find("ItemCount").GetComponent<Text>().text = (ReadJson.GetJsonValue(goods.ID, 0).Count + 1).ToString();
                items[goods.ID].transform.Find("ItemCount").gameObject.SetActive(true);
            }
            if (!ReadJson.JudgeHasGoods(goods.ID))
            {
                Debug.Log("�������ɣ�" + goods.Name);
                //���һ����ȫ��
                GameObject insGood3 = Instantiate(Resources.Load<GameObject>("Item/GoodItem"));
                insGood3.transform.SetParent(scroll[3].transform.Find("Viewport/BagContent").transform, false);
                insGood3.transform.Find("GoodItem").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + goods.IconName);
                Goods.splintersGoods.Add(goods);
                Goods.goods.Add(goods);
                //��������ӵ�������
                items.Add(goods.ID, insGood3);
                BagDataManager.AddItem(goods.ID, insGood3);
                BagDataManager.AddID(insGood3, goods.ID);

                //��ӵ�����
                BagDataManager.AddSplinters(goods.ID, insGood3);
            }
        }
        Goods.goods.Add(goods);
    }

    /// <summary>
    /// Toggleֵ�仯
    /// </summary>
    /// <param name="toggle">����Toggle</param>
    private void ItemTypeToggle(Toggle toggle)
    {
        //��ֵ�仯��ʱ��
        if (toggle.isOn)
        {
            //�ж����Ǹ��仯
            if (toggle.name == "AllToggle")
            {
                //��ʾ���
                transform.Find("ItemView/AllGoodsScrollView").gameObject.SetActive(true);
                if (Goods.UpdateCount == 0)
                {
                    //�������е�����
                    for (int i = 0; i < Goods.goods.Count; i++)
                    {
                        GameObject insGood = Instantiate(Resources.Load<GameObject>("Item/GoodItem"));
                        insGood.transform.SetParent(GameObject.Find("BagContent").transform, false);
                        insGood.transform.Find("GoodItem").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + Goods.goods[i].IconName);

                        BagDataManager.AddItem(Goods.goods[i].ID, insGood);
                        BagDataManager.AddID(insGood, Goods.goods[i].ID);
                    }

                    //�����º����ݸ�ֵ����ǰ����
                    Goods.UpdateCount = Goods.CurrentCount;
                    return;
                }

                //�ٴζ�ȡList���������
                Goods.UpdateCount = Goods.goods.Count;

                Debug.Log("AllToggle:" + Goods.UpdateCount);
                Debug.Log("AllToggle:" + Goods.CurrentCount);
                /* //���������ʱ��
                 if (Goods.UpdateCount >= Goods.CurrentCount)
                 {
                     //�����µ�����
                     for (int i = 0; i < Goods.UpdateCount - Goods.CurrentCount; i++)
                     {
                         Debug.Log("������ݲ��ԣ�����");
                         GameObject insGood = Instantiate(Resources.Load<GameObject>("Item/GoodItem"));
                         insGood.transform.SetParent(GameObject.Find("BagContent").transform, false);
                         insGood.transform.Find("GoodItem").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + Goods.goods[Goods.CurrentCount + i].IconName);
                     }
                     //�����º����ݸ�ֵ����ǰ����
                     Goods.CurrentCount = Goods.UpdateCount;
                     return;
                 }
    */
                //��û���µ���ӻ��߼��ٵ�ʱ��������
                if (Goods.UpdateCount == Goods.CurrentCount) { return; }
            }
            if (toggle.name == "EquipToggle")
            {
                transform.Find("ItemView/EquipGoodsScrollView").gameObject.SetActive(true);

                /* if (Goods.EquipUpdateCount == 0)
                 {
                     //�������е�����
                     */
                /*for (int i = 0; i < ReadJson.GetJsonValue(0, 1).Count; i++)*//*
                     for (int i = 0; i < 4; i++)
                     {
                         GameObject insGood = Instantiate(Resources.Load<GameObject>("Item/GoodItem"));
                         insGood.transform.SetParent(GameObject.Find("BagContent").transform, false);
                         insGood.transform.Find("GoodItem").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + ReadJson.GetJsonValue(0, 1)[i].IconName);
                     }
                     //�����º����ݸ�ֵ����ǰ����
                     Goods.EquipUpdateCount = Goods.EquipCount;
                     return;
                 }*/

                //�ٴζ�ȡList���������
                Goods.EquipUpdateCount = ReadJson.GetJsonValue(0, 1).Count;
                Debug.Log("EquipUpdateCount:" + Goods.EquipUpdateCount);
                Debug.Log("EquipUpdateCount:" + Goods.EquipCount);

                /* //���������ʱ��
                 if (Goods.EquipUpdateCount >= Goods.EquipCount)
                 {
                     //�����µ�����
                     for (int i = 0; i < Goods.EquipUpdateCount - Goods.EquipCount; i++)
                     {
                         Debug.Log("������ݲ��ԣ�����");

                         GameObject insGood = Instantiate(Resources.Load<GameObject>("Item/GoodItem"));
                         insGood.transform.SetParent(GameObject.Find("BagContent").transform, false);
                         insGood.transform.Find("GoodItem").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + ReadJson.GetJsonValue(0, 1)[Goods.EquipCount + i].IconName);
                     }
                     //�����º����ݸ�ֵ����ǰ����
                     Goods.EquipCount = Goods.EquipUpdateCount;
                     return;
                 }*/
                //��û���µ���ӻ��߼��ٵ�ʱ��������
                if (Goods.EquipUpdateCount == Goods.EquipCount) { return; }
            }
            if (toggle.name == "MaterialToggle")
            {
                transform.Find("ItemView/MaterialGoodsScrollView").gameObject.SetActive(true);

                /* if (Goods.MaterialUpdateCount == 0)
                 {
                     //�������е�����
                     */
                /* for (int i = 0; i < ReadJson.GetJsonValue(0, 2).Count; i++)*//*
                     for (int i = 0; i < 4; i++)
                     {
                         GameObject insGood = Instantiate(Resources.Load<GameObject>("Item/GoodItem"));
                         insGood.transform.SetParent(GameObject.Find("BagContent").transform, false);
                         insGood.transform.Find("GoodItem").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + ReadJson.GetJsonValue(0, 2)[i].IconName);
                     }
                     //�����º����ݸ�ֵ����ǰ����
                     Goods.MaterialUpdateCount = Goods.MaterialCount;
                     return;
                 }*/

                //�ٴζ�ȡList���������
                Goods.MaterialUpdateCount = ReadJson.GetJsonValue(0, 2).Count;
                Debug.Log("MaterialToggle:" + Goods.MaterialUpdateCount);
                Debug.Log("MaterialToggle:" + Goods.MaterialCount);

                /* //���������ʱ��
                 if (Goods.MaterialUpdateCount >= Goods.MaterialCount)
                 {
                     //�����µ�����
                     for (int i = 0; i < Goods.MaterialUpdateCount - Goods.MaterialCount; i++)
                     {
                         GameObject insGood = Instantiate(Resources.Load<GameObject>("Item/GoodItem"));
                         insGood.transform.SetParent(GameObject.Find("BagContent").transform, false);
                         insGood.transform.Find("GoodItem").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + ReadJson.GetJsonValue(0, 2)[Goods.MaterialCount + i].IconName);
                     }

                     //�����º����ݸ�ֵ����ǰ����
                     Goods.MaterialCount = Goods.MaterialUpdateCount;
                     return;
                 }*/
                //��û���µ���ӻ��߼��ٵ�ʱ��������
                if (Goods.MaterialUpdateCount == Goods.MaterialCount) { return; }
            }
            if (toggle.name == "SplintersToggle")
            {
                transform.Find("ItemView/SplintersGoodsScrollView").gameObject.SetActive(true);

                /* if (Goods.SplintersUpdateCount == 0)
                 {
                     //�������е�����
                     */
                /*for (int i = 0; i < ReadJson.GetJsonValue(0, 3).Count; i++)*//*
                     for (int i = 0; i < 3; i++)
                     {
                         GameObject insGood = Instantiate(Resources.Load<GameObject>("Item/GoodItem"));
                         insGood.transform.SetParent(GameObject.Find("BagContent").transform, false);
                         insGood.transform.Find("GoodItem").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + ReadJson.GetJsonValue(0, 3)[i].IconName);
                     }
                     //�����º����ݸ�ֵ����ǰ����
                     Goods.SplintersUpdateCount = Goods.SplintersCount;
                     return;
                 }*/

                Goods.SplintersUpdateCount = ReadJson.GetJsonValue(0, 3).Count;

                Debug.Log("SplintersToggle:" + Goods.SplintersUpdateCount);
                Debug.Log("SplintersToggle:" + Goods.SplintersCount);

                /*  //���������ʱ��
                  if (Goods.SplintersUpdateCount >= Goods.SplintersCount)
                  {
                      //�����µ�����
                      for (int i = 0; i < Goods.SplintersUpdateCount - Goods.SplintersCount; i++)
                      {
                          Debug.Log("������ݲ��ԣ�����");

                          GameObject insGood = Instantiate(Resources.Load<GameObject>("Item/GoodItem"));
                          insGood.transform.SetParent(GameObject.Find("BagContent").transform, false);
                          insGood.transform.Find("GoodItem").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + ReadJson.GetJsonValue(0, 3)[Goods.SplintersCount + i].IconName);
                      }

                      //�����º����ݸ�ֵ����ǰ����
                      Goods.SplintersCount = Goods.SplintersUpdateCount;
                      return;
                  }*/

                //��û���µ���ӻ��߼��ٵ�ʱ��������
                if (Goods.SplintersUpdateCount == Goods.SplintersCount) { return; }
            }
        }
        else
        {
            //�ж����Ǹ��仯
            if (toggle.name == "AllToggle")
            {
                /*�����Ӧ���ݶ���*/
                transform.Find("ItemView/AllGoodsScrollView").gameObject.SetActive(false);
            }
            if (toggle.name == "EquipToggle")
            {
                /*�����Ӧ���ݶ���*/
                transform.Find("ItemView/EquipGoodsScrollView").gameObject.SetActive(false);
            }
            if (toggle.name == "MaterialToggle")
            {
                /*�����Ӧ���ݶ���*/
                transform.Find("ItemView/MaterialGoodsScrollView").gameObject.SetActive(false);
            }
            if (toggle.name == "SplintersToggle")
            {
                /*�����Ӧ���ݶ���*/
                transform.Find("ItemView/SplintersGoodsScrollView").gameObject.SetActive(false);
            }
        }
    }
    void InitItemData()
    {
        if (Goods.EquipUpdateCount == 0)
        {
            //�������е�����
            /*for (int i = 0; i < ReadJson.GetJsonValue(0, 1).Count; i++)*/
            Goods.equipmentGoods = ReadJson.GetJsonValue(0, 1);
            /*Debug.Log(ReadJson.GetJsonValue(0, 1).Count);*/
            for (int i = 0; i < 4; i++)
            {
                GameObject insGood = Instantiate(Resources.Load<GameObject>("Item/GoodItem"));
                insGood.transform.SetParent(scroll[1].transform.Find("Viewport/BagContent").transform, false);
                insGood.transform.Find("GoodItem").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + ReadJson.GetJsonValue(0, 1)[i].IconName);

                //��������ӵ�������
                items.Add(ReadJson.GetJsonValue(0, 1)[i].ID, insGood);
                BagDataManager.AddItem(ReadJson.GetJsonValue(0, 1)[i].ID, insGood);
                //��ӵ�����
                BagDataManager.AddEquie(ReadJson.GetJsonValue(0, 1)[i].ID, insGood);
                BagDataManager.AddID(insGood, ReadJson.GetJsonValue(0, 1)[i].ID);
            }
            //�����º����ݸ�ֵ����ǰ����
            Goods.EquipUpdateCount = Goods.EquipCount;
        }
        if (Goods.MaterialUpdateCount == 0)
        {
            //�������е�����
            /* for (int i = 0; i < ReadJson.GetJsonValue(0, 2).Count; i++)*/
            Goods.materialGoods = ReadJson.GetJsonValue(0, 2);

            for (int i = 0; i < 4; i++)
            {
                GameObject insGood = Instantiate(Resources.Load<GameObject>("Item/GoodItem"));
                insGood.transform.SetParent(scroll[2].transform.Find("Viewport/BagContent").transform, false);
                insGood.transform.Find("GoodItem").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + ReadJson.GetJsonValue(0, 2)[i].IconName);
                //��������ӵ�������
                items.Add(ReadJson.GetJsonValue(0, 2)[i].ID, insGood);
                BagDataManager.AddItem(ReadJson.GetJsonValue(0, 2)[i].ID, insGood);
                //��ӵ�����
                BagDataManager.AddMaterial(ReadJson.GetJsonValue(0, 2)[i].ID, insGood);
                BagDataManager.AddID(insGood, ReadJson.GetJsonValue(0, 2)[i].ID);
            }
            //�����º����ݸ�ֵ����ǰ����
            Goods.MaterialUpdateCount = Goods.MaterialCount;
        }
        if (Goods.SplintersUpdateCount == 0)
        {
            //�������е�����
            /*for (int i = 0; i < ReadJson.GetJsonValue(0, 3).Count; i++)*/
            Goods.splintersGoods = ReadJson.GetJsonValue(0, 3);
            for (int i = 0; i < 3; i++)
            {
                GameObject insGood = Instantiate(Resources.Load<GameObject>("Item/GoodItem"));
                insGood.transform.SetParent(scroll[3].transform.Find("Viewport/BagContent").transform, false);
                insGood.transform.Find("GoodItem").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + ReadJson.GetJsonValue(0, 3)[i].IconName);
                //��������ӵ�������
                items.Add(ReadJson.GetJsonValue(0, 3)[i].ID, insGood);

                //��ӵ�ȫ��
                BagDataManager.AddItem(ReadJson.GetJsonValue(0, 3)[i].ID, insGood);
                //��ӵ�����
                BagDataManager.AddSplinters(ReadJson.GetJsonValue(0, 3)[i].ID, insGood);
                BagDataManager.AddID(insGood, ReadJson.GetJsonValue(0, 3)[i].ID);
            }
            //�����º����ݸ�ֵ����ǰ����
            Goods.SplintersUpdateCount = Goods.SplintersCount;
        }
    }

    /// <summary>
    /// ��ȡ���ͣ����UI
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
        Debug.Log("�뿪UI");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter.name == "GoodItem(Clone)")
        {
            Debug.Log(eventData.pointerEnter.name);
        }
        Debug.Log(eventData.pointerEnter.name);
        Debug.Log("����UI");
    }
    /* public void OnMouseOver()
     {
         Debug.Log("��ͣUI");
     }*/
}
