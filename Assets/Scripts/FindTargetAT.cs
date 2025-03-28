using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class FindTargetAT : ActionTask {

		public FindCriteria criteria;
        public CharacterType characterType;
		public BBParameter<GameObject> target;

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
            if (characterType == CharacterType.Ally) layer = agent.gameObject.layer;
            else
            {
                if (agent.gameObject.layer == LayerMask.GetMask("Enemies")) layer = LayerMask.GetMask("Allies");
                else layer = LayerMask.GetMask("Enemies");
            }
            Collider[] enemies = Physics.OverlapSphere(agent.transform.position, Mathf.Infinity, layer);

			if(enemies != null)
			{
                GameObject bestTarget = null;

                switch (criteria)
                {
                    case FindCriteria.Nearest:
                        float shortestDistance = Mathf.Infinity;
                        foreach (Collider enemy in enemies)
                        {
                            float distance = Vector3.Distance(agent.transform.position, enemy.transform.position);

                            if (distance < shortestDistance)
                            {
                                shortestDistance = distance;
                                bestTarget = enemy.gameObject;
                            }
                        }
                        break;

                    case FindCriteria.Furthest:
                        float furthestDistance = 0;
                        foreach (Collider enemy in enemies)
                        {
                            float distance = Vector3.Distance(agent.transform.position, enemy.transform.position);

                            if (distance > furthestDistance)
                            {
                                furthestDistance = distance;
                                bestTarget = enemy.gameObject;
                            }
                        }
                        break;

                    case FindCriteria.HighestHealthPercentage:
						float highestHealth = 0;
                        foreach (Collider enemy in enemies)
                        {
							float health = enemy.GetComponent<HeroController>().GetHealthPercentage();

                            if (health > highestHealth)
                            {
                                highestHealth = health;
                                bestTarget = enemy.gameObject;
                            }
                        }
                        break;

                    case FindCriteria.LowestHealthPercentage:
                        float lowestHealth = Mathf.Infinity;
                        foreach (Collider enemy in enemies)
                        {
                            float health = enemy.GetComponent<HeroController>().GetHealthPercentage();

                            if (health < lowestHealth)
                            {
                                lowestHealth = health;
                                bestTarget = enemy.gameObject;
                            }
                        }
                        break;

                }

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