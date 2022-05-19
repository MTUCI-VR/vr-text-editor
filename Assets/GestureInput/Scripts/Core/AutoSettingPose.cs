using Oculus.Interaction;
using Oculus.Interaction.Input;
using Oculus.Interaction.PoseDetection;
using Oculus.Interaction.PoseDetection.Debug;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSettingPose : MonoBehaviour
{
    [SerializeField]
    private HandShapeSkeletalDebugVisual skeletalDebugVisual;
    [SerializeField]
    private TransformRecognizerDebugVisual recognizerDebugVisual;

    [SerializeField,Interface(typeof(IHand))]
    private MonoBehaviour _hand;

    private void OnEnable()
    {
       // for (int i = 0; i < transform.childCount; i++)
       // {
          //  var handRefObj = transform.GetChild(i);
          //  handRefObj.GetComponent<HandRef>()._hand = _hand;
          //  handRefObj.GetComponent<SelectorUnityEventWrapper>().WhenSelected.AddListener(OnSelectedPose);
            //skeletalDebugVisual._shapeRecognizerActiveState = handRefObj.GetComponent<ShapeRecognizerActiveState>();
           // recognizerDebugVisual._transformRecognizerActiveStates = new TransformRecognizerActiveState[1];
           // recognizerDebugVisual._transformRecognizerActiveStates[0] = handRefObj.GetComponent<TransformRecognizerActiveState>();
       //}
    }
    private void Awake()
    {
        
        
    }

    private void OnSelectedPose(string name)
    {
        //MyLogger.Logger.Instance.SetText(name);
    }
}
