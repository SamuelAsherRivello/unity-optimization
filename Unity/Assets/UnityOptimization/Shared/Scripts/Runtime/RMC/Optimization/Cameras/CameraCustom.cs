using UnityEngine;

namespace RMC.Optimizations.Shared
{
    /// <summary>
    /// Replace with comments...
    /// </summary>
    public class CameraCustom : MonoBehaviour
    {
        //  Events ----------------------------------------

        //  Properties ------------------------------------
        public Camera Camera { get { return _camera; } }
        public bool Is3D { get { return _camera.orthographic == true; }}
        
        public FreeFlyCamera FreeFlyCamera { get { return _freeCamera;} }
        public bool HasFreeCamera { get { return _freeCamera != null; } }

        //  Fields ----------------------------------------
        [Header("Required References")] 
        [SerializeField]
        private Camera _camera;

        [Header("Optional References")] 
        [SerializeField]
        private FreeFlyCamera _freeCamera;
        
        //  Unity Methods ---------------------------------

        //  Methods ---------------------------------------

        //  Event Handlers --------------------------------
    }
}