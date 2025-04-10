using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Spell : MonoBehaviour
{
    public Button button;
    public Slider cooldownBar;
    public SpellType spell;
    public LayerMask targetLayer;
    public float cooldown;
    public GameObject enemySide, allySide, statusEffecter, statBooster;

    float timer;
    bool spellReady;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        cooldownBar.maxValue = cooldown;
        spellReady = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!spellReady) timer += Time.deltaTime;
        if (timer >= cooldown) spellReady = true;

        cooldownBar.value = timer;
    }

    public void CastSpell()
    {
        if(spellReady)
        {
            switch (spell)
            {
                case SpellType.Blazing:
                    Blazing();
                    break;

                case SpellType.Freezing:
                    Freezing();
                    break;

                case SpellType.Healing:
                    Healing();
                    break;

                case SpellType.Shielding:
                    Shielding();
                    break;
            }

            timer = 0;
            spellReady = false;
        }
        
    }

    public void Blazing()
    {
        Collider[] enemies = Physics.OverlapSphere(Vector3.zero, 1000, targetLayer);

        foreach(Collider enemy in enemies)
        {
            enemy.gameObject.GetComponent<HeroController>().TakeDamage(15, StatChangeType.PercentageOfCurrent);
        }
    }

    public void Freezing()
    {
        Collider[] enemies = Physics.OverlapSphere(Vector3.zero, 1000, targetLayer);
        List<GameObject> targets = new();
        foreach(Collider enemy in enemies) targets.Add(enemy.gameObject);

        GameObject firstEnemy = NearestTarget(enemySide, targets);
        targets.Remove(firstEnemy);
        GameObject secondEnemy = NearestTarget(enemySide, targets);

        StatusEffecter.Effect effect = new(StatusEffect.Freeze, firstEnemy, 5);
        if(firstEnemy != null) Instantiate(statusEffecter, firstEnemy.transform).GetComponent<StatusEffecter>().Init(effect);

        effect = new(StatusEffect.Freeze, secondEnemy, 5);
        if(secondEnemy != null) Instantiate(statusEffecter, secondEnemy.transform).GetComponent<StatusEffecter>().Init(effect);
    }

    public void Healing()
    {
        Collider[] allies = Physics.OverlapSphere(Vector3.zero, 1000, targetLayer);
        List<GameObject> targets = new();
        foreach (Collider ally in allies) targets.Add(ally.gameObject);

        GameObject firstAlly = WeakestTarget(targets);
        targets.Remove(firstAlly);
        GameObject secondAlly = WeakestTarget(targets);
        targets.Remove(secondAlly);
        GameObject thirdAlly = WeakestTarget(targets);

        if (firstAlly != null) firstAlly.GetComponent<HeroController>().Heal(Stat.Health, 10, StatChangeType.Percentage);
        if (secondAlly != null) secondAlly.GetComponent<HeroController>().Heal(Stat.Health, 10, StatChangeType.Percentage);
        if (thirdAlly != null) thirdAlly.GetComponent<HeroController>().Heal(Stat.Health, 10, StatChangeType.Percentage);
    }

    public void Shielding()
    {
        Collider[] allies = Physics.OverlapSphere(Vector3.zero, 1000, targetLayer);
        List<GameObject> targets = new();
        foreach (Collider ally in allies) targets.Add(ally.gameObject);

        GameObject frontmost = NearestTarget(enemySide, targets);
        StatBooster.StatBoost boost = new(SetType.IncreaseBy, frontmost.GetComponent<HeroController>(), Stat.Defense, 5, 20, StatChangeType.Percentage);
        Instantiate(statBooster, frontmost.transform).GetComponent<StatBooster>().Init(boost);
    }

    public GameObject NearestTarget(GameObject distanceFrom, List<GameObject> targets)
    {
        GameObject bestTarget = null;
        float shortestDistance = Mathf.Infinity;
        foreach (GameObject target in targets)
        {
            float distance = Vector3.Distance(distanceFrom.transform.position, target.transform.position);
            if(distance <= shortestDistance)
            {
                shortestDistance = distance;
                bestTarget = target;
            }
        }

        return bestTarget;
    }

    public GameObject WeakestTarget(List<GameObject> targets)
    {
        GameObject bestTarget = null;
        float lowestHealth = Mathf.Infinity;
        foreach (GameObject target in targets)
        {
            float health = target.GetComponent<HeroController>().GetStat(Stat.Health);
            if (health <= lowestHealth)
            {
                lowestHealth = health;
                bestTarget = target;
            }
        }

        return bestTarget;
    }
}

public enum SpellType
{
    Blazing,
    Freezing,
    Healing,
    Shielding
}
