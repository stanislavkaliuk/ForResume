using System;
using Managers;
using UnityEngine;

namespace Tools
{
    [CreateAssetMenu(fileName = "SpriteContainer", menuName = "Cleopatra/Sprite Container",order = 150)]
    public class SpriteContainer : ScriptableObject,IContainer<Sprite>
    {
        public Sprite[] Sprites;

        public int Count => Sprites.Length;

        public Sprite GetItem(int index)
        {
            if (index >= Count)
            {
                Debug.LogError("[Sprite Container] Can't get sprite. Index out of range");
                throw new ArgumentOutOfRangeException();
            }

            return Sprites[index];
        }
    }
}