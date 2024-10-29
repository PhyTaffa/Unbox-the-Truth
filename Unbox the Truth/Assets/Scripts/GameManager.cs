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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnPlayerDied()
    {
        //Debug.Log("Game Over");
        //shpuld change to the current scene
        Scene curScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(curScene.name);
    }

    void OnLevelExit()
    {
        Debug.Log("Level Exit");
        SceneManager.LoadScene("HubLevel");
    }
    
}
