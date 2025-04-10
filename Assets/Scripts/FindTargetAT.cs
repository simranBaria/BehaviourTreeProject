using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class FindTargetAT : ActionTask {

		public FindCriteria criteria;
        public CharacterType characterType;
		public BBParameter<GameObject> target;

        public bool logLastTarget;
        public BBParameter<GameObject> lastTarget;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
            LayerMask layer;
            if (characterType == CharacterType.Ally) layer = LayerMask.GetMask(LayerMask.LayerToName(agent.gameObject.layer));
            else
            {
                if (LayerMask.GetMask(LayerMask.LayerToName(agent.gameObject.layer)) != LayerMask.GetMask("Allies")) layer = LayerMask.GetMask("Allies");
                else layer = LayerMask.GetMask("Enemies");
            }
            Collider[] heroes = Physics.OverlapSphere(agent.transform.position, Mathf.Infinity, layer);

            if (heroes != null)
            {
                GameObject bestTarget = null;

                switch (criteria)
                {
                    case FindCriteria.Nearest:
                        float shortestDistance = Mathf.Infinity;
                        foreach (Collider hero in heroes)
                        {
                            float distance = Vector3.Distance(agent.transform.position, hero.transform.position);

                            if (distance < shortestDistance)
                            {
                                shortestDistance = distance;
                                bestTarget = hero.gameObject;
                            }
                        }
                        break;

                    case FindCriteria.Furthest:
                        float furthestDistance = 0;
                        foreach (Collider hero in heroes)
                        {
                            float distance = Vector3.Distance(agent.transform.position, hero.transform.position);

                            if (distance > furthestDistance)
                            {
                                furthestDistance = distance;
                                bestTarget = hero.gameObject;
                            }
                        }
                        break;

                    case FindCriteria.HighestHealthPercentage:
                        float highestHealth = 0;
                        foreach (Collider hero in heroes)
                        {
                            float health = hero.gameObject.GetComponent<HeroController>().GetHealthPercentage();

                            if (health > highestHealth)
                            {
                                highestHealth = health;
                                bestTarget = hero.gameObject;
                            }
                        }
                        break;

                    case FindCriteria.LowestHealthPercentage:
                        float lowestHealth = Mathf.Infinity;
                        foreach (Collider hero in heroes)
                        {
                            float health = hero.gameObject.GetComponent<HeroController>().GetHealthPercentage();

                            if (health < lowestHealth)
                            {
                                lowestHealth = health;
                                bestTarget = hero.gameObject;
                            }
                        }
                        break;

                }

                if (characterType == CharacterType.Enemy)
                {
                    foreach (Collider hero in heroes)
                    {
                        if (hero.gameObject.CompareTag("Control Target")) bestTarget = hero.gameObject;
                    }
                }

                if (logLastTarget) lastTarget.value = target.value;
                target.value = bestTarget;
            }
			EndAction(true);
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}

public enum FindCriteria
{
	Nearest,
	Furthest,
	HighestHealthPercentage,
	LowestHealthPercentage
}

public enum CharacterType
{
    Ally,
    Enemy,
}