using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Sprite normal, highlighted;
    public SpriteRenderer SR;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        SR.sprite = highlighted;
    }

    private void OnMouseExit()
    {
        SR.sprite = normal;
    }
}
