using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ClickTool : MonoBehaviour
{
    public static Button Get(GameObject go)
    {
        Button button = null;
        if (go.GetComponent<Button>() == null)
        {
            button = go.AddComponent<Button>();
            button.transition = Selectable.Transition.None;
        }
        else
        {
            button = go.GetComponent<Button>();
        }
        return button;
    }
	
}
