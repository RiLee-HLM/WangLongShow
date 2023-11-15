using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace UICore
{
    public class UIManager : UnitySingleton<UIManager>
    {

        //缓存所有打开过的窗体
        private Dictionary<E_UiId, BaseUI> dicAllUI;
        //缓存正在显示的窗体
        private Dictionary<E_UiId, BaseUI> dicShowUI;

        //缓存最近显示出来的窗体
        private BaseUI currentUI = null;
        //缓存上一个窗体
        // private BaseUI beforeUI = null;
        private E_UiId beforeUiId = E_UiId.NullUI;

        //缓存画布
        private Transform canvas;
        //缓存保持在最前方的窗体的父节点
        private Transform keepAboveUIRoot;
        //缓存普通窗体的父节点
        private Transform normalUIRoot;

        private void Awake()
        {
            //Test.Instance.Show();
            dicAllUI = new Dictionary<E_UiId, BaseUI>();
            dicShowUI = new Dictionary<E_UiId, BaseUI>();
            InitUIManager();
        }
        //初始化UI管理类
        private void InitUIManager()
        {
            canvas = this.transform.parent;
            //设置画布在场景切换的时候不被销毁，因为整个游戏共用唯一的一个画布
            DontDestroyOnLoad(canvas);
            if (keepAboveUIRoot == null)
            {
                keepAboveUIRoot = GameTool.FindTheChild(canvas.gameObject, "KeepAboveUIRoot");
            }
            if (normalUIRoot == null)
            {
                normalUIRoot = GameTool.FindTheChild(canvas.gameObject, "NormalUIRoot");
            }

            ShowUI(E_UiId.TopUI);
            ShowUI(E_UiId.BagUI);
        }
        //销毁窗体
        private void DestroyUI(E_UiId uiId)
        {
            if (dicAllUI.ContainsKey(uiId))
            {
                //存在该窗体，去销毁
                Destroy(dicAllUI[uiId].gameObject);
                dicAllUI.Remove(uiId);
                //  Debug.Log("销毁窗体");
            }
        }
        //供外界调用的，显示窗体的方法
        public BaseUI ShowUI(E_UiId uiId, bool isSaveBeforeUiId = true)
        {
            if (uiId == E_UiId.NullUI)
            {
                uiId = E_UiId.BagUI;
            }
            BaseUI baseUI = JudgeShowUI(uiId);
            if (baseUI != null)
            {
                baseUI.ShowUI();
            }
            if (isSaveBeforeUiId)
            {
                baseUI.BeforeUiId = beforeUiId;
            }
            return baseUI;
        }
        //供外界调用，反向切换窗体的方法
        public void ReturnBeforeUI(E_UiId uiId)
        {
            ShowUI(uiId, false);
        }
        //供外界调用的，隐藏单个窗体的方法
        public void HideSingleUI(E_UiId uiId, Del_AfterHideUI del = null)
        {
            if (!dicShowUI.ContainsKey(uiId))
            {
                return;
            }
            dicShowUI[uiId].HideUI(del);
            dicShowUI.Remove(uiId);
        }
        public GameObject GetUIPanel(E_UiId uiId)
        {
            if (dicAllUI.ContainsKey(uiId))
            {
                return dicAllUI[uiId].gameObject;
            }
            else
            {
                return null;
            }
        }
        private BaseUI JudgeShowUI(E_UiId uiId)
        {
            //判断将要显示的窗体是否已经正在显示了
            if (dicShowUI.ContainsKey(uiId))
            {
                //如果已经正在显示了，就不需要处理其他逻辑了
                return null;
            }
            //判断窗体是否有加载过
            BaseUI baseUI = GetBaseUI(uiId);
            if (baseUI == null)
            {
                //说明这个窗体没显示过（没有加载过）,要去动态加载
                string path = GameDefine.dicPath[uiId];
                GameObject theUI = Resources.Load<GameObject>(path);
                if (theUI != null)
                {
                    //把该窗体生成出来
                    GameObject willShowUI = Instantiate(theUI);
                    //窗体生成出来后，要确保有挂对应的UI脚本
                    baseUI = willShowUI.GetComponent<BaseUI>();
                    if (baseUI == null)
                    {
                        //说明生成出来的这个窗体上面没有挂载对应的UI脚本
                        //那么就需要给这个窗体自动添加对应的脚本
                        Type type = GameDefine.GetUIScriptType(uiId);
                        baseUI = willShowUI.AddComponent(type) as BaseUI;
                    }
                    //判断这个窗体是属于哪个父节点的
                    Transform uiRoot = GetTheUIRoot(baseUI);
                    GameTool.AddChildToParent(uiRoot, willShowUI.transform);
                    willShowUI.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
                    //这个窗体是第一次加载显示出来的，那么就需要缓存起来
                    dicAllUI.Add(uiId, baseUI);
                }
                else
                {
                    Debug.LogError("指定路径下面找不到对应的预制体");
                }
            }
            UpdateDicShowUIAndHideUI(baseUI);
            return baseUI;
        }
        //更新缓存正在显示的窗体的字典并且隐藏对应的窗体
        private void UpdateDicShowUIAndHideUI(BaseUI baseUI)
        {
            //判断是否需要隐藏其他窗体
            if (baseUI.IsHideOtherUI())
            {
                //如果返回值为true, E_ShowUIMode.HideOther与 E_ShowUIMode.HideAll
                //需要隐藏其他窗体
                if (dicShowUI.Count > 0)
                {
                    //有窗体正在显示，就要隐藏对应的窗体
                    if (baseUI.uiType.showMode == E_ShowUIMode.HideOther)
                    {
                        HideAllUI(false, baseUI);
                    }
                    else//HideAll
                    {
                        HideAllUI(true, baseUI);
                    }

                }
            }
            //更新缓存正在显示的窗体的字典
            dicShowUI.Add(baseUI.GetUiId, baseUI);
        }
        //隐藏所有窗体的方法
        public void HideAllUI(bool isHideAboveUI, BaseUI baseUI)
        {
            if (isHideAboveUI)
            {
                //1、隐藏所有的窗体，不管是普通窗体还是保持在最前方的窗体，都需要全部隐藏
                foreach (KeyValuePair<E_UiId, BaseUI> uiItem in dicShowUI)
                {
                    uiItem.Value.HideUI();
                }
                dicShowUI.Clear();
                //Debug.Log("清空dicShowUI");
            }
            else
            {
                //2、隐藏所有的窗体，但是不包含保持在最前方的窗体
                //缓存所有被隐藏的窗体
                List<E_UiId> list = new List<E_UiId>();
                foreach (KeyValuePair<E_UiId, BaseUI> uiItem in dicShowUI)
                {
                    //如果不是保持在最前方的窗体
                    if (uiItem.Value.uiType.uiRootType != E_UIRootType.KeepAbove)
                    {
                        uiItem.Value.HideUI();
                        //存储上一个窗体的ID
                        beforeUiId = uiItem.Key;
                        // baseUI.BeforeUiId= uiItem.Key;
                        list.Add(uiItem.Key);
                    }
                }
                for (int i = 0; i < list.Count; i++)
                {
                    dicShowUI.Remove(list[i]);
                }
            }
        }
        //判断窗体的父物体
        private Transform GetTheUIRoot(BaseUI baseUI)
        {
            if (baseUI.uiType.uiRootType == E_UIRootType.KeepAbove)
            {
                return keepAboveUIRoot;
            }
            else
            {
                return normalUIRoot;
            }
        }
        private BaseUI GetBaseUI(E_UiId UiId)
        {
            if (dicAllUI.ContainsKey(UiId))
            {
                return dicAllUI[UiId];
            }
            else
            {
                return null;
            }
        }
        void Update()
        {
            AutoDestroyUI();
        }
        //自动销毁窗体
        private void AutoDestroyUI()
        {
            if (dicAllUI.Count == 0)
            {
                return;
            }
            List<E_UiId> list = new List<E_UiId>();
            //foreach只读，遍历dicAllUI过程中，不能对dicAllUI进行移除或者增加
            foreach (KeyValuePair<E_UiId, BaseUI> UIItem in dicAllUI)
            {
                if (UIItem.Value.uiType.destroyType == E_DestroyType.NoDestroy)
                {
                    continue;
                }
                //延时销毁
                else if (UIItem.Value.uiType.destroyType == E_DestroyType.Delay)
                {
                    if (UIItem.Value.destroyTimer.Elapsed.Seconds == UIItem.Value.delayTime)
                    {
                        list.Add(UIItem.Value.GetUiId);
                    }
                }
                //立刻销毁
                else if (UIItem.Value.uiType.destroyType == E_DestroyType.ImmidiatelyDestroy && !dicShowUI.ContainsKey(UIItem.Value.GetUiId))
                {
                    list.Add(UIItem.Value.GetUiId);
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                DestroyUI(list[i]);
            }
        }
    }

}
