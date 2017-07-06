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

        public void renderDamageOnPlayer(Player player, float damage)
        {
            GameObject canvas = GameObject.Find("Canvas");
            GameObject textGO = new GameObject();
            Text text = textGO.AddComponent<Text>();
            text.text = damage.ToString("0");
            text.transform.position = GameObject.Find("Main Camera").GetComponent<Camera>().
                                                 WorldToScreenPoint(player.transform.position);
            renderTextDamage(text);
        }
        public void renderDamageOnEnemy(Enemy enemy)
        {

        }

        private IEnumerator renderTextDamage(Text text)
        {
            for (float i = 1f; i > 0; i -= 0.1f)
            {
                text.transform.position = Vector3.Lerp(text.transform.position,
                                                                       new Vector3(0, text.transform.position.y + 10, 0),
                                                                       Time.time);
                text.color = new Color(255, 0, 0, i);
                yield return null;
            }

            Destroy(text);
        } 
    }
}