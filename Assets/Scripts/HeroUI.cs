using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeroUI : MonoBehaviour
{
    Vector3 playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = Camera.main.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = Camera.main.transform.forward;
    }
}
