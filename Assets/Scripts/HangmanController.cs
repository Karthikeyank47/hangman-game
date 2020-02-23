using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangmanController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject head;
    public GameObject body;
    public GameObject arms;
    public GameObject legs;

    private int tries;
    private GameObject[] parts;
    void Start()
    {
        parts = new GameObject[] { legs, arms, body, head };
        reset();
        
    }

    public bool isDead
    {
        get { return tries < 0; }
    }

    public void punish()
    {
        if(tries>=0)
        {
            parts[tries--].SetActive(true);
        }
    }

    public void reset()
    {
        tries = parts.Length - 1;
        foreach(GameObject g in parts)
        {
            g.SetActive(false);
        }
    }

}
