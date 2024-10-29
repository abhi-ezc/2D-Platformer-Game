using System;
using UnityEngine;

namespace Sounds
{
    [Serializable]
    public class SoundType
    {
        public AudioClip soundClip;
        public ESound soundType;
    }

    public enum ESound
    {
        ButtonClick,
        PlayerMove,
        PlayerDeath,
        EnemyDeath,
        Music,
        Victory
    }
}
