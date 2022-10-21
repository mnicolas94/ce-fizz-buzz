using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;
using UnityEngine.Pool;

namespace View
{
    public class EnemyViewPool : MonoBehaviour
    {
        [SerializeField] private EnemyView _enemyViewPrefab;

        private Dictionary<Enemy, EnemyView> _dataToViewMap;
        private ObjectPool<EnemyView> _enemiesPool;
        
        private void Start()
        {
            _dataToViewMap = new Dictionary<Enemy, EnemyView>();
            _enemiesPool = new ObjectPool<EnemyView>(
                () => Instantiate(_enemyViewPrefab, transform),
                view => view.gameObject.SetActive(true),
                view => view.gameObject.SetActive(false)
            );
        }

        public EnemyView GetView(Enemy enemy)
        {
            EnemyView view;
            if (_dataToViewMap.ContainsKey(enemy))
            {
                view = _dataToViewMap[enemy];
            }
            else
            {
                view = _enemiesPool.Get();
                view.Initialize(enemy);
                _dataToViewMap.Add(enemy, view);
            }

            return view;
        }
        
        public List<EnemyView> GetViews()
        {
            return _dataToViewMap.Values.ToList();
        }

        public void RemoveView(Enemy enemy)
        {
            if (_dataToViewMap.ContainsKey(enemy))
            {
                var view = _dataToViewMap[enemy];
                _enemiesPool.Release(view);
            }
        }

        public void Clear()
        {
            foreach (var enemy in _dataToViewMap.Keys)
            {
                var view = _dataToViewMap[enemy];
                _enemiesPool.Release(view);
            }
            
            _dataToViewMap.Clear();
        }
    }
}