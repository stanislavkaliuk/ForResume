using System;
using Core;
using Managers;

public interface IEffect : IUpdate
{
    IGame ThisGame { get; set; }
    int[] FinalValues { get; set; }
    void LaunchEffect<T>(params T[] data);
    void Stop();

    bool IsComplete();
}
