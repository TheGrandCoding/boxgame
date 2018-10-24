using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

    public Text RedText;
    public Text BlueText;

    public Image BackgroundImg;
    public Text WinText;

	// Use this for initialization
	void Start () {
        BackgroundImg.enabled = false;
        WinText.enabled = false;
	}

    private bool won = false;
	// Update is called once per frame
	void Update () {
        if(won)
        {
            BackgroundImg.enabled = true;
            WinText.enabled = true;
            WinText.text = string.Format("{0} wins!\r\n{1} : {2}", GameScript.Player1Amount > GameScript.Player2Amount ? "Red" : "Blue", GameScript.Player1Amount, GameScript.Player2Amount);

            return;
        }
        if(GameScript.Player1Amount + GameScript.Player2Amount == 9)
        {
            won = true;
        }
        RedText.text = "Red\r\n" + GameScript.Player1Amount.ToString();
        BlueText.text = "Blue\r\n" + GameScript.Player2Amount.ToString();
        RedText.fontStyle = GameScript.CurrentPlayerGo == GameScript.WhoPlayer.Player1 ? FontStyle.Bold : FontStyle.Normal;
        BlueText.fontStyle = GameScript.CurrentPlayerGo == GameScript.WhoPlayer.Player2 ? FontStyle.Bold : FontStyle.Normal;
    }
}
