using Core;
using Games.MainGame;
using Managers;
using Tools;
using UnityEngine;


 public class MainGame : IGame
 {
     //game should contain
     //1. ref to sprites
     //2. ref to effects/animations
     //3. ref to sprite renders
     //4. ref to maps
     public MainGame()
     {
         EventsController.AddListener(EventsType.OnSpinProcessing, Spin);      
     }

     public SpriteRenderer[,] Renderers { get; set; }
     public Map CurrentMap { get; set; }
     public GameContext Context { get; set; }
     public IMultipleResourceManager<Sprite> SpriteSource { get; set; }
     public IResourceManager<Map> MapSource { get; set; }
     
     public IEffect[] SpinEffect { get; set; }

     private float _SpinAge;
     private int _activeReel = -1;
     private bool _isSpin;
     public void Spin()
     {
         _isSpin = true;
         _SpinAge = 0.0f;
         _activeReel = -1;
         float val = CleopatraRandom.Value;
         int index = Mathf.FloorToInt(val * (MapSource.Count() - 1));
         CurrentMap = MapSource.GetItem(index);
         byte offset = (byte)Random.Range(0, SpriteSource.Count());
         for (int i = 0; i < CurrentMap.Data.Length; i++)
         {
             CurrentMap.Data[i] = (byte) ((CurrentMap.Data[i] + offset) % SpriteSource.Count());
         }

         for (int i = 0; i < SpinEffect.Length; i++)
         {
             int[] value = new int[3];
             for (int j = 0; j < 3; j++)
             {
                 value[j] = CurrentMap.Data[3*j+i];
             }

             SpinEffect[i].FinalValues = value;
         }
     }

     public void Dispose()
     {
         EventsController.RemoveListener(EventsType.OnSpinProcessing, Spin);
     }

     public void OnUpdate()
     {
         if(!_isSpin) return;

         if (_activeReel == -1)
         {
             _SpinAge += Time.deltaTime;
             if (_SpinAge > 2f)
             {
                 _activeReel = 0;
                 SpinEffect[_activeReel].Stop();
             }
         }
         else
         {
             if (SpinEffect[_activeReel].IsComplete())
             {
                 _activeReel++;
                 if (_activeReel >= SpinEffect.Length)
                 {
                     _isSpin = false;
                     //todo check result here
                     EventsController.Broadcast(EventsType.OnSpinEnded);
                     return;
                 }
             }
             else
             {
                 SpinEffect[_activeReel].Stop();
             }
         }
         
         foreach (var effect in SpinEffect)
         {
             effect.OnUpdate();
         }
     }
 }
