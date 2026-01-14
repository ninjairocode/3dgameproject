using UnityEngine;

namespace Save
{
    public class GameLoader : MonoBehaviour
    {
        public static bool loadOnStart = false;

        private void Start()
        {
            if (loadOnStart)
            {
                loadOnStart = false;
                SaveManager.Instance.LoadGame();
            }
        }
    }
}
