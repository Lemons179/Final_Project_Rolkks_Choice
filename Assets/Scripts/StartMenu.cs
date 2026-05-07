using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // Name of the scene loaded when the Play button is pressed.
    public string gameSceneName = "SampleScene";

    public void PlayGame()
    {
        // Load the gameplay scene.
        SceneManager.LoadScene(gameSceneName);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // If the game is playing in the Unity Editor, stop the game
        #else
            Application.Quit(); // If the game is a built game playing, quit the application and close the entire game
        #endif
    }
}