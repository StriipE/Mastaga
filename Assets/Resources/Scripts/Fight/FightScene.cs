using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class FightScene : MonoBehaviour
{
    public static List<DamageText> damageParticles { get; set; }

    public void Start()
    {
        damageParticles = new List<DamageText>();
    }
    public void Update()
    {
        DamageText damageParticlesRemoved = null;
        foreach (DamageText particule in damageParticles)
        {
            if (particule.isOver())
            {
                damageParticlesRemoved = particule;
            }
            particule.Update();
        }
        if (damageParticlesRemoved != null)
        {
            damageParticles.Remove(damageParticlesRemoved);
        }
    }


}

