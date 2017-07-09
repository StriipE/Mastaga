using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapFieldChangeScene : ChangeSceneButton {

    public int fieldId = 1;

    private void Start()
    {
        
    }

    public new void onButtonClick()
    {
        GameData.actualMapFieldId = fieldId;
        base.OnMouseDown();
    }

    public  new void OnMouseDown()
    {
        GameData.actualMapFieldId = fieldId;
        base.OnMouseDown();
    }
}
