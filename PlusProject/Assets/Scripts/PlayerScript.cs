using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerScript : MonoBehaviour
{
    [Header("Settings")]
    public PlayerHealthSystem healthSystem;
    public SpellManager spellManager;

    [Header("Move")]
    [SerializeField] Rigidbody rig;
    [SerializeField] float speed;
    float fixedAngles;
    float angles;
    float smoothVelocity;
    [SerializeField] float smoothRotation;

    [SerializeField] int levelUp;
    public int currentlevel;

    void Start()
    {
        HpReset();
    }

    // Update is called once per frame
    void Update()
    {
        if (!healthSystem.canTakeDamage)
        {
            StartCoroutine(INV());
        }
    }

    public void LevelIncrease()
    {
        print("A");
        currentlevel++;
        if (currentlevel >= levelUp)
        {
            int i = Random.Range(0, 2);
            if (i == 0)
            {
                spellManager.LevelUp("satellite");
            }
            else
            {
                spellManager.LevelUp("firefly");
            }
            currentlevel = 0;
        }
    }

    IEnumerator INV()
    {
        for (int i = 0; i < healthSystem.invTime; i++)
        {
            yield return new WaitForSeconds(0.1f);
        }
        if (!healthSystem.canTakeDamage)
        {
            healthSystem.canTakeDamage = true;
        }
        
    }

    void HpReset()
    {
        healthSystem.canTakeDamage = true;
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector3 move = new(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        Vector3 moveNormalize = move.normalized;
        Vector3 velocityValue = new(moveNormalize.x * speed, rig.velocity.y, moveNormalize.z * speed);
        fixedAngles = Mathf.SmoothDampAngle(transform.eulerAngles.y, angles, ref smoothVelocity, smoothRotation);


        //rotation from
        if (move.magnitude > 0.1f)
        {
            angles = Mathf.Atan2(moveNormalize.x, moveNormalize.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, fixedAngles, 0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, fixedAngles, 0f);
        }
        rig.velocity = velocityValue;
    }
}
