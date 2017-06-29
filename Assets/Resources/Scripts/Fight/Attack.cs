using UnityEngine;

namespace Assets.Resources.Scripts
{
    public abstract class Attack : MonoBehaviour
    {
        public GameObject AttackSprite { get; set; }
        public abstract float calculateDamage();
        public virtual void castAttackOnPlayer(Player player) { }

        public virtual void castAttackOnEnemy(Enemy enemy) { }
    }
}