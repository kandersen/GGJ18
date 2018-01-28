using System.Collections.Generic;
using UnityEngine;

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
}
