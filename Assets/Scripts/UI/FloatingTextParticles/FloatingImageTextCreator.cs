using BrunoMikoski.AnimationSequencer;
using UnityEngine;
using UnityEngine.Pool;

namespace UI.FloatingTextParticles
{
    [CreateAssetMenu(fileName = "FloatingImageTextCreator", menuName = "FloatingImageTextCreator", order = 0)]
    public class FloatingImageTextCreator : ScriptableObject
    {
        [SerializeField] private FloatingImageText _floatingPrefab;

        private ObjectPool<FloatingImageText> _pool;
        
        private ObjectPool<FloatingImageText> Pool
        {
            get
            {
                CreatePoolIfNotCreated();
                return _pool;
            }
        }

        public FloatingImageText Spawn()
        {
            return Pool.Get();
        }

        private void CreatePoolIfNotCreated()
        {
            if (_pool == null)
            {
                _pool = new ObjectPool<FloatingImageText>(
                    CreateParticle,
                    OnGetParticle,
                    OnReleaseParticle
                );
            }
        }

        private FloatingImageText CreateParticle()
        {
            var particle = Instantiate(_floatingPrefab);

            var animation = particle.GetComponent<AnimationSequencerController>();
            particle.SetOnSpawn(() =>
            {
                void FinishedCallback() => OnAnimationEnded(particle);
                animation.Play(FinishedCallback);
            });
            
            return particle;
        }

        private void OnGetParticle(FloatingImageText floatingParticle)
        {
            
            floatingParticle.Spawn();
        }
        
        private void OnReleaseParticle(FloatingImageText floatingParticle)
        {
            floatingParticle.Hide();
        }

        private void OnAnimationEnded(FloatingImageText particle)
        {
            Pool.Release(particle);
        }
    }
}