using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Projectile : MonoBehaviour
{
    public float speed, lifetime;
    public DamageType damageType;
    public bool homing;
    public bool piercing;


    public GameObject target;
    public float damage;
    public GameObject agent;

    [Header("For AOE damage only")]
    public GameObject AOEObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(homing)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            transform.LookAt(target.transform);
        }
        else transform.position = transform.position + Vector3.forward * Time.deltaTime;

        lifetime -= Time.deltaTime;
        if (lifetime <= 0) Destroy(gameObject);
    }

    public void SetTarget(GameObject target) => this.target = target;

    public void SetDamage(float damage) => this.damage = damage;

    public void SetAgent(GameObject agent) => this.agent = agent;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == target.layer)
        {
            if (damageType == DamageType.AreaOfEffect)
            {
                GameObject instantiatedAOE = Instantiate(AOEObject, transform.position, Quaternion.identity);
                instantiatedAOE.GetComponent<AreaOfEffect>().SetEffectAmount(damage);
                instantiatedAOE.GetComponent<AreaOfEffect>().SetTargetLayer(target.layer);
            }
            else target.GetComponent<HeroController>().TakeDamage(damage, StatChangeType.Fixed);

            if (!piercing) Destroy(gameObject);

            Debug.Log($"{agent.name} dealt {damage} to {collision.gameObject.name}");
        }
    }
}

public enum DamageType
{
    Direct,
    AreaOfEffect
}
