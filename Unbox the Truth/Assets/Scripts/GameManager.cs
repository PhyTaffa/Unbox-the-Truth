using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //OnPlayerDied();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnPlayerDied()
    {
        //Debug.Log("Game Over");
        //shpuld change to the current scene
        SceneManager.LoadScene("SpikesScene");
    }
    
}
