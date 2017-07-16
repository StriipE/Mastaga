using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plant : Dropper {

    public HarvestField master = null;
    
    public Material alive;
    public Material old;

    private GameObject actualMesh;
    public GameObject aliveMesh;
    private GameObject dropText = null;
    private Text dropTextComp;

    public int growTime;
    public int aliveTime;
    public int dropTime;

    private bool droppable = false;

    public void setPosition(float x, float z)
    {
        this.gameObject.transform.SetPositionAndRotation(new Vector3(x, this.gameObject.transform.position.y, z), new Quaternion());
        this.gameObject.SetActive(true);
    }

    public void onDroppable()
    {
        this.droppable = true;
        if (dropText == null)
        {
            GameObject canvas = GameObject.Find("NPData");
            dropText = new GameObject("DroppableText");
            dropText.transform.SetParent(canvas.transform);
           
            dropTextComp = dropText.AddComponent<Text>();
            dropTextComp.text = "!";
            dropTextComp.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            dropTextComp.fontSize = 30;
            dropText.transform.position = GameObject.Find("Main Camera").GetComponent<Camera>().
                                                 WorldToScreenPoint(transform.position +
                                                 new Vector3(1.5f, 0, 0));
            
        }
    }
     public void onAlive()
    {
        this.gameObject.GetComponent<Renderer>().material = alive;
        if (aliveMesh)
        {
            setupNewState(aliveMesh);
        }
    }

    public void onOld()
    {
        if (actualMesh)
        {
            Destroy(actualMesh);
        }
        this.gameObject.GetComponent<Renderer>().material = old;
    }

    public void onDeath()
    {
        Destroy(this.gameObject);
    }

    public void OnMouseDown()
    {
        if (this.droppable)
        {
            Destroy(dropText);
            for (int i = 0; i < dropItems.Count; ++i)
            {
                int random = (int)((Random.value * this.dropCountMax[i]) + this.dropCountMin[i]);
                GameData.inventory.addItem(this.dropItems[i], random);
                master.onDrop(this.dropItems[i] + "(" + random + ")");
            }
            this.droppable = false;
        }
    }

    private void setupNewState(GameObject plant)
    {
        this.actualMesh = Instantiate(aliveMesh);
        this.actualMesh.gameObject.transform.SetParent(this.gameObject.transform.parent);
        this.actualMesh.GetComponent<PlantChild>().setMaster(this);
        this.actualMesh.transform.SetPositionAndRotation(
            new Vector3(this.transform.position.x,
                0,
                this.transform.position.z),
            new Quaternion());
        this.gameObject.SetActive(false);
    }

    public bool isDroppable()
    {
        return this.droppable;
    }
}
