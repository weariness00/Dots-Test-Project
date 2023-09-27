using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float3 offset;
    public float distance;

    private Entity target;
    private EntityManager entityManager;

    private void Awake()
    {
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    }

    private void Start()
    {
        target = entityManager.CreateEntityQuery(typeof(PlayerTag)).GetSingletonEntity();
    }

    private void LateUpdate()
    {
        if (target == null) return;

        LocalTransform targetTransform = entityManager.GetComponentData<LocalTransform>(target);
        transform.position = targetTransform.Position + offset +(-distance * targetTransform.Forward());
        transform.LookAt(targetTransform.Position + offset);
    }
}
