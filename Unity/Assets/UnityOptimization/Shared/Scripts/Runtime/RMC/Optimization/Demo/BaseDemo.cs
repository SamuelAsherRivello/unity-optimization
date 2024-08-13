using UnityEngine;

namespace RMC.Optimizations.Shared
{
    public enum CameraMode
    {
        Camera2D,
        Camera3D,
        Camera3DWithMouse
    }
    
    /// <summary>
    /// Replace with comments...
    /// </summary>
    public class BaseDemo : MonoBehaviour
    {
        //  Events ----------------------------------------


        //  Properties ------------------------------------
        protected Common Common { get { return _common; } }


        //  Fields ----------------------------------------
        [Header("Scene References")]
        [SerializeField]
        private Common _common;

        [Header("Settings")]
        
        [SerializeField]
        [Tooltip("If mouse, use 'r' key to reset camera.")]
        private CameraMode _cameraMode = CameraMode.Camera3D;
        private CameraMode _cameraModePrevious;
        
        [SerializeField]
        private bool _hasFPSCounter = true;
        
        [SerializeField]
        private bool _hasPostProcessing = true;
        
        [SerializeField]
        private bool _hasLights = true;
        
        //  Unity Methods ---------------------------------
        protected virtual void OnValidate()
        {
            if (_common == null)
            {
                return;
                
            }
            if (_common?.UIView?.TitleLabel != null)
            {
                _common.UIView.TitleLabel.text = $"{GetType().Name}";
            }
            
            if (_cameraMode != _cameraModePrevious)
            {
                _cameraModePrevious = _cameraMode;
                _common.Has3DCamera = _cameraMode == CameraMode.Camera3D || _cameraMode == CameraMode.Camera3DWithMouse;
                if (_common.CanCameraMouseSupport)
                {
                    _common.HasCameraMouse = _cameraMode == CameraMode.Camera3DWithMouse;
                }
            }
            
            if (_common?.hasPostProcessing != _hasPostProcessing)
            {
                _common.hasPostProcessing = _hasPostProcessing;
            }
            
            if (_common?.HasLights != _hasLights)
            {
                _common.HasLights = _hasLights;
            }
        }
        
        protected virtual void Awake()
        {
            _cameraModePrevious = CameraMode.Camera2D;
            OnValidate();
        }

        protected virtual void Start()
        {
            if (_hasFPSCounter)
            {
                _common.GraphyManager.Enable();
            }
        }

        protected virtual void Update()
        {
            //Debug.Log($"{GetType().Name}.Update()");
        }


        //  Methods ---------------------------------------
        protected void logDemoInstructions(string message)
        {
            Debug.Log($"{message}");
        }

        //  Event Handlers --------------------------------
    }
}