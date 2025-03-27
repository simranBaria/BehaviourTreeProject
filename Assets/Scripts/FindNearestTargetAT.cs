using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class FindNearestTargetAT : ActionTask {

		public LayerMask characterLayer;
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
			Collider[] detectedObjects= Physics.OverlapSphere(agent.transform.position, 100, characterLayer);
			GameObject nearestObject = null;
			float shortestDistance = Mathf.Infinity;

			foreach(Collider detectedObject in detectedObjects)
            {
				float distance = Vector3.Distance(agent.transform.position, detectedObject.transform.position);

				if(distance < shortestDistance)
                {
					shortestDistance = distance;
					nearestObject = detectedObject.gameObject;
                }
            }

			if (nearestObject != null) target.value = nearestObject;

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