using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
using Unity.Barracuda;

public class Load_CreateMusicModel : MonoBehaviour
{

    [SerializeField] private NNModel modelAsset;


    private Model _runtimeModel;
    private IWorker _engine;


    // Start is called before the first frame update
    void Start()
    {
        //¶ÁÈ¡Ä£ÐÍ
        _runtimeModel = ModelLoader.Load(modelAsset);
        _engine = WorkerFactory.CreateWorker(_runtimeModel, WorkerFactory.Device.GPU);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
