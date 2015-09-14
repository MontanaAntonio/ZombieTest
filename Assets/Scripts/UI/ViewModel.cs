using System;
using UnityEngine;
using System.Reflection;

public class ViewModel : MonoBehaviour {
	public delegate void AnyChanges(bool cmd);
	public static event AnyChanges AnyChanged;

	public static ViewModel ins;
	public bool currentBoolValue;
    public bool currentSetBoolValue;
         
    public string currentStringValue;
    public string currentSetValue;

    public float currentSetFloatValue;
    public string DebugLog;

	void Awake () {

		if (ins == null)
		{
			ins = this;
            DontDestroyOnLoad(gameObject);
		} else
		{ 
            Destroy(gameObject);
		}
	}

    public bool CallBool(string msg)
    {
        currentBoolValue = false;
            
		System.Type thisType = this.GetType();
		MethodInfo theMethod = thisType.GetMethod(msg);
        if (GameManager.ins != null)
		if(theMethod!=null)
			theMethod.Invoke(this, null);

		return currentBoolValue;
	}
	public string CallString(string msg)
	{
		System.Type thisType = this.GetType();
		MethodInfo theMethod = thisType.GetMethod(msg);
		if(GameManager.ins!=null)
		if(theMethod!=null)
			theMethod.Invoke(this, null);
		
		return currentStringValue;
	}

    public void SetValue(string msg, string value)
    {
        System.Type thisType = this.GetType();
        MethodInfo theMethod = thisType.GetMethod(msg);
        if (GameManager.ins != null)
        currentSetValue = value;
        if (theMethod != null)
            theMethod.Invoke(this, null);

        currentSetValue = "";
        
    }



    public static void MakeChanged()
    {
        if(ins!=null)
           ins.Change();
    }
	public void Changed()
	{
	    Change();
	}

    void Change()
    {
        if (AnyChanged != null)
            AnyChanged(true);
    }

	public void Changed(UIManager.UIState state)
	{
        Change();
	}

    public void UIState()
    {
        currentStringValue = UIManager.ins.uistate.ToString();
    }
    public void LevelClear()
    {
        currentBoolValue = UIManager.ins.uistate == UIManager.UIState.LEVEL_CLEAR;
    }

    public void PlayGame()
    {
        currentBoolValue = UIManager.ins.uistate == UIManager.UIState.PLAY_GAME;
    }

    public void Lose()
    {
        currentBoolValue = UIManager.ins.uistate == UIManager.UIState.LOSE;
    }

    public void MainMenu()
    {
        currentBoolValue = UIManager.ins.uistate == UIManager.UIState.MAIN_MENU;
    }

    public void Pause()
    {
        currentBoolValue = UIManager.ins.uistate == UIManager.UIState.PAUSE;
    }



    internal float CallFloat(string msg)
    {
        System.Type thisType = this.GetType();
        MethodInfo theMethod = thisType.GetMethod(msg);
        if(GameManager.ins!=null)
        if (theMethod != null)
            theMethod.Invoke(this, null);

        return currentSetFloatValue;
    }

    internal void SetFloatValue(string msg, float value)
    {
        System.Type thisType = this.GetType();
        MethodInfo theMethod = thisType.GetMethod(msg);
        if (GameManager.ins != null)
        currentSetFloatValue = value;
        if (theMethod != null)
            theMethod.Invoke(this, null);

        currentSetFloatValue = 0;
    }
    internal void SetToogleValue(string msg, bool value)
    {
        System.Type thisType = this.GetType();
        MethodInfo theMethod = thisType.GetMethod(msg);
        
        //Debug.Log(msg + "/"+value);
        if (GameManager.ins != null)
        currentSetBoolValue = value;
        if (theMethod != null)
            theMethod.Invoke(this, null);

    } 
    internal void ChangedWithEndOfFrame()
    {
        Invoke("Changed",0.1f);
    }
    
    internal static void ChangedOnEndOfFrame()
    {
        if (ins != null)
        {
            ins.ChangedWithEndOfFrame();
        }
    }



    public void CurrentLevelScore()
    {
        currentStringValue = GameManager.ins.Score.ToString();
    }
    public void TimerCountdown()
    {
        currentStringValue = String.Format("{0:0}", GameManager.ins.Timer);
    }


    public void Bombs()
    {
        currentStringValue = String.Format("{0}", GameManager.ins.Bombs);
    }

}
