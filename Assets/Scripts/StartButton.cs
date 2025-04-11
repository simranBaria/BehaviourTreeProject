using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public Sprite active, unactive;
    public bool canStart = false;
    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Enable(bool enable)
    {
        if(enable)
        {
            image.sprite = active;
            canStart = true;
        }
        else
        {
            image.sprite = unactive;
            canStart = false;
        }
    }

    public void StartRound()
    {
        if (canStart) Player.instance.StartRound();
    }
}
