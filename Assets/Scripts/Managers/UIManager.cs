using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager ins;
    public enum UIState
    {
       NONE,
       PAUSE,
       MAIN_MENU,
       SELECT_LEVEL,
       PLAY_GAME,
       LEVEL_CLEAR,
       LOSE,

    }

    public UIState uistate;
    public event System.Action<UIState> ChangeUIEvent;
    

    void Awake () {
        //Singletone
        if (ins == null) { ins = this; DontDestroyOnLoad(gameObject); } else { Destroy(gameObject); }

    }

    public void ChangeUIState(UIState state)
    {
        uistate = state;
    }

    void Update () {
	
	}

    public void Exit()
    {
        Application.Quit();
    }


}
