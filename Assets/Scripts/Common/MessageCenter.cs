using UnityEngine;
using System.Collections;
using UICore;
using System.Collections.Generic;

public class MessageCenter : MonoBehaviour
{
    public delegate void CallBack(object obj);
    //一个用来存放所有监听的字典<消息类型，监听到消息后所要处理的逻辑>
    public static Dictionary<E_MessageType, CallBack> dicMessageType = new Dictionary<E_MessageType, CallBack>();
    //添加监听
    public static void AddMessageListener(E_MessageType messageType, CallBack callBack)
    {
        if (!dicMessageType.ContainsKey(messageType))
        {
            dicMessageType.Add(messageType,null);
        }
        dicMessageType[messageType] += callBack;
    }
    //取消监听
    public static void RemoveListener(E_MessageType messageType, CallBack callBack)
    {
        if (dicMessageType.ContainsKey(messageType))
        {
            dicMessageType[messageType] -= callBack;
        }
    }
    //取消所有的监听
    public static void RemoveAllMessage()
    {
        dicMessageType.Clear();
    }
    //广播消息（触发消息）
    public static void SendMessage(E_MessageType messageType,object obj=null)
    {
        CallBack callBack;
        if (dicMessageType.TryGetValue(messageType,out callBack))
        {
            if (callBack!=null)
            {
                callBack(obj);
            }
        }
    }
}
