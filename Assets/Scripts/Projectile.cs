using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed, lifetime;
    public bool homing;
    public bool piercing;
    public bool spawnAOE;
    public AreaOfEffect.AOE AOE;
    public GameObject effect;


    private GameObject target;
    private float damage;
    private Vector3 direction;

    public void Init(GameObject target, float damage)
    {
        this.target = target;
        this.damage = damage;
    }

    private void Start()
    {
        if (!homing) direction = target.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(homing)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            transform.LookAt(target.transform);
        }
        else transform.position = Vector3.MoveTowards(transform.position, direction + transform.position, speed * Time.deltaTime);

        lifetime -= Time.deltaTime;
        if (lifetime <= 0) Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == target.layer)
        {
            if (spawnAOE)
            {
                GameObject instantiatedAOE = Instantiate(effect, transform.position, Quaternion.identity);
                AOE.effectAmount = damage;
                AOE.targetLayer = LayerMask.GetMask(LayerMask.LayerToName(target.layer));
                instantiatedAOE.GetComponent<AreaOfEffect>().Init(AOE);

            }
            else target.GetComponent<HeroController>().TakeDamage(damage, StatChangeType.Fixed);

            if (!piercing) Destroy(gameObject);
        }
    }
}
