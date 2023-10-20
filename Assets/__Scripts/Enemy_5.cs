using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Enemy_5 : Enemy
{
    [Header("Set in Inspector: Enemy_5")]
 

    private Vector3 p0, p1; // The two points to interpolate
    private float timeStart; // Birth time for this Enemy_4
    private float duration = 5; // Duration of movement
    public GameObject DroppingPrefab;
    public float secondsBetweenDroppingDrops = 1f;

    void Start()
    {
        InitMovement();

        InvokeRepeating("DropDropping", 6f, secondsBetweenDroppingDrops);

        

    }
    void InitMovement()
    { // b
        p0 = p1 = pos;  // Set p0 to the old p1
                       // Assign a new on-screen location to p1
        float widMinRad = bndCheck.camWidth - bndCheck.radius;
        float hgtMinRad = bndCheck.camHeight - bndCheck.radius;
        p1.x = Random.Range(-widMinRad, widMinRad);
        p1.y = Random.Range(-hgtMinRad, hgtMinRad);

        timeStart = Time.time;

    }
    void DropDropping()
    { // b
        if (DroppingPrefab != null)
        {
            
            // Instantiate and position the DroppingPrefab
            GameObject Dropping = Instantiate<GameObject>(DroppingPrefab);
            Dropping.transform.position = transform.position;
        }
    }

    public override void Move()
    { // c
      // This completely overrides Enemy.Move() with a linear interpolation
        float u = (Time.time - timeStart) / duration;
        if (u >= 1)
        {
            InitMovement();
            u = 0;
        }
        u = 1 - Mathf.Pow(1 - u, 2); // Apply Ease Out easing to u // d
        pos = (1 - u) * p0 + u * p1; // Simple linear interpolation // e
    
    }

}

