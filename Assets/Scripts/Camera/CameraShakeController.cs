using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

namespace Camera
{
    public class CameraShakeController : MonoBehaviour
    {
        public static CameraShakeController Instance;

        [Header("Câmera principal (State-Driven ou Virtual)")]
        public CinemachineCamera mainCamera;

        [Header("Configuração do Shake")]
        public float shakeDuration = 0.2f;
        public float shakeAmplitude = 2f;
        public float shakeFrequency = 2f;

        private CinemachineBasicMultiChannelPerlin noise;
        private float defaultAmplitude;
        private float defaultFrequency;

        private void Awake()
        {
            Instance = this;

            // Pega o componente de ruído da câmera
            noise = mainCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();

            if (noise != null)
            {
                defaultAmplitude = noise.AmplitudeGain;
                defaultFrequency = noise.FrequencyGain;
            }
        }

        public void Shake()
        {
            if (noise != null)
                StartCoroutine(ShakeRoutine());
        }

        private IEnumerator ShakeRoutine()
        {
            noise.AmplitudeGain = shakeAmplitude;
            noise.FrequencyGain = shakeFrequency;

            yield return new WaitForSeconds(shakeDuration);

            noise.AmplitudeGain = defaultAmplitude;
            noise.FrequencyGain = defaultFrequency;
        }
    }
}