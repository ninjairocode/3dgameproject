using System.Collections.Generic;
using UnityEngine;
using EBAC.Core.Singleton;

namespace Cloth
{
    public enum ClothType
    { 
        NORMAL,
        SPEED,
        RESIST,
        POWER
    }
    public class ClothManager : Singleton<ClothManager>
    {
        public List<ClothSetup> clothSetups;
        
        public ClothSetup GetSetup(ClothType clothType)
        {
            return clothSetups.Find(i => i.clothType == clothType);
        }
    }
    
    [System.Serializable]
    public class ClothSetup
    {
        public ClothType clothType;
        public Texture2D text;
    }
}
