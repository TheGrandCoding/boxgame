﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// dont diss my names ok
/// <summary>
/// Script placed on each of the weird cube things
/// </summary>
public class ClickyThingsScript : MonoBehaviour {

    public Material Highlighted;
    public Material Red;
    public Material Blue;
    public Material Transparent;
    public GameScript.WhoPlayer OwnedBy = GameScript.WhoPlayer.None;
    public List<GameScript.GameBlock> PartOfBlocks;
    public int X
    {
        get
        {
            return int.Parse(this.name.Substring(0, 1));
        }
    }
    public int Y
    {
        get
        {
            return int.Parse(this.name.Split(',')[1].Substring(0, 1));
        }
    }
    public int Z
    {
        get
        {
            if (this.name.Contains("-"))
                return 1;
            return 0;
        }
    }

    public void Init(Material high, Material red, Material blue, Material trans, GameScript.GameBlock block)
    {
        Highlighted = high;
        Red = red;
        Blue = blue;
        Transparent = trans;
        if (PartOfBlocks == null)
            PartOfBlocks = new List<GameScript.GameBlock>();
        PartOfBlocks.Add(block);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseOver()
    {
        if(this.OwnedBy == GameScript.WhoPlayer.None)
        {
            this.GetComponent<Renderer>().material = Highlighted;
        }
    }
    void OnMouseExit()
    {
        if(this.OwnedBy == GameScript.WhoPlayer.None)
        {
            this.GetComponent<Renderer>().material = Transparent;
        }
    }

    void OnMouseUp()
    {
        if(this.OwnedBy == GameScript.WhoPlayer.None)
        {
            this.OwnedBy = GameScript.CurrentPlayerGo;
            if(this.OwnedBy == GameScript.WhoPlayer.Player1)
            {
                GameScript.CurrentPlayerGo = GameScript.WhoPlayer.Player2;
                this.GetComponent<Renderer>().material = Red;
            }
            else
            {
                GameScript.CurrentPlayerGo = GameScript.WhoPlayer.Player1;
                this.GetComponent<Renderer>().material = Blue;
            }
            foreach(var b in PartOfBlocks)
            {
                b.SetBlockOwned(new Vector3(X, Y, Z), this.OwnedBy);
            }
        }
        Debug.LogError(string.Format("Clicked ({0}, {1}, {2}) with {3} blocks", X, Y, Z, PartOfBlocks.Count));
    }


}
