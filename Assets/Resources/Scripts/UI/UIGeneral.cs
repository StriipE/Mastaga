using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGeneral : MonoBehaviour {

    public int sceneDestination;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onButtonClick()
    {
        SceneManager.LoadScene(sceneDestination);
    }
}
