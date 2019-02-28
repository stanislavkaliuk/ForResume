using UnityEngine;
using UnityEngine.UI;

namespace UI
{
#if UNITY_EDITOR
    [ExecuteInEditMode]
#endif
    public abstract class WindowBase : MonoBehaviour, IWindow
    {
        public WindowBase()
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

        [SerializeField] private RectTransform _canvasSafeArea;
        [SerializeField]private Canvas _canvas;
        public Canvas canvas
        {
            get
            {
                if (_canvas == null) _canvas = GetComponent<Canvas>();
                return _canvas;
            }
        }

        public void Open()
        {
            if (!canvas.isActiveAndEnabled) canvas.enabled = true;
        }

        public void Close()
        {
            if (canvas.isActiveAndEnabled) canvas.enabled = false;
        }

#if UNITY_EDITOR
        void Awake()
        {
            if (!gameObject.tag.Equals(TagContainer.IWindowTag))
            {
                gameObject.tag = TagContainer.IWindowTag;
                Debug.Log(gameObject.name +": Tag changed to "+ gameObject.tag);
            }
        }
#endif
    }
}
