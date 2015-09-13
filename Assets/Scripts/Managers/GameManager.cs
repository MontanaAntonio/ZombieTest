using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager ins;
    public Vector2 screenBorders;
    public int lifes =3;
    public int bombs = 5;
    public float finishLinePositionZ;
    public Transform finishLine;
    public float offsetFinishLine = 1;
    private float timer = 60;
    public VisualLifeController lifeController;
    
    void Awake () {
        
        //Singletone
        if (ins == null) { ins = this; DontDestroyOnLoad(gameObject); } else { Destroy(gameObject); }

        if (lifeController==null)
        lifeController = GetComponent<VisualLifeController>();
    }

    private void Start()
    {
        screenBorders = GetScreenBorders();
        Debug.Log("Width = " + screenBorders.x + "; Height is = " + screenBorders.y);
        finishLinePositionZ = -screenBorders.y+ offsetFinishLine;
    }

    public float Timer
    {
        get { return timer; }
    }



    public void TimerCountdown()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            LevelClear();
        }
    }

    public void ChangeGameState(UIManager.UIState gamestate)
    {
      UIManager.ins.ChangeUIState(gamestate);
      AnyChanged();
    }

    public void LevelClear()
    {
        ChangeGameState(UIManager.UIState.LEVEL_CLEAR);
        Debug.Log("LevelClear");
    }

    public void YouLose()
    {

    }

    public void Restart()
    {

    }

    public void RandomSpawnUnit()
    {

    }

    public void StartGame()
    {
        ChangeGameState(UIManager.UIState.PLAY_GAME);
    }

    public void OnClick(string cmd)
    {
        System.Type thisType = this.GetType();
        System.Reflection.MethodInfo theMethod = thisType.GetMethod(cmd.ToString());

        if (theMethod != null)
            theMethod.Invoke(this, null);

        AnyChanged();
    }



    public void StartNextLevel()
    {

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

        if (Input.GetKeyUp(KeyCode.S))
        {
            RandomSpawnUnit();
        }

   


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

    }

    public void AnyChanged()
    {
      ViewModel.ins.Changed();
    }
}
