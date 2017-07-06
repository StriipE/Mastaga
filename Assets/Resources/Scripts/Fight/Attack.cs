using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

namespace Assets.Resources.Scripts
{
    public abstract class Attack : MonoBehaviour
    {
        public List<GameObject> AttackSprites { get; set; }
        public abstract float calculateDamage();
        public virtual void castAttackOnPlayer(Player player) { }

        public virtual void castAttackOnEnemy(Enemy enemy) { }        
    }
}