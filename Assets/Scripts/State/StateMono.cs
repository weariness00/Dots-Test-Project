using Unity.Entities;
using UnityEditor.Searcher;
using UnityEngine;
using UnityEngine.Serialization;

namespace State
{
    public class StateMono : MonoBehaviour
    {
        public bool isScriptableObject;

        public StateScriptableObject stateScriptableObject;
        public StateStruct state;
    }
    
    public class StateBaker : Baker<StateMono>
    {
        public override void Bake(StateMono authoring)
        {
            var entity = GetEntity(authoring.gameObject, TransformUsageFlags.Dynamic);
            StateStruct state;
            if (authoring.stateScriptableObject == null)
                state = authoring.state;
            else
                state = authoring.isScriptableObject ? authoring.stateScriptableObject.state : authoring.state;
            AddComponent(entity, new StateData() { Data = state });
        }
    }
}
