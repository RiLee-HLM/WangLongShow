// ========================================================
// 作者：RiLee 
// 创建时间：2023-10-24 22:04:08
// ========================================================
using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

//��Ϸ�����࣬��һЩ�ᱻ��羭���õ��ķ����������������
//Ϊ�˷��������ã�ͨ��������ķ�������Ϊ��̬��������̬����������ͨ������ֱ�ӵ��÷�����
public class GameTool : MonoBehaviour
{
    //�����ڴ�ķ�����һ�����л�������ʱ����ã�
    public static void ClearMemory()
    {
        //�������գ�����ȥƵ���ĵ��ã�Ӧ�����ʵ�������²�ȥ����
        //��Ϊ�������ջ����ĺܴ�����ܣ�Ƶ�����ûᵼ�¿���
        GC.Collect();
        //ж���ڴ���û�õ���Դ
        Resources.UnloadUnusedAssets();
    }
    //�����ڴ棬���ݳ־û���PlayerPrefs��
    //�ж�ϵͳ�ڴ������Ƿ���ĳ����
    public static bool HasKey(string key)
    {
        return PlayerPrefs.HasKey(key);
    }
    //ȥ�ڴ�������ݼ���ȡֵ
    public static int GetInt(string key)
    {
        return PlayerPrefs.GetInt(key);
    }
    public static float GetFloat(string key)
    {
        return PlayerPrefs.GetFloat(key);
    }
    public static string GetString(string key)
    {
        return PlayerPrefs.GetString(key);
    }
    //��ϵͳ�ڴ�����ȥ��ֵ
    public static void SetInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }
    public static void SetFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }
    public static void SetString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }
    //���ڴ�����ɾ��ָ�������ݣ���ֵ�ԣ�
    public static void DeleteKey(string key)
    {
        PlayerPrefs.DeleteKey(key);
    }
    //ɾ���ڴ��������е����ݣ���ֵ�ԣ�
    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
    //����������
    public static Transform FindTheChild(GameObject goParnent, string chilidName)
    {
        //Debug.Log("�����������"+goParnent.name);
        Transform searchTrans = goParnent.transform.Find(chilidName);
        if (searchTrans == null)
        {
            //����goParnent�����е�һ��������
            foreach (Transform trans in goParnent.transform)
            {
                //�ݹ����
                searchTrans = FindTheChild(trans.gameObject, chilidName);
                if (searchTrans != null)
                {
                    return searchTrans;
                }
            }
        }
        return searchTrans;
    }
    //��ȡ��������������
    public static T GetTheChildComponent<T>(GameObject goParnent, string chilidName) where T : Component
    {
        Transform searchTrans = FindTheChild(goParnent, chilidName);
        if (searchTrans != null)
        {
            return searchTrans.GetComponent<T>();
        }
        else
        {
            return null;
            //return default(T);
        }

    }
    //��������������
    public static T AddTheChildComponent<T>(GameObject goParnent, string chilidName) where T : Component
    {
        Transform searchTrans = FindTheChild(goParnent, chilidName);
        if (searchTrans != null)
        {
            T[] arr = searchTrans.GetComponents<T>();
            for (int i = 0; i < arr.Length; i++)
            {
                //DestroyҲ�����٣����ǲ����������٣���ǰ֡����ǰ����
                // Destroy(arr[i]);
                //��������
                DestroyImmediate(arr[i], true);
            }
            return searchTrans.gameObject.AddComponent<T>();
        }
        else
        {
            return null;
        }
    }
    //���������
    public static void AddChildToParent(Transform parentTrans, Transform childTrans)
    {
        childTrans.SetParent(parentTrans);
        childTrans.localPosition = Vector3.zero;
        childTrans.localScale = Vector3.one;
    }
    //��¼��Ϸ�Ƿ��г�ʼ��������
    // public static bool isInitData = false;

}
