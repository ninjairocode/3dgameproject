using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using EBAC.Core.Singleton;
using Player;
using Item;
using Bag;

namespace Save
{
    
    [Serializable]
    public class SaveSetup
    {
        
        public float playerPosX;
        public float playerPosY;
        public float playerPosZ;
        public float playerLife;

        
        public int coins;

       
        [Serializable]
        public class BagItemData
        {
            public string itemID;
            public int quantity;
            public float cooldownTimer;
        }

        public List<BagItemData> bagItems = new List<BagItemData>();

       
        public List<string> openedChests = new List<string>();
        public List<string> defeatedEnemies = new List<string>();
    }

   
    public class SaveManager : Singleton<SaveManager>
    {
        private SaveSetup _currentSave;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }

        private string GetPath()
        {
            return Path.Combine(Application.persistentDataPath, "save.json");
        }

        
        public void SaveGame()
        {
            _currentSave = new SaveSetup();

            
            var player = FindObjectOfType<PlayerController>();
            if (player != null)
            {
                _currentSave.playerPosX = player.transform.position.x;
                _currentSave.playerPosY = player.transform.position.y;
                _currentSave.playerPosZ = player.transform.position.z;
                _currentSave.playerLife = player.currentLife;
            }

            
            if (ItemManager.Instance != null)
                _currentSave.coins = ItemManager.Instance.coinsQuantity;

            
            var bagItems = GameObject.FindObjectsOfType<BagItemUI>();
            foreach (var item in bagItems)
            {
                var data = new SaveSetup.BagItemData
                {
                    itemID = item.itemID,
                    quantity = item.quantity,
                    cooldownTimer = item.GetCooldownTimer()
                };

                _currentSave.bagItems.Add(data);
            }

            
            _currentSave.openedChests = GameWorldState.Instance.openedChests;
            _currentSave.defeatedEnemies = GameWorldState.Instance.defeatedEnemies;

           
            string json = JsonUtility.ToJson(_currentSave, true);
            File.WriteAllText(GetPath(), json);

            Debug.Log("Jogo salvo em: " + GetPath());
        }

       
        public void LoadGame()
        {
            string path = GetPath();

            if (!File.Exists(path))
            {
                Debug.LogWarning("Nenhum save encontrado!");
                return;
            }

            string json = File.ReadAllText(path);
            _currentSave = JsonUtility.FromJson<SaveSetup>(json);

            
            var player = FindObjectOfType<PlayerController>();
            if (player != null)
                player.ApplySave(_currentSave);

            
            if (ItemManager.Instance != null)
            {
                ItemManager.Instance.coinsQuantity = _currentSave.coins;
                ItemManager.Instance.ForceUpdateUI();
            }

            
            var bagItems = GameObject.FindObjectsOfType<BagItemUI>();
            foreach (var item in bagItems)
            {
                var data = _currentSave.bagItems.Find(x => x.itemID == item.itemID);
                if (data != null)
                    item.ApplySave(data.quantity, data.cooldownTimer);
            }

            
            GameWorldState.Instance.RestoreWorldState(_currentSave);

            Debug.Log("Jogo carregado!");
        }
    }
}