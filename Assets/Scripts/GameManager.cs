using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameObject BallObj;
    public GameObject PaddleObj;
    public GameObject AI_PaddleObj;

    public Vector2 TopRight;
    public Vector2 BottomLeft;
    public GameObject InstTextGameObj;
    public GameObject YouWinTextGameObject;
    public GameObject YouLoseTextGameObject;

    // Start is called before the first frame update
    void Start()
    {
        TopRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        BottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));

        PaddleObj.GetComponent<PaddleScript>().SetInitialPosition();
        AI_PaddleObj.GetComponent<AI_PaddleScript>().SetInitialPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            BallObj.GetComponent<BallScript>().IsPaused = false;
            InstTextGameObj.SetActive(false);
        }
    }

    public void YouWin()
    {
        Debug.Log("You Win");
        YouWinTextGameObject.SetActive(true);
        Invoker.InvokeDelayed(UnfreezeGame, 1.0f);
        Time.timeScale = 0;
    }

    public void YouLose()
    {
        Debug.Log("You Lose");
        YouLoseTextGameObject.SetActive(true);
        Invoker.InvokeDelayed(UnfreezeGame, 1.0f);
        Time.timeScale = 0;
    }

    void RestartGame()
    {
        SceneManager.LoadScene(0);

    }

    void UnfreezeGame()
    {
        Time.timeScale = 1.0f;

        Invoke("RestartGame", 1.0f);
    }

    public void IncreaseAILevel()
    {
        AI_PaddleObj.GetComponent<AI_PaddleScript>().Speed += 0.1f;
    }

}
