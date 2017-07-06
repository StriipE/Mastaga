using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Assets.Resources.Scripts.Attacks
{
    public class DamageText : MonoBehaviour
    {
        private const float TEXT_SPEED = 1f;

        public Text damageText;

        public void Update()
        {
            damageText.transform.position = Vector3.Lerp(damageText.transform.position,
                           new Vector3(damageText.transform.position.x, damageText.transform.position.y + TEXT_SPEED, 0),
                           Time.time);
        }

        public void renderDamage(GameObject target, float damage)
        {
            GameObject canvas = GameObject.Find("Canvas");
            GameObject damageTextGO = new GameObject("Damage Text Handler");

            damageTextGO.transform.SetParent(canvas.transform);

            damageText = damageTextGO.AddComponent<Text>();
            damageText.text = damage.ToString("0");
            damageText.font = UnityEngine.Resources.GetBuiltinResource<Font>("Arial.ttf");

            damageText.GetComponent<RectTransform>().sizeDelta = new Vector2(20, 20);

            damageText.transform.position = GameObject.Find("Main Camera").GetComponent<Camera>().
                                                 WorldToScreenPoint(target.transform.position + 
                                                 new Vector3(0, 0, target.transform.localScale.y / 1.5f)); // Offsets texts over the damaged target

            StartCoroutine(textFadeout(damageText));
        }

        private IEnumerator textFadeout(Text damageText)
        {
            for (float i = 1f; i > 0; i -= 0.1f)
            {
                damageText.color = new Color(255, 0, 0, i);
                yield return new WaitForSeconds(0.1f);
            }
            Destroy(damageText.gameObject);
            Destroy(this);
        }

        public void OnDestroy()
        {
            if (damageText != null)
            {
                Destroy(damageText.gameObject);
                Destroy(this);
            }
        }
    }
}
