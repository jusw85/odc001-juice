using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD_Health : MonoBehaviour
{

    public Sprite[] HeartSprites;

    public Image HeartUI;
    
    //To be referenced to PlayerController script
    //public PlayerController player;

    // Use this for initialization
    void Start ()
    {
       // player = FindObjectOfType<PlayerController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //curHealth to be referenced to PlayerController script. int value. I.e curHealth 5 - sprite no.5, curHealth 4 - sprite no.4.
        //HeartUI.sprite = HeartSprites[player.curHealth];
	}
}
