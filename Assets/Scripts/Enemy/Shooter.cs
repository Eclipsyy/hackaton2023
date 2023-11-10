using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    //public static Shooter instance;

    public GameObject scope;
    public GameObject scopeShotgun;
    public GameObject scopeBazooka;
    public GameObject lazer;
    public Transform playerTransform;
    Vector3 playerPosition;
    public float shootRate;
    public float switchRate;
    public float aimRadius;
    public int shotgunShots;
    public float shotgunSpread;
    public float shotgunScale;
    public float bazukaScale;

    public float rifleWeight;
    public float shotgunWeight;
    public float laserWeight;
    float weightSum;

    public Weapon currentWeapon;

    public int countMiss = 0;

    public GameObject allHand;
    public GameObject[] hands;
    public GameObject currentHand;
    private float targetX;
    public float speedHand;
    public Animator cloudCenzAnim;

    public AudioSource pistolShootAudio;
    public AudioSource shotgunShootAudio;
    public AudioSource bazukaShootAudio;

    public Animator changeHand;

    //public Sprite[] shotSprites;

    private void Awake()
    {
        //instance = this;
        weightSum = rifleWeight + shotgunWeight + laserWeight;
        SwitchToWeapon(Weapon.Pistol);
        StartCoroutine(SwitchToShotgun());
    }

    private void Update()
    {
        if (countMiss == 7)
        {
            cloudCenzAnim.SetTrigger("isAppear");
            countMiss = 0;
        }
    }

    public void StartShooting()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        StartCoroutine("ShootCor");
    }

    public void StopShooting()
    {
        StopCoroutine("ShootCor");
    }

    private Vector3 Aim()
    {

        Vector2 bias2D = Random.insideUnitCircle * aimRadius;
        Vector3 ShootBias = new Vector3(bias2D.x, bias2D.y, 0);
        playerPosition = playerTransform.position;
        Vector3 AimPosition = playerPosition + ShootBias;

        return AimPosition;
    }

    public IEnumerator TestCor()
    {
        yield return null;
        Debug.Log(Time.deltaTime);
    }

    public IEnumerator ShootCor()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootRate);
            switch (currentWeapon)
            {
                case Weapon.Pistol:
                    ShootPistol();
                    break;
                case Weapon.Shotgun:
                    ShootShotgun();
                    break;
                case Weapon.Bazooka:
                    ShootBazuka();
                    break;
                case Weapon.Lazer:
                    ShootLaser();
                    break;
            }
        }
    }

    private void ShootPistol()
    {
        Vector3 ScopePosition = Aim();

        GameObject createdScope = GameObject.Instantiate(scope);
        createdScope.transform.position = ScopePosition;
        //createdScope.GetComponent<SpriteRenderer>().sprite = shotSprites[0];
        targetX = createdScope.transform.position.x;

        Animator handAnim = currentHand.GetComponent<Animator>();
        handAnim.SetTrigger("isShoot");
        pistolShootAudio.PlayOneShot(pistolShootAudio.clip);
        countMiss += 1;

    }

    private void ShootBazuka()
    {
        Vector3 ScopePosition = Aim();

        GameObject createdScope = GameObject.Instantiate(scopeBazooka);
        //createdScope.GetComponent<SpriteRenderer>().sprite = shotSprites[2];
        createdScope.transform.position = ScopePosition;
        createdScope.transform.localScale = Vector3.one * bazukaScale;
        targetX = createdScope.transform.position.x;

        Animator handAnim = currentHand.GetComponent<Animator>();
        handAnim.SetTrigger("isShoot");
        bazukaShootAudio.PlayOneShot(bazukaShootAudio.clip);
        countMiss += 1;
    }

    private void ShootShotgun()
    {
        Vector3 ScopePosition = Aim();

        for (int i =0; i < shotgunShots; i++)
        {
            GameObject shot = Instantiate(scopeShotgun);
            //shot.GetComponent<SpriteRenderer>().sprite = shotSprites[1];
            Vector2 bias = Random.insideUnitCircle * shotgunSpread;
            shot.transform.position = new Vector3(bias.x, bias.y, 0) + playerPosition;
            shot.transform.localScale = Vector3.one * shotgunScale;
            targetX = shot.transform.position.x;
        }

        Animator handAnim = currentHand.GetComponent<Animator>();
        handAnim.SetTrigger("isShoot");
        shotgunShootAudio.PlayOneShot(shotgunShootAudio.clip);
        countMiss += 1;
    }

    private void ShootLaser()
    {
        Vector3 ScopePosition = Aim();

        GameObject createdScope = GameObject.Instantiate(lazer);
        createdScope.transform.position = new Vector3(ScopePosition.x, 0);
        //createdScope.GetComponent<SpriteRenderer>().sprite = shotSprites[0];
        targetX = createdScope.transform.position.x;

        //Animator handAnim = currentHand.GetComponent<Animator>();
        //handAnim.SetTrigger("isShoot");
        //laserShootAudio.PlayOneShot(laserShootAudio.clip);
        countMiss += 1;
    }

    public void SwitchToWeapon(Weapon weapon)
    {
        if (currentHand != null)
        {
            currentHand.SetActive(false);
        }
        changeHand.SetTrigger("isChange");
        currentHand = hands[(int)weapon];
        currentHand.SetActive(true);
        currentWeapon = weapon;
    }

    public IEnumerator SwitchToPistol()
    {
        yield return new WaitForSeconds(switchRate);
        SwitchToWeapon(Weapon.Pistol);
        StartCoroutine(SwitchToShotgun());
    }

    public IEnumerator SwitchToShotgun()
    {
        yield return new WaitForSeconds(switchRate);
        SwitchToWeapon(Weapon.Shotgun);
        StartCoroutine(SwitchToBazooka());
    }

    public IEnumerator SwitchToBazooka()
    {
        yield return new WaitForSeconds(switchRate);
        SwitchToWeapon(Weapon.Bazooka);
        StartCoroutine(SwitchToLazer());
        //StartCoroutine(SwitchToPistol());
    }

    public IEnumerator SwitchToLazer()
    {
        yield return new WaitForSeconds(switchRate);
        SwitchToWeapon(Weapon.Lazer);
        StartCoroutine(SwitchToPistol());
    }

    private void FixedUpdate()
    {
        allHand.transform.position = Vector3.Lerp(allHand.transform.position, new Vector3(targetX, allHand.transform.position.y, 0), Time.deltaTime * speedHand);
    }
}

public enum Weapon
{
    Pistol,
    Shotgun,
    Bazooka,
    Lazer
}
