using UnityEngine;

namespace Assets.Resources.Scripts
{
    public abstract class Attack : MonoBehaviour
    {
        public abstract float calculateDamage();
        public abstract void castAttackOnPlayer(Player player);
    }
}