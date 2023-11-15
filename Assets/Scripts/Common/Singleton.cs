using UnityEngine;
using System.Collections;
//单例模式（多个类共用一个实例）
//1、不继承于MonoBehaviour
//2、继承于MonoBehaviour
//区别：继承于MonoBehaviour的可以使用脚本生命周期函数

//不继承于MonoBehaviour的单例模式
public class Singleton<T> where T : new()
{
    protected static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }
    protected Singleton()
    {

    }
  
}
//继承于MonoBehaviour的单例模式,可以使用脚本生命周期函数
public class UnitySingleton<T> : MonoBehaviour where T : Component
{
    private static GameObject unitySingletonObj;
    protected static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                if (unitySingletonObj == null)
                {
                    //unitySingletonObj = GameObject.Find("Canvas/UnitySingletonObj");
                    unitySingletonObj = GameObject.FindGameObjectWithTag("UnitySingletonObj");
                }
                instance = unitySingletonObj.GetComponent<T>();
            }
            return instance;
        }
    }
    protected UnitySingleton()
    {
    }
}

