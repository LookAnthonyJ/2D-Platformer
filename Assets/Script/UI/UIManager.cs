
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    [Header("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;


    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);

    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            //If pause screen already active unpause and viceversa
            if(pauseScreen.activeInHierarchy)
            PauseGame(false);
            else
                PauseGame(true);

        }
    }

    #region Game over
    //Activate game over screen
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }    

    //Game over function

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit(); //Quits the game (Only work)
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; //Exits play mode (will only be executed in the editor)   
        #endif
    }
    #endregion

    #region Pause
    private void PauseGame(bool status)
    {
        //If status == true pause| if status == false unpause
        pauseScreen.SetActive(status);

        //When pause status is true change timescale to 0 (time stops
        //when it's false change it to 1 (time goes by normally)
        if(status)
        Time.timeScale = 0;
        else
            Time.timeScale = 1;

    }
    #endregion
}
