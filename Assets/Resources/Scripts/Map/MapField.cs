using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapField : MonoBehaviour {

    public int fieldId = 1;
    
    public State.Type actualMaptype = State.Type.Empty;

    public void onButtonClick()
    {
        onInteraction();
    }

    public void OnMouseDown()
    {
        onInteraction();
    }

    private void onInteraction()
    {
        GameData.actualMapFieldId = fieldId;
        switch (actualMaptype)
        {
            case State.Type.Empty:
                SceneManager.LoadScene(GameData.fightId);
                return;
            case State.Type.Cleared:
                GameObject.Find("ConstructMapPopUp").GetComponent<MapPopUp>().onActivate();
                break;
            case State.Type.Harvest:
                SceneManager.LoadScene(GameData.harvestBuildId);
                break;
            case State.Type.Mine:
                SceneManager.LoadScene(GameData.mineBuildId);
                break;
            default:
                return;
        }
    }
}
