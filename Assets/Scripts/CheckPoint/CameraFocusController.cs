using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

namespace CheckPoint
{
    public class CameraFocusController : MonoBehaviour
    {
        public static CameraFocusController Instance;

        [Header("Câmera que foca no boss")]
        public GameObject bossCameraObject;
        private CinemachineCamera bossCamera;

        [Header("State Driven Camera principal")]
        public CinemachineStateDrivenCamera stateDrivenCamera;

        [Header("Tempo focando no boss")]
        public float focusTime = 3f;

        private void Awake()
        {
            Instance = this;

            
            if (bossCameraObject != null)
                bossCameraObject.SetActive(false);

            bossCamera = bossCameraObject.GetComponent<CinemachineCamera>();
        }

        public void FocusOnBoss()
        {
            StartCoroutine(FocusRoutine());
        }

        private IEnumerator FocusRoutine()
        {
            
            bossCameraObject.SetActive(true);

            
            bossCamera.Priority = 20;

            
            stateDrivenCamera.Priority = 0;

            yield return new WaitForSeconds(focusTime);

            
            bossCameraObject.SetActive(false);

            
            stateDrivenCamera.Priority = 20;
        }
    }
}