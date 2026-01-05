using System;
using DG.Tweening;
using UnityEngine;

namespace Utils
{
    public class FlashColor : MonoBehaviour
    {
        public MeshRenderer meshRenderer;
        public SkinnedMeshRenderer skinnedMeshRenderer;
        
        [Header("Flash Settings")] 
        public Color color = Color.red;
        public float duration = .2f;
        
        private Color defaultColor;
        private Tween _currentTween;

        private void OnValidate()
        {
            if (meshRenderer == null) meshRenderer = GetComponent<MeshRenderer>();
            if (skinnedMeshRenderer == null) skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        }

        // private void Start()
        // {
        //     defaultColor = meshRenderer.material.GetColor("_EmissionColor");
        // }


        [NaughtyAttributes.Button]
        public void Flash()
        {
            if (meshRenderer != null && !_currentTween.IsActive()) 
                _currentTween = meshRenderer.material.DOColor(color, "_EmissionColor", duration).SetLoops(2, LoopType.Yoyo);
            
            if (skinnedMeshRenderer != null && !_currentTween.IsActive()) 
                _currentTween = skinnedMeshRenderer.material.DOColor(color, "_EmissionColor", duration).SetLoops(2, LoopType.Yoyo);
        }
    }
}
