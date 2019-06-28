using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parent;
    [SerializeField] int scorePerHit = 10;


    ScoreBoard scoreBoard;
    // Start is called before the first frame update
    void Start()
    {
        AddNewBoxCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void AddNewBoxCollider()
    {
        Collider enemyCollider = gameObject.AddComponent<BoxCollider>();
        enemyCollider.isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnParticleCollision(GameObject other)
    {
        if (scoreBoard)
        {
            scoreBoard.ScoreHit(scorePerHit);
        }
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
        
        Destroy(gameObject);       
    }
}
