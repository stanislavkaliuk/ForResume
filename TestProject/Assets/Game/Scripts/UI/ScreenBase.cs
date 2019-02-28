using UnityEngine;
using UnityEngine.UI;

namespace UI
{
#if UNITY_EDITOR
    [ExecuteInEditMode]
#endif
    public abstract class ScreenBase : MonoBehaviour, IScreen
    {
        public ScreenBase()
        {
            id = _instances;
            _instances++;
        }

        private static int _instances = 0;
        public int id { get; private set; }

        public int GetID()
        {
            return id;
        }

        [SerializeField] protected RectTransform _canvasSafeArea;
        [SerializeField] protected Canvas _canvas;
        public Canvas canvas
        {
            get
            {
                if (_canvas == null) _canvas = GetComponent<Canvas>();
                return _canvas;
            }
        }

        public void Activate()
        {
            if (!canvas.isActiveAndEnabled) canvas.enabled = true;
        }

        public void Deactivate()
        {
            if (canvas.isActiveAndEnabled) canvas.enabled = false;
        }

#if UNITY_EDITOR
        void Awake()
        {
            if (!gameObject.tag.Equals(TagContainer.IScreenTag))
            {
                gameObject.tag = TagContainer.IScreenTag;
                Debug.Log(gameObject.name +": Tag changed to "+ gameObject.tag);
            }
        }
#endif
    }
}
