using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Scripts
{
    /// The implementation of this class needs the currentValue and upperBound parameters
    /// Displayed as : currentValue / upperBound

    public class GenericProgressBar : MonoBehaviour
    {
        private GameObject barForeground;
        private GameObject barLabel;
        private Vector2 barSize;
        private float currentValue = 0;
        private float upperBound = 0;

        public Color barColor;

        void Awake()
        {
            barSize = gameObject.transform.GetChild(0).gameObject.GetComponent<RectTransform>().sizeDelta;
            barForeground = gameObject.transform.GetChild(1).gameObject;
            barLabel = gameObject.transform.GetChild(2).gameObject;

            barForeground.GetComponent<RawImage>().color = barColor;
        }

        public void setValues(float current, float upper)
        {
            currentValue = current;
            upperBound = upper;
        }

        public void updateCurrent(float newCurrent)
        {
            currentValue = newCurrent;
        }

        public void updateUpper(float newUpper)
        {
            upperBound = newUpper;
        }

        public void renderValues()
        {
            barLabel.GetComponent<Text>().text = currentValue.ToString("0.0") + " / " + upperBound.ToString("0.0");
            barForeground.GetComponent<RectTransform>().offsetMax = new Vector2( -(barSize.x - (barSize.x * (currentValue / upperBound))), barSize.y / 2);
        }

    }
}
