using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : Dropper {

    public HarvestField master = null;

    public Material growing;
    public Material alive;
    public Material old;

    private GameObject actualMesh;
    public GameObject aliveMesh;
    public List<Item> items;

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
        Destroy(actualMesh);
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
            //TODO : On affiche une image en particule sur l'UI, mais balek j'ai pas l'temps
            for (int i = 0; i < dropItems.Count; ++i)
            {
                int random = (int)((Random.value * this.dropCountMax[i]) + this.dropCountMin[i]);
                PlayerData.inventory.addItem(this.dropItems[i], random);
            }
            master.onDrop();
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
