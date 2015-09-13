using UnityEngine;
using UnityEngine.UI;

public class VisibleDataBinding : MonoBehaviour
{
    public string path;
    public bool Updatable;
    public bool invert;
    public bool RemoveComponentAfterGettingValues;
    public GameObject target;

    public UIManager.UIState UiState = UIManager.UIState.NONE;
     
    protected bool value;
    protected bool subs;
    protected bool isActive = true;
    
    public bool OffImageOnly;

    public void Start()
    {
       
        ViewModel.AnyChanged += Changed;
        target = target != null ? target : gameObject;
        
          
    }
    void OnDestroy()
    {
        ViewModel.AnyChanged -= Changed;
    }

    
    public void LateUpdate()
    {
        if (Updatable)
        {
            CallPath();
        }
    }

    public void Changed(bool cmd)
    {
        CallPath();
    }
    public void Changed()
    {
        CallPath();
    }
    protected void CallPath()
    {
       
        if ((UiState == UIManager.UIState.NONE ))
        {
            if (ViewModel.ins != null)
            {
                value = ViewModel.ins.CallBool(path);
                if (isActive)
                    Change(value);
            }
        }
    }

    protected void Change(bool value)
    {
        if (target != null)
        {
            if (OffImageOnly)
            {
                GetComponent<Image>().enabled = (invert ? !value : value);
            }
            else
            {
                if (target.activeSelf != (invert ? !value : value))
                target.SetActive(invert ? !value : value);
            }

            if (RemoveComponentAfterGettingValues)
                Destroy(this);
        }
    }
}
