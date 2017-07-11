﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class State {


    //La pour la map later.
    public enum Type
    {
        Empty,
        Harvest
    }

    public Type type;

    public State(Type t)
    {
        this.type = t;
    }

    public static string getPrefabPath(GameObject obj)
    {
        string outString = AssetDatabase.GetAssetPath(obj);
        
        return outString.Length > 17 ?
            outString.Substring(17, outString.Length - 24) :
            null;
    }

    public abstract void Update();

}
