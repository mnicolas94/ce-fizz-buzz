using System.Threading.Tasks;
using BrunoMikoski.AnimationSequencer;
using DG.Tweening;
using UnityEngine;

namespace View
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private AnimationSequencerController _spawnAnimation;
        
        public async Task Spawn()
        {
            _spawnAnimation.Play();
            await _spawnAnimation.PlayingSequence.AsyncWaitForCompletion();
        }
    }
}