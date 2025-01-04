using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //OnPlayerDied();
        Exit Exit = FindObjectOfType<Exit>();
        if(Exit)
        {
            Exit.onLevelExitEvent.AddListener(OnLevelExit);
        }
        
        CameraEnemy CameraEnemy = FindObjectOfType<CameraEnemy>();
        if(CameraEnemy)
        {
            CameraEnemy.onPlayerDiedEvent.AddListener(OnPlayerDied);
        }
    }
    public void OnPlayerDied()
    {
        Scene curScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(curScene.name);
    }

    void OnLevelExit()
    {
        SceneManager.LoadScene("HubLevel");
    }
    
}
