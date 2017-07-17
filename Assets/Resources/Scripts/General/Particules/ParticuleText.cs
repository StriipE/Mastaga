using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParticuleText
{
    GameObject gameObject;
    private Text text;
    private float timer = 0f;
    private float maxTimer = 3.0f;
    private const float TEXT_SPEED = 0.6f;

    public ParticuleText(GameObject target, string text, float timerValue)
    {
        this.maxTimer = timerValue;
        GameObject canvas = GameObject.Find("NPData");
        gameObject = new GameObject(text);

        gameObject.transform.SetParent(canvas.transform);

        this.text = gameObject.AddComponent<Text>();
        this.text.text = text;
        this.text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        this.text.fontSize = 13;
        this.text.color = Color.blue;
        this.text.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 20);

        this.text.transform.position = GameObject.Find("Main Camera").GetComponent<Camera>().
                                             WorldToScreenPoint(target.transform.position +
                                             new Vector3(0, 1.5f, 0)); // Offsets texts over the damaged target
    }

	public void Update () {
        timer += Time.deltaTime;
        if (timer > maxTimer)
        {
            GameObject.Destroy(gameObject);
        }
        else
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1 - (timer / maxTimer) );
            text.transform.position = Vector3.Lerp(text.transform.position,
                           new Vector3(text.transform.position.x, text.transform.position.y + TEXT_SPEED, 0),
                           Time.time);
        }
    }

    public bool isOver()
    {
        return timer > maxTimer;
    }
}
