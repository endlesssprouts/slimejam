using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public float loadLevelAfter;
    public int menuSceneIndex;

    private static LevelManager m_Instance;
    private AsyncOperation m_Operation;

    private void Awake()
    {
        if(m_Instance == null)
            m_Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        if(SceneManager.GetActiveScene().name == "Splash Screen")
            Invoke("LoadNextLevel", loadLevelAfter);
    }

    public void LoadLevel(string sceneName) { SceneManager.LoadScene(sceneName); }

    public AsyncOperation LoadLevelAsync(string sceneName)
    {
        m_Operation = SceneManager.LoadSceneAsync(sceneName);
        return m_Operation;
    }

    public void LoadLevel(int sceneIndex) { SceneManager.LoadScene(sceneIndex); }

    public AsyncOperation LoadLevelAsync(int sceneIndex)
    {
        m_Operation = SceneManager.LoadSceneAsync(sceneIndex);
        return m_Operation;
    }

    public void LoadNextLevel() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); }

    public AsyncOperation LoadNextLevelAsync() { return LoadLevelAsync(SceneManager.GetActiveScene().buildIndex + 1); }

    public void ReturnToMenu() { LoadLevel(menuSceneIndex); }
    
    public void QuitGame() { Application.Quit(); }

    public void ContinueToScene() { m_Operation.allowSceneActivation = true; }
}
