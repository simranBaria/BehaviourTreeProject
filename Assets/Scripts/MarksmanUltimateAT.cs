using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class MarksmanUltimateAT : ActionTask {
		public GameObject arrow;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			Collider[] enemies;

			if (agent.gameObject.layer == LayerMask.GetMask("Enemies")) enemies = Physics.OverlapSphere(agent.transform.position, 100, LayerMask.GetMask("Allies"));
			else enemies = Physics.OverlapSphere(agent.transform.position, 100, LayerMask.GetMask("Enemies"));

			foreach (Collider enemy in enemies)
            {
				GameObject instantiatedProjectile = Object.Instantiate(arrow, enemy.gameObject.transform.position + Vector3.up * 20, Quaternion.identity);
				instantiatedProjectile.GetComponent<Projectile>().SetTarget(enemy.gameObject);
				instantiatedProjectile.GetComponent<Projectile>().SetDamage(agent.GetComponent<HeroController>().GetAttack());
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