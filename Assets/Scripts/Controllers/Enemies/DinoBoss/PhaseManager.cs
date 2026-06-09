using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    public static PhaseManager instance;

    [SerializeField] private float waveDelay0;
    [SerializeField] private int waveSize0;

    [SerializeField] private float waveDelay1;
    [SerializeField] private int waveSize1;

    [SerializeField] private float waveDelay2;
    [SerializeField] private int waveSize2;

    [HideInInspector] public List<GameObject> wave;
    void Start()
    {
        instance = this;
        WaveSpawner.instance.StartWave(waveDelay0, waveSize0);
    }
    void Update()
    {
        
    }
}
