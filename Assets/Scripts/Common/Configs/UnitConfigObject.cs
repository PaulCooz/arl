﻿using UnityEngine;

namespace Common.Configs
{
    [CreateAssetMenu]
    public class UnitConfigObject : ScriptableObject
    {
        public GunConfigObject gunConfig;
        public BulletConfigObject bulletConfig;

        public int health;
        public float speed;
        public string onHpChange;
        public string onDie;
        public int levelExp;
        public int spawnChance;
        public GameObject prefab;

        public bool isPlayer;
    }
}