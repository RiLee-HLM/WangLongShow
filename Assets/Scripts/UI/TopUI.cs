using System.Collections;
using System.Collections.Generic;
using UICore;
using UnityEngine;
using UnityEngine.UI;

public class TopUI : BaseUI
{
    Text text_coin;
    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();
        text_coin = GameTool.GetTheChildComponent<Text>(gameObject, "Coin");
        text_coin.text = UserData.Coin.ToString();
    }

    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        this.uiId = E_UiId.TopUI;
        this.uiType.showMode = E_ShowUIMode.DoNothing;
        this.uiType.uiRootType = E_UIRootType.KeepAbove;

        MessageCenter.AddMessageListener(E_MessageType.ItemBeSell, (object obj) =>
        {
            object[] data = obj as object[];
            Debug.Log(" ’µΩ" + data[0]);
            text_coin.text = data[0].ToString();
        });
    }
}
