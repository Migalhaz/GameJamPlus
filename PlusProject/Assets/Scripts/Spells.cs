using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FireflySettings : Spell
{
    [SerializeField] GameObject bullet;
    [SerializeField] float fireForce;
    [SerializeField] List<GameObject> fireflies = new(5);
    [SerializeField] float timeToShot;
    List<float> currentTimerToShot = new(1);
    [SerializeField] Transform baseFireflies;
    [SerializeField] FollowTarget followSettings;
    [SerializeField] List<Transform> firepoint;

    public override void Setup()
    {
        followSettings.Follow(baseFireflies);
        currentTimerToShot.Clear();
        currentTimerToShot.Add(timeToShot);
        for (int i = 0; i < fireflies.Count; i++)
        {
            fireflies[i].SetActive(false);
        }
        fireflies[0].SetActive(true);
    }

    public override void AddLevel()
    {
        base.AddLevel();
        NewFirefly(currentLevel);
    }
    public void Atack()
    {
        for (int i = 0; i < currentTimerToShot.Count; i++)
        {
            currentTimerToShot[i] -= Time.deltaTime;
        }
        for (int i = 0; i < currentTimerToShot.Count; i++)
        {
            if (currentTimerToShot[i] <= 0)
            {
                currentTimerToShot[i] = timeToShot;

                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                if (enemies.Length > 0)
                {
                    Vector3 target = NearestEnemy(enemies, followSettings.GetTarget).position;
                    GameObject b = MonoBehaviour.Instantiate(bullet, firepoint[i].position, Quaternion.identity);
                    b.GetComponent<Bullet>().dir = target;
                }
                
            }
        }

    }

    Transform NearestEnemy(GameObject[] enemies, Transform _player)
    {
        if (enemies == null) return null;
        Transform target = null;
        float closer = Mathf.Infinity;
        Vector3 currentPos = _player.position;
        foreach (GameObject pTarget in enemies)
        {
            Vector3 dirToTarget = pTarget.transform.position - currentPos;
            float dSqrToTarget = dirToTarget.sqrMagnitude;
            if (dSqrToTarget < closer)
            {
                closer = dSqrToTarget;
                target = pTarget.transform;
            }
        }

        return target;
    }

    public override void Update()
    {
        Atack();
        followSettings.SmoothFollow(baseFireflies);

    }

    public void NewFirefly(int _fireflyIndex)
    {
        if (_fireflyIndex >= fireflies.Count) return;
        fireflies[_fireflyIndex].SetActive(true);
        currentTimerToShot.Add(timeToShot);
    }
}

[System.Serializable]
public class Satellite : Spell 
{
    [SerializeField] float rotationSpeed;
    float currentRotation;
    [SerializeField] List<SatellitePos> pos;
    List<GameObject> sattelitesInScene;
    [SerializeField] GameObject sattelite;
    [SerializeField] GameObject baseSatellite;
    [SerializeField] FollowTarget followSettings;
    float a = 0;

    public override void Setup()
    {
        a = 0;
        followSettings.Follow(baseSatellite);
        sattelitesInScene = new();
        currentRotation = 0;
    }
    public override void AddLevel()
    {
        base.AddLevel();
        if (sattelitesInScene.Count > 0)
        {
            foreach (GameObject s in sattelitesInScene)
            {
                MonoBehaviour.Destroy(s);
            }
            sattelitesInScene.Clear();
        }
        baseSatellite.transform.rotation = Quaternion.identity;
        for (int i = 0; i < currentLevel; i++)
        {
            GameObject s = MonoBehaviour.Instantiate(sattelite, pos[currentLevel-1].pos[i] + baseSatellite.transform.position, Quaternion.identity, baseSatellite.transform);
            sattelitesInScene.Add(s);
        }
    }

    public override void Update()
    {
        a += 1;
        foreach (var i in sattelitesInScene)
        {
            i.gameObject.transform.rotation = Quaternion.Euler(a, 0f, a);
        }
        followSettings.Follow(baseSatellite.transform);
        currentRotation += rotationSpeed * Time.deltaTime;
        currentRotation = currentRotation >= 360 ? 0 : currentRotation;
        Rotate(Quaternion.Euler(0f, currentRotation, 0f), baseSatellite.transform);
    }

    void Rotate(Quaternion _quat, Transform _obj)
    {
        _obj.rotation = _quat;
    }
}
[System.Serializable]
public struct SatellitePos 
{
    public List<Vector3> pos;
}
