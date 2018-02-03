using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DebrisParticleSystems : MonoBehaviour
{
	public List<ParticleSystem> DebrisSystems;

	public void Start()
	{
		var systems = GetComponentsInChildren<ParticleSystem>();
		DebrisSystems = new List<ParticleSystem>(systems);
	}

	public void DecrementSpawnRate(float decrement)
	{
		foreach (var system in DebrisSystems)
		{
			var emissions = system.emission;
			emissions.rateOverTime = new ParticleSystem.MinMaxCurve(emissions.rateOverTime.constant - decrement);
		}
	}

	public void SpeedUp(float goal, float delay) {
		foreach (ParticleSystem system in DebrisSystems) {
			DOTween.To(() => system.main.simulationSpeed, x => {var main = system.main; main.simulationSpeed = x;}, goal, delay);
		}
	}

	public void Stop() {
		foreach (var system in DebrisSystems) {
			system.Stop ();
		}
	}

}
