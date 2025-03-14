using UnityEngine;
using UnityEngine.SceneManagement;

public class CurrentSceneManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public VoidEventChannel onPlayerDeath;
    public GameObject playerMovement;
    public bool isPaused = false;
    public GameObject pauseScreen; 
    public VoidEventChannel onPause;
    public VoidEventChannel onResume;
    private void OnEnable(){
        onPlayerDeath.OnEventRaised += Die;
    }

    private void OnDisable(){
        onPlayerDeath.OnEventRaised -= Die;
    }

    public void Die(){
        gameOverScreen.SetActive(true);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);        
    }

    public void Pause() {
        if (Time.timeScale == 0) {
                Time.timeScale = 1;
                isPaused = false;
                playerMovement.GetComponent<PlayerMovement>().enabled = true;
                pauseScreen.SetActive(false);
                onPause.Raise();
            } else {
                Time.timeScale = 0;
                isPaused = true;
                playerMovement.GetComponent<PlayerMovement>().enabled = false;
                pauseScreen.SetActive(true);
                onResume.Raise();
            }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Pause();
        }
#if UNITY_EDITOR 
        if(Input.GetKeyDown(KeyCode.R)) {
            RestartGame();
        }
#endif
    }

    public void RestartGame() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ResumeGame(){
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
    }
}