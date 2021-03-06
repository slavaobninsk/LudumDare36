﻿using UnityEngine;
using System.Collections;

public class CosmicEnergy : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public GameObject beamGo;
    public EnergyAgent sourceEnergyAgent;
    public EnergyAgent targetEnergyAgent;

    private Base _playerBase;


	void Start ()
	{
	    _playerBase = FindObjectOfType<Base>();
	    sourceEnergyAgent = GetComponent<EnergyAgent>();
	}

    void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Player on energy column");
            beamGo.gameObject.SetActive(true);
            var playerCom = target.GetComponentInParent<Player>();
            targetEnergyAgent = playerCom.energyAgent;
            if (!playerCom.LastCosmicEnergies.Contains(this))
            {
                if (_playerBase.LastCosmicEnergies.Contains(this))
                {
                    _playerBase.LastCosmicEnergies.Remove(this);
                    _playerBase.TakeEnergy(-1, true);
                }
                playerCom.unitEnergy.TakeEnergy(1);
                playerCom.LastCosmicEnergies.Add(this);
            }
        }
    }

    void Update ()
    {
        if (targetEnergyAgent != null)
        {
            lineRenderer.SetPosition(0, sourceEnergyAgent.transform.position + sourceEnergyAgent.visualOffset);
            lineRenderer.SetPosition(1, targetEnergyAgent.transform.position + targetEnergyAgent.visualOffset);
        }
    }

    public void SetTargetEnergyAgent(EnergyAgent energyAgent)
    {
        targetEnergyAgent = energyAgent;
    }
}
