using System.Collections.Generic;
using UnityEngine;

public class BagDataManager
{
    //储存生成过的Item
    public static Dictionary<int, GameObject> items;
    public static Dictionary<GameObject, int> ids;
    public static Dictionary<int, GameObject> equies;
    public static Dictionary<int, GameObject> materials;
    public static Dictionary<int, GameObject> Splinters;
    /*  private void Awake()
      {
          Debug.Log("datayunx");

          items = new Dictionary<int, GameObject>();
          ids = new Dictionary<GameObject, int>();
      }*/
    public static void AddItem(int index, GameObject item)
    {
        if (items.ContainsKey(index)) { return; }
        items.Add(index, item);
    }

    public static void RemoveItem(int index)
    {
        items.Remove(index);
    }

    public static GameObject GetItem(int index)
    {
        return items[index];
    }

    public static void AddID(GameObject gameObject, int id)
    {
        if (ids.ContainsKey(gameObject)) { return; }
        ids.Add(gameObject, id);
    }
    public static void RemoveID(GameObject gameObject)
    {
        ids.Remove(gameObject);
    }
    public static int GetID(GameObject gameObject)
    {
        return ids[gameObject];
    }

    public static void AddEquie(int equie, GameObject gameObject)
    {
        if (equies.ContainsKey(equie)) { return; }
        equies.Add(equie, gameObject);
    }
    public static void RemoveEquie(int equie)
    {
        equies.Remove(equie);
    }
    public static GameObject GetEquie(int equie)
    {
        return equies[equie];
    }
    public static void AddMaterial(int equie, GameObject gameObject)
    {
        if (materials.ContainsKey(equie)) { return; }
        materials.Add(equie, gameObject);
    }
    public static void RemoveMaterial(int equie)
    {
        materials.Remove(equie);
    }
    public static GameObject GetMaterial(int equie)
    {
        return materials[equie];
    }
    public static void AddSplinters(int equie, GameObject gameObject)
    {
        if (Splinters.ContainsKey(equie)) { return; }
        Splinters.Add(equie, gameObject);
    }
    public static void RemoveSplinters(int equie)
    {
        Splinters.Remove(equie);
    }
    public static GameObject GetSplinters(int equie)
    {
        return Splinters[equie];
    }
}