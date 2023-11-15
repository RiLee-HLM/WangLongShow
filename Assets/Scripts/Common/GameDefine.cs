using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace UICore
{

    //窗体的ID
    public enum E_UiId
    {
        NullUI,
        BagUI,
        TopUI,
        ItemUI,
    }
    //窗体的层级类型（父节点的类型）
    public enum E_UIRootType
    {
        KeepAbove,//保持在最前方的窗体
        Normal//普通窗体
    }
    //窗体的显示方式
    public enum E_ShowUIMode
    {
        //窗体显示出来的时候，不会去隐藏任何窗体
        DoNothing,
        //窗体显示出来的时候,会隐藏掉所有的普通窗体，但是不会隐藏保持在最前方的窗体
        HideOther,
        //窗体显示出来的时候,会隐藏所有的窗体，不管是普通的还是保持在最前方的
        HideAll
    }
    //窗体的销毁类型
    public enum E_DestroyType
    {
        NoDestroy,
        ImmidiatelyDestroy,
        Delay
    }
    //消息类型
    public enum E_MessageType
    {
        ItemBeChoose,
        ItemBeSell,
        AddCoin,
    }
    //物品类型
    public enum E_GoodsType
    {
        Default,//全部类型
        Equipment,//装备
        Potions,//药水
        Rune,//符文
        Material//材料
    }
    public class GameDefine
    {
        //标记是否生成了画布
        public static bool hasCanvas = false;
        public static Dictionary<E_UiId, string> dicPath = new Dictionary<E_UiId, string>()
         {

             { E_UiId.BagUI,"UIPrefab/"+"BagUI"},
             { E_UiId.TopUI,"UIPrefab/"+"TopUI"},
             { E_UiId.ItemUI,"UIPrefab/"+"ItemUI"},

         };
        public static Type GetUIScriptType(E_UiId uiId)
        {
            Type scriptType = null;
            switch (uiId)
            {
                case E_UiId.BagUI:
                    scriptType = typeof(BagUI);
                    break;
                case E_UiId.TopUI:
                    scriptType = typeof(TopUI);
                    break;
                case E_UiId.ItemUI:
                    scriptType = typeof(ItemUI);
                    break;
            }
            return scriptType;
        }
    }

}
