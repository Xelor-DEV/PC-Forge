using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private LevelUnlockSystem unlockSystem;

    [Header("UI Elements")]
    [SerializeField] private Button[] levelButtons;

    private void Start()
    {
        RefreshMenu();
    }

    public void RefreshMenu()
    {
        for (int i = 0; i < levelButtons.Length; ++i)
        {
            int levelIndex = i + 1;
            levelButtons[i].interactable = unlockSystem.IsLevelUnlocked(levelIndex);
        }
    }

    public void LoadLevel(string levelName)
    {
        SceneLoader.Instance.LoadScene(levelName);
    }
}
