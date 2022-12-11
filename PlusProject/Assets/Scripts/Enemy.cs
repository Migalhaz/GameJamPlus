using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public HealthSystem hpSys;
    [SerializeField] float speed;
    [SerializeField] ColliderMesh col;
    [SerializeField] List<Material> textures;
    [SerializeField] SkinnedMeshRenderer mesh;
    Transform target;
    [SerializeField] PlayerScript playerScript;

    void Start()
    {
        mesh.material = textures[Random.Range(0, textures.Count)];
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
        playerScript = player.GetComponent<PlayerScript>();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        foreach (var c in col.InCollision(transform.position))
        {
            string s = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(s);
        }
        if (hpSys.GetLife <= 0)
        {
            playerScript.LevelIncrease();
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        col.DrawCollider(transform.position);
    }
}
