using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{
    public class ClothChanger : MonoBehaviour
    {
        public List<SkinnedMeshRenderer> meshes;
        public string shaderIdName = "_EmissionMap";

        public void ChangeCloth(ClothType type)
        {
            var setup = ClothManager.Instance.GetSetup(type);

            if (setup == null)
            {
                Debug.LogWarning("ClothSetup não encontrado para: " + type);
                return;
            }

            foreach (var mesh in meshes)
            {
                if (mesh != null)
                    mesh.materials[0].SetTexture(shaderIdName, setup.text);
            }
        }
    }
}