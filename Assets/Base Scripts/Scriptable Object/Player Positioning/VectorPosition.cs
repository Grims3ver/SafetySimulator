using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class VectorPosition : ScriptableObject, ISerializationCallbackReceiver
{
  

    public Vector2 initialPos;
    public Vector2 defaultPos; 

    public void OnAfterDeserialize()
    {
        initialPos = defaultPos; //reset position after deserialization
    }

    public void OnBeforeSerialize() { 


    }

}
