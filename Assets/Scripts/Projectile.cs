using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed, lifetime;
    public DamageType damageType;
    public bool homing;
    public bool piercing;

    [Header("For AOE damage only")]
    public GameObject AOEObject;

    public GameObject target;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(homing)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed);
            transform.LookAt(target.transform);
        }
        else transform.position = transform.position + Vector3.forward * speed;

        lifetime -= Time.deltaTime;
        if (lifetime <= 0) Destroy(gameObject);
    }

    public void SetTarget(GameObject target) { this.target = target; }

    public void SetDamage(float damage) { this.damage = damage; }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == target.layer)
        {
            if (damageType == DamageType.AreaOfEffect)
            {
                GameObject instantiatedAOE = Instantiate(AOEObject, transform.position, Quaternion.identity);
                instantiatedAOE.GetComponent<AreaOfEffect>().SetDamage(damage);
                instantiatedAOE.GetComponent<AreaOfEffect>().SetTargetLayer(target.layer);
            }
            else target.GetComponent<HeroController>().TakeDamage(damage);

            if (!piercing) Destroy(gameObject);
        }
    }
}

public enum DamageType
{
    Direct,
    AreaOfEffect
}
