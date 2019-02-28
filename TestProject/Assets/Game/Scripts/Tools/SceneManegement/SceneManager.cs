using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameSceneManager = UnityEngine.SceneManagement.SceneManager;

namespace Tools
{
    public static class SceneManager
    {
        private static Scene LastScene;

        /// <summary>
        /// Loading single scene by build index
        /// </summary>
        /// <param name="index">build index</param>
        /// <returns></returns>
        public static IEnumerator LoadScene(int index)
        {
            var async = GameSceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
            while (!async.isDone)
            {
                yield return null;
            }
            Log("Scene loaded !");
            LastScene = GameSceneManager.GetSceneByBuildIndex(index);
        }

        /// <summary>
        /// Loading scene additive by build index
        /// </summary>
        /// <param name="index">build index</param>
        /// <returns></returns>
        public static IEnumerator AddScene(int index)
        {
            var async = GameSceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
            while (!async.isDone)
            {
                yield return null;
            }
            Log("Scene added!");
            LastScene = GameSceneManager.GetSceneByBuildIndex(index);
        }

        /// <summary>
        /// Loading scene by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IEnumerator AddScene(string name)
        {
            var async =GameSceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
            while (!async.isDone)
            {
                yield return null;
            }
            Log("Scene added!");
            LastScene = GameSceneManager.GetSceneByName(name);
        }

        public static void RemoveScene()
        {
            GameSceneManager.UnloadSceneAsync(LastScene);
        }

        public static Scene GetCurrentScene()
        {
            return LastScene;
        }

        public static bool isSceneLoaded(int index)
        {
            return GameSceneManager.GetSceneByBuildIndex(index).isLoaded;
        }

        private static void Log(string message)
        {
            Debug.Log("[SceneManager] "+ message);
        }
    }
}