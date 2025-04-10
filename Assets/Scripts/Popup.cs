using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    public float duration;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = duration;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0) DestroyPopup();
    }

    public void DestroyPopup()
    {
        Destroy(gameObject);
    }
}
