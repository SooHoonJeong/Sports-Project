using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBootstrapper : MonoBehaviour
{
    [SerializeField] private string[] scenesToLoad = { "Court", "Lighting", "UI" };

    private void Start()
    {
        foreach (var sceneName in scenesToLoad)
        {
            if (!SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            }
        }
    }
}
