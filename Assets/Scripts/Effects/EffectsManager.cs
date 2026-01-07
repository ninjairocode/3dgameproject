using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Effects
{
    public class EffectsManager : MonoBehaviour
    {
        public static EffectsManager Instance;

        [Header("Global Volume")]
        public Volume globalVolume;

        
        private ColorAdjustments colorAdjust;
        private Vignette vignette;

        [Header("Configurações")]
        public float flashDuration = 0.2f;
        public float vignetteIntensityOnDamage = 0.45f;

        private void Awake()
        {
            Instance = this;

            
            globalVolume.profile.TryGet(out colorAdjust);
            globalVolume.profile.TryGet(out vignette);
        }

       
        public void DamageFlash()
        {
            StopAllCoroutines();
            StartCoroutine(DamageFlashRoutine());
        }

        private IEnumerator DamageFlashRoutine()
        {
            
            vignette.color.Override(Color.red);
            vignette.intensity.Override(vignetteIntensityOnDamage);

            yield return new WaitForSeconds(flashDuration);

          
            vignette.intensity.Override(0f);
        }

        
        public void DeathEffect()
        {
            StartCoroutine(DeathRoutine());
        }
        
        public void ResetEffects()
        {
            
            colorAdjust.saturation.Override(0f);

            
            vignette.intensity.Override(0f);
        }

        private IEnumerator DeathRoutine()
        {
            float t = 0f;

            while (t < 1f)
            {
                t += Time.deltaTime;

               
                colorAdjust.saturation.Override(Mathf.Lerp(0f, -100f, t));

                yield return null;
            }
        }
    }
}