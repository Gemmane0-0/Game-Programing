using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour{
   public void PlayGame() {
        SceneManager.LoadSceneAsync(1);  // Loads scene with build index 1
    }

    public void QuitGame() {
        Application.Quit();  // Quits the application (only works in a build)
    }

}
