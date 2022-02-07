using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Attractor : MonoBehaviour
{
    public LayerMask AttractionLayer;
    public float gravity = -10;
    [SerializeField] private float effectionRadius = 10;
    public List<Collider2D> AttractedObjects = new List<Collider2D>();
    [HideInInspector] public Transform planetTransform;
    
    void Awake()
    {
        planetTransform = GetComponent<Transform>();
    }

    void Update()
    {
        SetAttractedObjects();
    }

    void FixedUpdate()
    {
        AttractObjects();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, effectionRadius);
    }

    void SetAttractedObjects()
    {
        AttractedObjects = Physics2D.OverlapCircleAll(planetTransform.position, effectionRadius, AttractionLayer).ToList();// can be optimized
    }

    void AttractObjects()
    {
        for (int i = 0; i < AttractedObjects.Count; i++)
        {
            AttractedObjects[i].GetComponent<Attractable>().Attract(this);
        }
    }

}

