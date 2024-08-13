using RMC.Optimizations.Shared;
using UnityEngine;

namespace RMC.Optimizations.Demos.Demo_StaticBatching
{
    /// <summary>
    /// Demo: Occulsion Culling
    ///
    /// Docs: https://docs.unity3d.com/Manual/OcclusionCulling.html
    ///
    /// About: Occlusion culling is a process which prevents Unity from performing
    /// rendering calculations for GameObjects that are completely hidden from
    /// view (occluded) by other GameObjects.
    /// 
    /// FRUSTRUM CULLING - Render only what's in the camera's frustrum
    ///
    /// OCCLUSION CULLING - Render only what's NOT BLOCKED AND in the camera's frustrum
    ///
    /// </summary>
    public class Demo_OcculsionCulling : BaseDemo
    {
        //  Events ----------------------------------------


        //  Properties ------------------------------------


        //  Fields ----------------------------------------
        [Header("Settings")]
        [SerializeField]
        private Renderer _occluder;
        
        [SerializeField]
        private bool _isCullingEnabled;

        //  Unity Methods ---------------------------------
        protected override void OnValidate()
        {
            base.OnValidate();

            if (Application.isPlaying)
            {
                return;
            }

            // Check in edit mode
            CheckSettings();
        }

        
        protected override void Awake()
        {
            base.Awake();
            logDemoInstructions ($"Welcome to {this.GetType().Name}"!);

        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();
            
            // Check in edit mode
            CheckSettings();
        }


        //  Methods ---------------------------------------
        protected void CheckSettings()
        {
            if (_occluder != null)
            {
                if (_occluder.enabled != _isCullingEnabled)
                {
                    _occluder.enabled = _isCullingEnabled;
                    OcclusionCullingCompute();
                }
            }
        }
        
        protected void OcclusionCullingCompute()
        {
#if UNITY_EDITOR
            //This is meant for editor only
            //But for demo's sake, we do it at runtime 
            //To showcase the differences
            UnityEditor.StaticOcclusionCulling.Compute();
#endif
        }


        //  Event Handlers --------------------------------
    }
}