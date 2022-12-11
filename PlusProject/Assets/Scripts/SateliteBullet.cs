using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SateliteBullet : MonoBehaviour
{
    [SerializeField] ColliderMesh col;
    PlayerScript player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var i in col.InCollision(transform.position))
        {
            player.LevelIncrease();
            Destroy(i.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        col.DrawCollider(transform.position);
    }
}
