using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOfEffect : MonoBehaviour
{
    public float lifetime;
    public EffectType effect;
    float effectAmount;
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
        HeroController hero = other.gameObject.GetComponent<HeroController>();
        if (other.gameObject.layer == targetLayer)
        {
            if (effect == EffectType.Damaging) hero.TakeDamage(effectAmount, StatChangeType.Fixed);
            else hero.Heal(effectAmount, StatChangeType.Fixed);
        }
    }

    public void SetEffectAmount(float amount) => effectAmount = amount;

    public void SetTargetLayer(LayerMask layer) => targetLayer = layer;
}

public enum EffectType
{
    Healing,
    Damaging
}
