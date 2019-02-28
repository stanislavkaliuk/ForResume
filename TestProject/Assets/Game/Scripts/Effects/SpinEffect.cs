using System.Collections;
using Core;
using Managers;
using Tools;
using UnityEngine;

namespace Effects
{
    public class SpinEffect : IEffect
    {
        private readonly Column _column;
        private Transform _transform;
        private Vector3[] SymbolPos;
        public int[] FinalValues { get; set; }
        private readonly int ColumnIndex;
        private bool _isSpin = false;
        private bool _inStop = false;
        private float _speed;
        private float minRotateDistance;
        private int _stopOffest = -2;
        private int _spinCount = 0; 

        public IGame ThisGame { get; set; }
        public SpinEffect(IGame game, Column column, int index)
        {
            ThisGame = game;
            _column = column;
            _transform = column.ColumnTransform;
            ColumnIndex = index;
            FinalValues = new int[column.ColumnData.Length];
            EventsController.AddListener(EventsType.OnSpinCall,StartEffect);
        }

        private void StartEffect()
        {
            EventsController.Broadcast(EventsType.OnSpinStarted);
            EventsController.Broadcast(EventsType.OnSpinProcessing);
            _speed = 0.0f;
            minRotateDistance = 10;
            _stopOffest = -2;
            Vector3 vPos = _transform.localPosition;
            vPos.y = 0;
            _spinCount = 0;
            _transform.localPosition = vPos;
            _isSpin = true;
            _inStop = false;
        }

        public void OnUpdate()
        {        
            if (!_isSpin)
            {
                return;
            }

            float dt = Mathf.Min(Time.deltaTime, 0.1f);
            Vector3 vPos = _transform.localPosition;
            if (_inStop && minRotateDistance <= 0f)
            {
                if(!ThisGame.SpriteSource.IsStandardActive())
                    ThisGame.SpriteSource.SwitchTarget(((ResourceManager) ThisGame.SpriteSource).Container);
                _speed -= 5 * dt;
                float delta = _speed * dt;
                if (_stopOffest < 3)
                {
                    vPos.y -= delta;
                    if (vPos.y < -1.75f)
                    {
                        vPos.y += 1.75f;
                        if (_stopOffest >= -1)
                        {
                            ShiftSymbolsDown(_stopOffest<2?FinalValues[_stopOffest+1]:GetRandom());
                        }
                        else
                        {
                            ShiftSymbolsDown(GetRandom());
                        }

                        _stopOffest++;
                    }    
                }
                else
                {

                    if ((int)vPos.y != 0)
                    {
                        vPos.y = Mathf.Lerp(vPos.y, 0.0f, dt);
                    }
                    else
                    {
                        vPos.y = 0.0f;
                        _inStop = false;
                        _isSpin = false;
                    }
                }
            }
            else
            {
                if (_spinCount > 3 && ThisGame.SpriteSource.IsStandardActive())
                {
                    ThisGame.SpriteSource.SwitchTarget(((ResourceManager) ThisGame.SpriteSource).EffectSpriteContainer);
                }
                _speed += 70 * dt;//todo configure acceleration from resource file
                _speed = Mathf.Clamp(_speed, 0, 50);//todo configure max speed
                float delta = _speed * dt;
                vPos.y -= delta;
                if (_inStop)
                {
                    minRotateDistance -= delta;
                    _speed -= 5 * dt;
                }

                if (vPos.y <= -1.7f)
                {
                    vPos.y = 0;
                    ShiftSymbolsDown(GetRandom());
                }
            }

            _transform.localPosition = vPos;
        }
        
        public int GetRandom() {
            return Random.Range(0,ThisGame.SpriteSource.Count());
        }

        private void ShiftSymbolsDown(int addVal)
        {
            for (int i = 0; i < _column.ColumnData.Length; i++)//3 symbols for game 1 reserved
            {
                if (i == _column.ColumnData.Length - 1)//top symbol. unseen by default
                {
                    _column.ColumnData[i].sprite = ThisGame.SpriteSource.GetItem(addVal);
                }
                else
                {
                    _column.ColumnData[i].sprite = _column.ColumnData[i + 1].sprite;
                }
            }

            _spinCount++;
        }



        public void LaunchEffect<T>(params T[] data)
        {
            StartEffect();
        }

        public void Stop()
        {
            _inStop = true;
        }

        public bool IsComplete()
        {
            return !_inStop && !_isSpin;
        }
    }
}