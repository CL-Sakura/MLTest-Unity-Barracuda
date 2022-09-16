using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Barracuda;
using UnityEngine;

public class ReadModel : MonoBehaviour
{
    [SerializeField] private NNModel modelAsset;
    [SerializeField] private Texture2D texture;


    private Model _runtimeModel;

    private IWorker _engine;


    [Serializable]
    public struct Prediction
    {
        public int predictValue;
        public float[] predicted;

        public void SetPrediction(Tensor tensor)
        {
            predicted = tensor.AsFloats();
            predictValue = Array.IndexOf(predicted, predicted.Max());
            Debug.Log($"Predicted {predictValue}");

        }
    }

    public Prediction prediction;


    void Start()
    {
        _runtimeModel = ModelLoader.Load(modelAsset);
        _engine = WorkerFactory.CreateWorker(_runtimeModel, WorkerFactory.Device.GPU);
        prediction = new Prediction();

    }

    
    void Update()
    {
        //将tenser转化为灰度图
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var channelCount = 1;
            var inputX = new Tensor(texture, channelCount);

            Tensor outputY = _engine.Execute(inputX).PeekOutput();
            inputX.Dispose();
            prediction.SetPrediction(outputY);

        }
    }


    private void OnDestroy()
    {
        _engine?.Dispose();
    }

}
