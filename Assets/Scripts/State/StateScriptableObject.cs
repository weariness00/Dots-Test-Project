using UnityEngine;

namespace State
{
    [CreateAssetMenu(fileName = "Game", menuName = "State", order = 0)]
    public class StateScriptableObject : ScriptableObject
    {
        public StateStruct state;
    }
}