using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    [SerializeField] FireflySettings firefly;
    [SerializeField] Satellite satellite;
    void Start()
    {
        firefly.Setup();
        satellite.Setup();
    }

    // Update is called once per frame
    void Update()
    {
        firefly.Update();
        satellite.Update();
    }

    [ContextMenu("LevelUp")]
    public void LevelUp(string _s)
    {
        if (_s == "satellite")
        {
            satellite.AddLevel();
        }
        else if (_s == "firefly")
        {
            firefly.AddLevel();
        }
    }

    [ContextMenu("satelite")]
    void upS()
    {
        satellite.AddLevel();
    }
}

[System.Serializable]
public class Spell 
{
    [SerializeField] protected string name;
    [SerializeField] protected int currentLevel;
    [SerializeField] protected int maxLevel;
    [SerializeField] protected int startLevel;
    [SerializeField] protected int levelToBuff;

    public virtual void Setup() { }
    public virtual void Update() { }
    public virtual void AddLevel()
    {
        if (CanAddLevel())
        {
            currentLevel += 1;
            if (currentLevel == levelToBuff)
            {
                LevelUp();
            }
        }
        
    }

    public bool CompareSpell(Spell _spell)
    {
        return this == _spell;
    }

    public bool CanAddLevel()
    {
        return currentLevel < maxLevel;
    }

    public virtual void LevelUp()
    {
        //a implementar
    }

}


[System.Serializable]
public class HealthSystem
{
    [SerializeField] protected float maxHealthPoints = 5;
    [SerializeField] protected float currentHealthPoints = 5;
    public float GetLife { get { return currentHealthPoints; } private set { GetLife = value; } }
    public virtual void Damage(float _damage)
    {
        currentHealthPoints -= _damage;
    }
}

[System.Serializable]
public class PlayerHealthSystem : HealthSystem 
{
    public float GetMaxHealth { get { return maxHealthPoints;  } set { GetMaxHealth = value; } }
    public float invTime;
    public bool canTakeDamage;
    public void Heal(float _healValue)
    {
        currentHealthPoints += _healValue;
        currentHealthPoints = currentHealthPoints > maxHealthPoints ? maxHealthPoints : currentHealthPoints;
    }

    public override void Damage(float _damage)
    {
        if (!canTakeDamage) return;
        base.Damage(_damage);
        canTakeDamage = false;
    }
}

[System.Serializable]
public class FollowTarget
{
    //[SerializeField] bool smooth;
    [SerializeField] float smoothposValue;
    [SerializeField] float smoothrotValue;
    [SerializeField] bool rotate;
    [SerializeField] Transform target;
    public Transform GetTarget { get { return target;  } private set { GetTarget = value; } }
    [SerializeField] Vector3 offset;

    public void Follow(GameObject _obj)
    {
        _obj.transform.position = target.position + offset;
        if (rotate)
        {
            _obj.transform.rotation = target.rotation;
        }
        
    }

    public void Follow(Transform _obj)
    {
        _obj.position = target.position + offset;
        if (rotate)
        {
            _obj.rotation = target.rotation;
        }
       
    }

    public void SmoothFollow(Transform _obj)
    {
        //pegar speed do player para sincronizar a velocidade
        //float m_randomSmoothPos = Random.Range(speedPosValue / 1.5f, speedPosValue * 1.2f);
        _obj.position = Vector3.MoveTowards(_obj.position, target.position + offset, smoothposValue * Time.deltaTime);
        //float m_randomSmooth = Random.Range(smoothrotValue / 1.5f, smoothrotValue * 1.2f);
        _obj.rotation = Quaternion.Slerp(_obj.rotation, target.rotation, smoothrotValue * Time.deltaTime);



        //Vector3 smoothpos = new(Mathf.Lerp(_obj.position.x, target.position.x + offset.x, smoothposValue), Mathf.Lerp(_obj.position.y, target.position.y + offset.y, smoothposValue), Mathf.Lerp(_obj.position.z, target.position.z + offset.z, smoothposValue));
        /*
        Vector3 smoothrot = new(Mathf.LerpAngle(_obj.rotation.x, target.rotation.x, smoothrotValue), Mathf.LerpAngle(_obj.rotation.y, target.rotation.y, smoothrotValue), Mathf.LerpAngle(_obj.rotation.z, target.rotation.z, smoothrotValue));
        _obj.position = smoothpos;
        if (rotate)
        {
            //_obj.rotation = target.rotation;
            _obj.rotation = Quaternion.Euler(smoothrot.x, smoothrot.y, smoothrot.z);
        }
        */
    }
}

[System.Serializable]
public class ColliderMesh
{
    public enum ColliderType { N, Box, Circle }
    [SerializeField] ColliderType colliderType;
    [SerializeField] Vector3 size;
    [SerializeField] Vector3 offset;
    [SerializeField] LayerMask layer;

    public Collider[] InCollision(Vector3 _pos)
    {
        switch (colliderType)
        {
            case ColliderType.Box:
                return Physics.OverlapBox(_pos + offset, size, Quaternion.identity, layer);
            case ColliderType.Circle:
                return Physics.OverlapSphere(_pos + offset, size.x, layer);
            default:
                return null;
        }
    }

    public void DrawCollider(Vector3 _pos)
    {
        Gizmos.color = InCollision(_pos).Length > 0 ? Color.green : Color.red;
        switch (colliderType)
        {
            case ColliderType.Box:
                Gizmos.DrawWireCube(_pos + offset, size);
                break;
            case ColliderType.Circle:
                Gizmos.DrawWireSphere(_pos + offset, size.x);
                break;
        }
    }
}
