using Core;
using UnityEngine;
using Managers;
using UnityEngine.UI;

namespace UI
{
    public class UIMainGame : ScreenBase
    {
        [SerializeField]private Camera UICamera;

        [Header("Panels")]
        public RectTransform panelTop;
        public RectTransform panelSpin;
        public RectTransform panelBalance;

        [Header("UI Elements")]
        public Button SpinButton;

        private void Awake()
        {
            UIManager.OnScreenOrientation += OnScreenOrientation;
            EventsController.AddListener(EventsType.OnSpinStarted,OnSpinStart);
            EventsController.AddListener(EventsType.OnSpinEnded,OnSpinEnd);
            if (UICamera == null) UICamera = Camera.main;
            OnScreenOrientation(Screen.orientation);
        }

        private void OnSpinEnd()
        {
            SpinButton.interactable = true;
        }

        private void OnSpinStart()
        {
            SpinButton.interactable = false;
        }

        private void OnDestroy()
        {
            UIManager.OnScreenOrientation -= OnScreenOrientation;
            EventsController.RemoveListener(EventsType.OnSpinStarted,OnSpinStart);
            EventsController.RemoveListener(EventsType.OnSpinEnded,OnSpinEnd);
        }

        [EnumAction(typeof(EventsType))]
        public void OnButtonClick(int eventType)
        {
            EventsController.Broadcast((EventsType)eventType);
        }

        private void OnScreenOrientation(ScreenOrientation orientation)
        {
            Debug.Log("[UIMainGame] OnScreenOrientation = " + orientation);
            panelSpin.anchoredPosition = Vector2.zero;
            panelBalance.anchoredPosition = Vector2.zero;

            if (orientation == ScreenOrientation.Landscape)
            {
                UICamera.fieldOfView = 60;
                panelTop.anchoredPosition = Vector2.zero;

                panelSpin.anchorMin = new Vector2(1, 0.5f);
                panelSpin.anchorMax = new Vector2(1, 0.5f);
                panelSpin.eulerAngles = new Vector3(0, 0, 90);

                panelBalance.anchorMin = new Vector2(0.5f, 0);
                panelBalance.anchorMax = new Vector2(0.5f, 0);
                panelBalance.pivot = new Vector2(0.5f, 0);
            }
            else
            {
                UICamera.fieldOfView = 70;
                panelTop.anchoredPosition = new Vector2(0, -90);

                panelSpin.anchorMin = new Vector2(0.5f, 0);
                panelSpin.anchorMax = new Vector2(0.5f, 0);
                panelSpin.eulerAngles = new Vector3(0, 0, 0);

                panelBalance.anchorMin = new Vector2(0.5f, 1);
                panelBalance.anchorMax = new Vector2(0.5f, 1);
                panelBalance.pivot = new Vector2(0.5f, 1);
            }

            float ratio = Screen.width / (float)Screen.height;
            if (ratio <= 0.47f)
            {
                _canvasSafeArea.localScale = new Vector2(0.95f, 0.95f);
            }
            else
            {
                _canvasSafeArea.localScale = new Vector2(1, 1);
            }
        }
    }
}
