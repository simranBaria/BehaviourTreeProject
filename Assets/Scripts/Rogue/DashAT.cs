using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;

namespace NodeCanvas.Tasks.Actions {

	public class DashAT : ActionTask {

		public BBParameter<Vector3> target;
		public float dashSpeed;
		public float stoppingDistnace;
		public BBParameter<bool> dashing;
		NavMeshAgent nmAgent;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			nmAgent = agent.gameObject.GetComponent<NavMeshAgent>();
			nmAgent.speed = dashSpeed;
			agent.GetComponent<Collider>().enabled = false;
			nmAgent.SetDestination(target.value);
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			if (Vector3.Distance(agent.transform.position, target.value) <= nmAgent.stoppingDistance)
			{
				Debug.Log("here" + LayerMask.LayerToName(agent.gameObject.layer));
				agent.GetComponent<Collider>().enabled = true;
				nmAgent.speed = 3.5f;
				dashing.value = false;
				EndAction(true);

			}
		}

		//Called when the task is disabled.
		protected override void OnStop() {
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}