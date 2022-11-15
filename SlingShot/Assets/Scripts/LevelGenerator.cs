using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameMechanicsScript _GMScript;
    [SerializeField] private Transform _levelContainer;
    [SerializeField] private List<GameObject> _levels = new List<GameObject>();

    public int LoadRandomLevel()
    {
        _levelContainer = GameObject.FindGameObjectWithTag("Container").transform;
        GameObject Level = Instantiate(_levels[Random.Range(0, _levels.Count)], _levelContainer);
        int CoinsOnScene = Level.transform.GetChild(1).childCount;
        return CoinsOnScene;
    }
}
