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
    public GameObject bomb;
    public GameObject scopePulemet;
    public Transform playerTransform;
    Vector3 playerPosition;
    public float startShootRate;
    public float pulemetRate;
    private float shootRate;
    private float switchRate;
    public float aimRadius;
    public int shotgunShots;
    public float shotgunSpread;
    public float shotgunScale;
    public float bazukaScale;

    public float[] switchRateRand = {0, 0};

    public float rifleWeight;
    public float shotgunWeight;
    public float laserWeight;
    float weightSum;

    public Weapon currentWeapon;

    private GameObject createdScopePulemet;

    public int countMiss = 0;
    public bool isBomb;

    public GameObject allHand;
    public GameObject[] hands;
    public GameObject currentHand;
    private float targetX;
    public float speedHand;
    public Animator cloudCenzAnim;

    public AudioSource pistolShootAudio;
    public AudioSource shotgunShootAudio;
    public AudioSource bazukaShootAudio;
    public AudioSource pulemetShootAudio;

    public Animator changeHand;

    //public Sprite[] shotSprites;

    private void Awake()
    {
        //instance = this;
        shootRate = startShootRate;
        weightSum = rifleWeight + shotgunWeight + laserWeight;
        SwitchToWeapon(Weapon.Pistol);
        StartCoroutine(SwitchToShotgun());
        switchRate = Random.Range(switchRateRand[0], switchRateRand[1]);
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
/*
    public IEnumerator TestCor()
    {
        yield return null;
        Debug.Log(Time.deltaTime);
    }
    */

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
                case Weapon.Pulemet:
                    shootRate = pulemetRate;
                    ShootPulumet();
                    break;
                case Weapon.Bomb:
                    shootRate = startShootRate;
                    ShootBomb();
                    StopShooting();
                    break;
            }
        }
    }

    private void ShootPistol()
    {
        Vector3 ScopePosition = Aim();

        GameObject createdScope = GameObject.Instantiate(scope);
        createdScope.transform.position = ScopePosition;
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
        createdScope.transform.position = new Vector3(ScopePosition.x, 0, 0);
        targetX = createdScope.transform.position.x;
        countMiss += 1;
    }

    private void ShootBomb()
    {
        Vector3 ScopePosition = Aim();
        isBomb = true;
        GameObject createdScope = GameObject.Instantiate(bomb);
        createdScope.transform.position = new Vector3(ScopePosition.x, 6, 0);
        Scope scope = createdScope.GetComponent<Scope>();
        Invoke("AfterBomb", scope.delay);
        targetX = createdScope.transform.position.x;
        countMiss += 1;
    }

    private void ShootPulumet()
    {
        Vector3 ScopePosition = Aim();

        GameObject createdScope = GameObject.Instantiate(scopePulemet);
        createdScope.transform.position = ScopePosition;
        //createdScope.transform.localScale = Vector3.one * shotgunScale;
        targetX = createdScope.transform.position.x;

        Vector3 targetPosition = new Vector3(playerTransform.position.x, playerTransform.position.y, 0);
        float smoothSpeed = 0.5f;
        createdScope.transform.position = Vector3.Lerp(createdScope.transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        Animator handAnim = currentHand.GetComponent<Animator>();
        handAnim.SetTrigger("isShoot");
        pulemetShootAudio.PlayOneShot(pulemetShootAudio.clip);
        //countMiss += 1;
    }

    public void AfterBomb()
    {
        isBomb = false;
        StartShooting();
        SwitchToWeapon(Weapon.Pistol);
        StartCoroutine(SwitchToShotgun());
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
        switchRate = Random.Range(switchRateRand[0], switchRateRand[1]);
        yield return new WaitForSeconds(switchRate);
        SwitchToWeapon(Weapon.Pistol);
        StartCoroutine(SwitchToShotgun());
    }

    public IEnumerator SwitchToShotgun()
    {
        switchRate = Random.Range(switchRateRand[0], switchRateRand[1]);
        yield return new WaitForSeconds(switchRate);
        SwitchToWeapon(Weapon.Shotgun);
        StartCoroutine(SwitchToBazooka());
    }

    public IEnumerator SwitchToBazooka()
    {
        switchRate = Random.Range(switchRateRand[0], switchRateRand[1]);
        yield return new WaitForSeconds(switchRate);
        SwitchToWeapon(Weapon.Bazooka);
        StartCoroutine(SwitchToLazer());
    }

    public IEnumerator SwitchToLazer()
    {
        switchRate = Random.Range(switchRateRand[0], switchRateRand[1]);
        yield return new WaitForSeconds(switchRate);
        SwitchToWeapon(Weapon.Lazer);
        StartCoroutine(SwitchToPulemet());
    }

    public IEnumerator SwitchToPulemet()
    {
        switchRate = Random.Range(switchRateRand[0], switchRateRand[1]);
        yield return new WaitForSeconds(switchRate);
        SwitchToWeapon(Weapon.Pulemet);
        StartCoroutine(SwitchToBomb());
    }

    public IEnumerator SwitchToBomb()
    {
        //switchRate = Random.Range(switchRateRand[0], switchRateRand[1]);
        yield return new WaitForSeconds(switchRate);
        SwitchToWeapon(Weapon.Bomb);
        //StartCoroutine(SwitchToPistol());
    }

    private void FixedUpdate()
    {
        //if (currentWeapon == Weapon.Pulemet)
        //{
            //playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //    Vector3 targetPosition = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
        //    float smoothSpeed = 0.5f; // Управляет скоростью передвижения прицела, измените это значение по своему усмотрению
        //    createdScopePulemet.transform.position = Vector3.Lerp(createdScopePulemet.transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        //}
        allHand.transform.position = Vector3.Lerp(allHand.transform.position, new Vector3(targetX, allHand.transform.position.y, 0), Time.deltaTime * speedHand);
    }
}

public enum Weapon
{
    Pistol,
    Shotgun,
    Bazooka,
    Lazer,
    Pulemet,
    Bomb
}
