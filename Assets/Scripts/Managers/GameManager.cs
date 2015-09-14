using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager ins;
    public Vector2 screenBorders;
    private int _lifes =3;
    private int _bombs = 5;
    public float finishLinePositionZ;
    public Transform finishLine;
    public float offsetFinishLine = 1;
    private float _timer = 60;
    public VisualLifeController lifeController;
    private int _score;
    public UnitsManager unitsManager;
    
    void Awake () {
        
        //Singletone
        if (ins == null) { ins = this; DontDestroyOnLoad(gameObject); } else { Destroy(gameObject); }

        if (lifeController==null)
        lifeController = GetComponent<VisualLifeController>();

        if (unitsManager == null)
            unitsManager = GetComponent<UnitsManager>();



    }

    private void Start()
    {
        screenBorders = GetScreenBorders();
        Debug.Log("Width = " + screenBorders.x + "; Height is = " + screenBorders.y);
        finishLinePositionZ = -screenBorders.y+ offsetFinishLine;
        SetDefaultParameters();
    }

    public float Timer
    {
        get { return _timer; }
        set { _timer = value; }
    }
    public int Score
    {
        get { return _score; }
        set { _score = value; }
    }

    public int Bombs
    {
        get { return _bombs; }
        set { _bombs = value; }
    }

    public void TimerCountdown()
    {
        Timer -= Time.deltaTime;
        if (Timer <= 0)
        {
            LevelClear();
        }
    }

    public void Lose()
    {
        ChangeGameState(UIManager.UIState.LOSE);
    }

    public void ChangeGameState(UIManager.UIState gamestate)
    {
      UIManager.ins.ChangeUIState(gamestate);
      AnyChanged();
    }

    public void LevelClear()
    {
        ChangeGameState(UIManager.UIState.LEVEL_CLEAR);
    }

    public void Restart()
    {
        ClearGameField();
        ChangeGameState(UIManager.UIState.PLAY_GAME);
        SetDefaultParameters();
    }

    public void SetDefaultParameters()
    {
        Timer = 60; //default time round
        Score = 0;
        _lifes = 3;
        _bombs = 3;
        lifeController.AddLifes(_lifes);
    }

    public void ClearGameField()
    {
        // выключить всех противников
     unitsManager.DisableAllunits();
    }

    public void MainMenu()
    {
        ChangeGameState(UIManager.UIState.MAIN_MENU);
    }

    public void StartGame()
    {
        Restart();
    }

    public void OnClick(string cmd)
    {
        System.Type thisType = this.GetType();
        System.Reflection.MethodInfo theMethod = thisType.GetMethod(cmd.ToString());

        if (theMethod != null)
            theMethod.Invoke(this, null);

        AnyChanged();
    }
    
    private Vector2 GetScreenBorders()
    {
        Camera cam = Camera.main;
        Vector3 p1 = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.transform.position.y));
        Vector3 p2 = cam.ViewportToWorldPoint(new Vector3(1, 0, cam.transform.position.y));
        Vector3 p3 = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.transform.position.y));
        float width = (p2 - p1).magnitude;
        float height  = (p3 - p2).magnitude;
        Vector2 dimensions  = new Vector2(width/2, height/2);
        return dimensions;
    }

    void Update () {


        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (UIManager.ins.uistate == UIManager.UIState.PAUSE)
            {
                Resume();
            }
            else if (UIManager.ins.uistate == UIManager.UIState.PLAY_GAME)
            {
                Pause();
            }

        }

        if (UIManager.ins.uistate!= UIManager.UIState.PLAY_GAME)
            return;
        
        unitsManager.SpawnUnits();
        TimerCountdown();

        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
                if (hit.collider != null && hit.collider.tag == "unit")
                    hit.collider.GetComponent<BaseUnit>().PushOnUnit();
        }

    }

    public void Resume()
    {
        ChangeGameState(UIManager.UIState.PLAY_GAME);
        Time.timeScale = 1;
    }

    public void Pause()
    {
        
            ChangeGameState(UIManager.UIState.PAUSE);
            Time.timeScale = 0;
        
    }

    public void MinusLife()
    {
        if (_lifes-1 > 0)
        {
            SoundManager.ins.MinusLife();
            EffectsManager.ins.MinusLifeEffect();
            lifeController.RemoveLife();
            _lifes--;
        }
        else
        {
            Lose();
        }
    }

    public void AnyChanged()
    {
      ViewModel.ins.Changed();
    }

    public void SetScore(int scoreForKill)
    {
        Score += scoreForKill;
        AnyChanged();
    }
}
