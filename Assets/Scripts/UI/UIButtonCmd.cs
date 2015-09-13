using UnityEngine;
using System.Collections;

public class UIButtonCmd : MonoBehaviour, UnityEngine.EventSystems.IPointerClickHandler
{

    public string cmd;

    public void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
    {
        GameManager.ins.OnClick(cmd);
    }
}
