using UnityEngine;

public class TutorialSceneController : MonoBehaviour
{
    [Header("Main Menu Scene Name")]
    [SerializeField, Tooltip("The name of the scene to be placed in this variable is that of the main menu.")] private string sceneName = "MenuPrincipal";
    [Header("References")]
    [SerializeField] private SceneLoader sceneLoader;

    void Start()
    {
        CheckTutorialCompletion();
    }

    private void CheckTutorialCompletion()
    {
        if (SaveManager.Instance.UnlockSystem.IsLevelUnlocked(2) == true)
        {
            sceneLoader.LoadScene(sceneName);
        }
    }
}