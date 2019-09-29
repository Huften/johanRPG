using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    Vector3 localScale;
    private EnemyHealth enemyHealth;
    public GameObject Enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        enemyHealth = Enemy.GetComponent<EnemyHealth>();
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        localScale.x = enemyHealth.currentHealth;
        transform.localScale = localScale;
    }
}
