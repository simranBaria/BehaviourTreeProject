using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace NodeCanvas.Tasks.Actions {

	public class FindAllHeroesAT : ActionTask {

		public CharacterType characterType;
		public BBParameter<List<GameObject>> allHeroes;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			allHeroes.value = new();
			LayerMask layer;
			if (characterType == CharacterType.Ally) layer = LayerMask.GetMask(LayerMask.LayerToName(agent.gameObject.layer));
			else
			{
				if (LayerMask.GetMask(LayerMask.LayerToName(agent.gameObject.layer)) != LayerMask.GetMask("Allies")) layer = LayerMask.GetMask("Allies");
				else layer = LayerMask.GetMask("Enemies");
			}
			Collider[] enemies = Physics.OverlapSphere(agent.transform.position, Mathf.Infinity, layer);

			foreach(Collider enemy in enemies) allHeroes.value.Add(enemy.gameObject);

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