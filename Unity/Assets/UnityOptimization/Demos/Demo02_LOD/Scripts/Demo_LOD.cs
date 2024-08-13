using RMC.Optimizations.Shared;
using UnityEngine;

namespace RMC.Optimizations.Demos.Demo_LOD
{
    /// <summary>
    /// Demo: Level of Detail (LOD) for meshes
    ///
    /// Docs:  https://docs.unity3d.com/Manual/LevelOfDetail.html
    ///
    /// About:  Level of detail (LOD) is a technique that reduces the number of
    /// GPU operations that Unity requires to render distant meshes.
    ///
    /// When a GameObject  in the Scene is far away from the Camera , you see
    /// less detail compared to when the GameObject is close to the Camera.
    /// However, by default, Unity uses the same number of triangles to render
    /// it at both distances. This can result in wasted GPU operations, which
    /// can impact performance in your Scene. The LOD technique allows Unity
    /// to reduce the number of triangles it renders for a GameObject based
    /// on its distance from the Camera. To use it, a GameObject must have a
    /// number of meshes with decreasing levels of detail in its geometry.
    /// These meshes are called LOD levels. The farther a GameObject is
    /// from the Camera, the lower-detail LOD level Unity renders.
    /// This technique reduces the load on the hardware for these distant GameObjects,
    /// and can therefore improve rendering performance.
    ///
    /// </summary>
    public class Demo_LOD : BaseDemo
    {
        //  Events ----------------------------------------


        //  Properties ------------------------------------


        //  Fields ----------------------------------------
        [Header("Settings")]
        [SerializeField]
        private GameObject _model;
        
        [SerializeField]
        private bool _isLODEnabled;


        //  Unity Methods ---------------------------------
        protected override void OnValidate()
        {
            base.OnValidate();

            if (_model.GetComponent<LODGroup>() != null)
            {
                if (_model.GetComponent<LODGroup>().enabled != _isLODEnabled)
                {
                    _model.GetComponent<LODGroup>().enabled = _isLODEnabled;
                }
            }
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
        }


        //  Methods ---------------------------------------


        //  Event Handlers --------------------------------
    }
}