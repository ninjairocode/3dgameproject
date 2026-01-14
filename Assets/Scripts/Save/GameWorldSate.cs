using System.Collections.Generic;
using EBAC.Core.Singleton;
using Enemy;
using Item;
using UnityEngine;


namespace Save
{
    public class GameWorldState : Singleton<GameWorldState>
    {
        [Header("Estados do Mundo")]
        public List<string> openedChests = new List<string>();
        public List<string> defeatedEnemies = new List<string>();


       

        public void RegisterChestOpened(string id)
        {
            if (!string.IsNullOrEmpty(id) && !openedChests.Contains(id))
                openedChests.Add(id);
        }

        public void RegisterEnemyDefeated(string id)
        {
            if (!string.IsNullOrEmpty(id) && !defeatedEnemies.Contains(id))
                defeatedEnemies.Add(id);
        }


      

        public void RestoreWorldState(SaveSetup save)
        {
            if (save == null)
            {
                Debug.LogWarning("SaveSetup nulo ao restaurar estado do mundo.");
                return;
            }

            
            openedChests = save.openedChests ?? new List<string>();
            defeatedEnemies = save.defeatedEnemies ?? new List<string>();


           
            foreach (var chest in FindObjectsOfType<ChestController>())
            {
                bool wasOpened = openedChests.Contains(chest.chestID);
                chest.ApplySavedState(wasOpened);
            }


            
            foreach (var enemy in FindObjectsOfType<EnemyBase>())
            {
                bool wasDefeated = defeatedEnemies.Contains(enemy.enemyID);
                enemy.ApplySavedState(wasDefeated);
            }

            Debug.Log("Estado do mundo restaurado com sucesso.");
        }
    }
}