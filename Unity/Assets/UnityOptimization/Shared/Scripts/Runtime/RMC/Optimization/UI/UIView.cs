using UnityEngine;
using UnityEngine.UIElements;

namespace RMC.Optimizations.Shared
{
    /// <summary>
    /// Replace with comments...
    /// </summary>
    public class UIView : MonoBehaviour
    {
        //  Events ----------------------------------------


        //  Properties ------------------------------------
        public Label TitleLabel { get { return _uiDocument?.rootVisualElement?.Q<Label>("TitleLabel"); } }


        //  Fields ----------------------------------------
        [SerializeField]
        private UIDocument _uiDocument;


        //  Unity Methods ---------------------------------

        
        //  Methods ---------------------------------------

        
        //  Event Handlers --------------------------------
    }
}