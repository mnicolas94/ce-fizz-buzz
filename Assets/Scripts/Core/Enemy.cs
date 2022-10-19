using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    /// <summary>
    /// The core enemy data.
    /// </summary>
    [Serializable]
    public class Enemy
    {
        [SerializeField] private Vector2 _position;
        
        /// <summary>
        /// The classes (dumb, fizz, buzz, fizzbuzz) to which an enemy has belonged, as integers.
        /// </summary>
        [SerializeField] private List<int> _classes;

        public Vector2 Position
        {
            get => _position;
            set => _position = value;
        }

        public EnemyClass CurrentClass => EnemyClassUtils.GetClassFromInt(_classes[_classes.Count - 1]);

        public int Score => _classes[_classes.Count - 1];

        public Enemy(Vector2 position, int firstClass)
        {
            _position = position;
            _classes = new List<int>{ firstClass };
        }

        public void ChangeClass(int newClass)
        {
            _classes.Add(newClass);
        }
    }
}