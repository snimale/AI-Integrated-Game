using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOptionsScript : MonoBehaviour {
    public void onClickSinglePlayer() {
        SceneManager.LoadScene("SinglePlayerMap");
    }
    public void onClickMultiPlayer() {
        SceneManager.LoadScene("MultiPlayerMap");
    }
    public void onClickQuit() {
        Application.Quit();
        Debug.Log("quit");
    }
}
