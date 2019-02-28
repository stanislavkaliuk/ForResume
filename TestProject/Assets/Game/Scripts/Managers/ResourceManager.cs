using System;
using Games.MainGame;
using Tools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    
    [CreateAssetMenu(fileName = "Resource Manager", menuName = "Cleopatra/Resource Manager", order = 151)]
    public class ResourceManager : ScriptableObject,IMultipleResourceManager<Sprite>,IResourceManager<Map>
    {
        public SpriteContainer Container;
        public SpriteContainer EffectSpriteContainer;
        public MapContainer Maps;

        private IContainer<Sprite> _spriteContainer {get; set;}
        private IContainer<Map> _mapContainer => Maps;

        public void SwitchTarget(IContainer<Sprite> container)
        {
            _spriteContainer = container;
        }

        public bool IsStandardActive()
        {
            return (SpriteContainer) _spriteContainer == Container;
        }


        Sprite IResourceManager<Sprite>.GetRandomItem()
        {         
           return _spriteContainer.GetItem(Random.Range(0, _spriteContainer.Count));
        }

        Map IResourceManager<Map>.GetRandomItem()
        {
            return _mapContainer.GetItem(Random.Range(0, _mapContainer.Count));
        }

        Sprite IResourceManager<Sprite>.GetItem(int index)
        {
            return _spriteContainer.GetItem(index);
        }

        int IResourceManager<Map>.Count()
        {
            return _mapContainer.Count;
        }

        int IResourceManager<Sprite>.Count()
        {
            return _spriteContainer.Count;
        }


        Map IResourceManager<Map>.GetItem(int index)
        {
            return _mapContainer.GetItem(index);
        }

       
    }
}