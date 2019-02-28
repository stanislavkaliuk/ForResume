using System.Collections;
using UnityEngine;
using Effects;
using Tools;
using Core;
using UI;

namespace Managers
{

    public class GameManager : SingleMono<GameManager>
    {
        private UIManager _ui;
        public UIManager ui
        {
            get
            {
                if (_ui == null) _ui = Get<UIManager>();
                return _ui;
            }
        }

        public ResourceManager Resources;
        public IGame CurrentGame;


        public IEnumerator Start()
        {
            Debug.Log("Loading...");
            #if UNITY_IOS
            Application.targetFrameRate = 60;
            #endif
            yield return new WaitForSeconds(1.0f);
            ui.Screens.Set<ScreenLoading>();
            yield return StartCoroutine(SceneManager.AddScene(1));

            CurrentGame = new MainGame();
            UpdateManager.Add(CurrentGame);
            CurrentGame.Context = FindObjectOfType<GameContext>();
            CurrentGame.Renderers = CurrentGame.Context.Renderers.To2DimArray(3, 3);
            CurrentGame.MapSource = Resources;
            CurrentGame.SpriteSource = Resources;
            Resources.SwitchTarget(Resources.Container);
            CurrentGame.SpinEffect = new IEffect[3];
            for (int i = 0; i < 3; i++)
            {
                CurrentGame.SpinEffect[i] = new SpinEffect(CurrentGame, CurrentGame.Context.Columns[i], i);
            }

            ui.Screens.Set<UIMainGame>();

            Debug.Log("Started!");
            EventsController.Broadcast(EventsType.OnApplicationStart);
        }

        public void OnApplicationFocus(bool focus)
        {
            EventsController.Broadcast(EventsType.OnApplicationFocus, focus);
        }

        public void OnApplicationQuit()
        {
            EventsController.Broadcast(EventsType.OnApplicationQuit);
        }
    }
}