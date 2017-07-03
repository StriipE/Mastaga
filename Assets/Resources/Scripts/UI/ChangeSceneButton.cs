using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneButton : MonoBehaviour {

    public int sceneDestination;

    public void onButtonClick()
    {
        SceneManager.LoadScene(sceneDestination);
    }
}
