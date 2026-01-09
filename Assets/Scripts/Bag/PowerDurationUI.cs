using System.Collections;
using TMPro;
using UnityEngine;

namespace Bag
{
    public class PowerDurationUI : MonoBehaviour
    {
        public static PowerDurationUI Instance;

        public TextMeshProUGUI durationText;

        private void Awake()
        {
            Instance = this;
            durationText.text = "";
        }

        public void ShowDuration(float time)
        {
            StopAllCoroutines();
            StartCoroutine(CountDown(time));
        }

        private IEnumerator CountDown(float time)
        {
            while (time > 0)
            {
                durationText.text = "Power: " + Mathf.Ceil(time).ToString() + "s";
                time -= Time.deltaTime;
                yield return null;
            }

            durationText.text = "";
        }
    }
}
