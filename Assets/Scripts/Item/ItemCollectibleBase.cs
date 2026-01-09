using UnityEngine;

namespace Item
{
    public class ItemCollectibleBase : MonoBehaviour
    {
        
        public string compareTag = "Player";
        //public ParticleSystem particles;
        //public AudioSource audioSource;

        private void Awake()
        {
            //if (particles != null) particles.transform.SetParent(null);
        }
        

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.transform.CompareTag(compareTag))
            {
                Collect();
            }
        }
        
        protected virtual void Collect()
        {
            Debug.Log("Collect");
            OnCollect();
            gameObject.SetActive(false);
            
        }

        protected virtual void OnCollect()
        {
            //if (particles != null) particles.Play();
            //if (audioSource != null) audioSource.Play();
        }
    }
}
