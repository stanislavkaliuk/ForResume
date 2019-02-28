using System;
using System.Collections.Generic;
using Core;
using Games.MainGame;
using Tools;
using UnityEngine;

namespace Managers
{
    public interface IGame : IUpdate,IDisposable
    {
        SpriteRenderer[,] Renderers { get; set; }
        Map CurrentMap { get; set; }
        GameContext Context { get; set; }

        IMultipleResourceManager<Sprite> SpriteSource { get; set; }
        IResourceManager<Map> MapSource { get; set; }
        
        IEffect[] SpinEffect { get; set; }

        void Spin();

    }
}