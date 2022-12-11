using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float speed;
    [SerializeField] ColliderMesh col;
    [SerializeField] Rigidbody rig;
    public Vector3 dir;
    PlayerScript player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, dir, speed * Time.deltaTime);
        foreach (var i in col.InCollision(transform.position))
        {
            if (i.gameObject.CompareTag("Enemy"))
            {
                player.LevelIncrease();
                Destroy(i.gameObject);
            }
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        col.DrawCollider(transform.position);
    }
}
