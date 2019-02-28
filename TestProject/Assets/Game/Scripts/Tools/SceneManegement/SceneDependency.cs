using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    public class SceneDependency : MonoBehaviour
    {
        public int initSceneNum = 0;

        public List<Object> dependencyScenes = new List<Object>();

#if UNITY_EDITOR
        public void Start()
        {
            if (!SceneManager.isSceneLoaded(initSceneNum))
            {
                StartCoroutine(SceneManager.LoadScene(initSceneNum));
            }
        }
#endif

    }
}
