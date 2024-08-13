using System.Collections.Generic;
using Tayx.Graphy;
using UnityEngine;
using UnityEngine.Rendering;

namespace RMC.Optimizations.Shared
{
    /// <summary>
    /// Replace with comments...
    /// </summary>
    public class Common : MonoBehaviour
    {
        //  Events ----------------------------------------


        //  Properties ------------------------------------
        public UIView UIView { get { return _uiView; } }

        public GraphyManager GraphyManager { get { return _graphyManager; } }

        public bool Has3DCamera
        {
            get { return _has3DCamera; }
            set
            {
                _has3DCamera = value;

                //reset old
                HasCameraMouse = false;
                
                //set new
                _camera3D.gameObject.SetActive(_has3DCamera);
                _camera2D.gameObject.SetActive(!_has3DCamera);
            }
        }
        
        public CameraCustom ActiveCamera
        {
            get
            {
                if (Has3DCamera)
                {
                    return _camera3D;
                }
                else
                {
                    return _camera2D;
                }
            }
        }
        
        public bool hasPostProcessing
        {
            get { return _hasPostProcessing; }
            set
            {
                _hasPostProcessing = value;
                _postProcessing.enabled = _hasPostProcessing;
            }
        }
        
        public bool HasLights
        {
            get { return _hasLights; }
            set
            {
                _hasLights = value;
                foreach (Light nextLight in _lights)
                {
                    nextLight.enabled = _hasLights;
                }
            }
        }

        public bool CanCameraMouseSupport {
            get
            {
                return ActiveCamera.HasFreeCamera;
            }
        }
        public bool HasCameraMouse {
            get
            {
                return _hasCameraMouse;
            }
            set
            {
                _hasCameraMouse = value;
                if (ActiveCamera.HasFreeCamera)
                {
                    ActiveCamera.FreeFlyCamera.enabled = _hasCameraMouse;
                }
            }
        }

        //  Fields ----------------------------------------
        [Header("Scene References")]
        [SerializeField]
        private UIView _uiView;

        [SerializeField]
        private CameraCustom _camera2D;

        [SerializeField]
        private CameraCustom _camera3D;

        [SerializeField]
        private Volume _postProcessing;

        [SerializeField]
        private GraphyManager _graphyManager;

        [SerializeField]
        private List<Light> _lights;

        private bool _has3DCamera = true;
        private bool _hasPostProcessing = true;
        private bool _hasLights = true;
        private bool _hasCameraMouse = false;
        
        //  Unity Methods ---------------------------------

        //  Methods ---------------------------------------

        //  Event Handlers --------------------------------
    }
}