using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuOptionsScript : MonoBehaviour {
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject loadingScreenCanvas;
    [SerializeField] private Image image;
    [SerializeField] private Slider slider;
    private Color loadingFade;
    private void Awake() {
        loadingFade = new Color(255, 255, 255);
        image.color = loadingFade;
        slider.value=0f;
        mainMenuCanvas.SetActive(true);
        loadingScreenCanvas.SetActive(false);
    }
    public void onClickSinglePlayer() {
        StartCoroutine(LoadSceneAsync(1));
    }
    public void onClickMultiPlayer() {
        StartCoroutine(LoadSceneAsync(2));
    }
    public void onClickQuit() {
        Application.Quit();
    }

    IEnumerator LoadSceneAsync(int sceneID) {
        mainMenuCanvas.SetActive(false);
        loadingScreenCanvas.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);
        while(!operation.isDone) {
            float progressValue = Mathf.Clamp01(operation.progress/0.9f);
            loadingFade.b = 255 - (255*progressValue);
            loadingFade.g = 255 - (255*progressValue);
            image.color = loadingFade;
            slider.value=progressValue;
            yield return null;
        }
    }
}
