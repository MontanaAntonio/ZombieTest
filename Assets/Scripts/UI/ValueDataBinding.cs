using UnityEngine;
using UnityEngine.UI;

public class ValueDataBinding : MonoBehaviour {
    
	public string path;
	public bool Updatable;
	public bool RemoveComponentAfterGettingValues;
	public GameObject target;
	public string Format = "{0}";
	GameObject _target;

    Text _tText;

    public UIManager.UIState UiState = UIManager.UIState.NONE;
    

	public void Start()
	{
		ViewModel.AnyChanged += Changed;
		_target = target != null ? target : gameObject;

        _tText = _target.GetComponent<Text>();

		Apply();

	}
	
    public void LateUpdate()
	{
		if (Updatable)
		{
			Apply();
		}
	}

	public void Changed(bool cmd)
	{
        Apply();
	}

	void Apply()
    { 
        if (_tText == null)
            return;

        if (UiState != UIManager.UIState.NONE)
            return;
        if (ViewModel.ins == null)
            return;

        _tText.text = string.Format(Format, ViewModel.ins.CallString(path));
             
        if (RemoveComponentAfterGettingValues)
            Destroy(this);
	}
}
