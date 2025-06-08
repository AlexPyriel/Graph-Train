using System.Collections.Generic;
using Game.Configs;
using Graph.Interfaces;
using Graph.Models;
using UnityEngine;
using Zenject;

namespace Game.Views
{
    public class TrainSpawner : MonoBehaviour
    {
        [Header("Configs")]
        [SerializeField] private List<TrainConfig> _trainConfigs;
        [Space, Header("Prefabs")]
        [SerializeField] private List<GameObject> _trainPrefabs;
        
        private List<Train> _spawnedTrains;
        private IGraphModel _graphModel;

        [Inject]
        private void Construct(IGraphModel graphModel)
        {
            _graphModel = graphModel;
        }

        public void SpawnTrains()
        {
            _spawnedTrains ??= new List<Train>();
            
            _trainConfigs.ForEach(config =>
            {
                NodeModel startNode = _graphModel.GetRandomNode();
                GameObject trainObject = Instantiate(GetRandomPrefab(), startNode.Position, Quaternion.identity);

                if (!trainObject.TryGetComponent(out Train train))
                {
                    Destroy(trainObject);
                    return;
                }

                train.Initialize(startNode, _graphModel.EdgeMap, config);
                _spawnedTrains.Add(train);
            });

        }
        
        public void DespawnTrains()
        {
            _spawnedTrains.ForEach(train => Destroy(train.gameObject));
            _spawnedTrains.Clear();
        }

        private GameObject GetRandomPrefab()
        {
            return _trainPrefabs[Random.Range(0, _trainPrefabs.Count)];
        }
    }
}