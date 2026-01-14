using Save;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utils
{
    public class ChangeScene : MonoBehaviour
    {
        public string trocarCena;

        public void Change()
        {
            SceneManager.LoadScene(trocarCena);
        }

        public void LoadGame()
        {
            GameLoader.loadOnStart = true;
            SceneManager.LoadScene(trocarCena);
        }
    }
}
