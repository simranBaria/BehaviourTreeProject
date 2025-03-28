using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOfEffect : MonoBehaviour
{
    public float lifetime;
    float damage;
    LayerMask targetLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == targetLayer) other.gameObject.GetComponent<HeroController>().TakeDamage(damage);
    }

    public void SetDamage(float damage) { this.damage = damage; }

    public void SetTargetLayer(LayerMask layer) { targetLayer = layer; }
}
