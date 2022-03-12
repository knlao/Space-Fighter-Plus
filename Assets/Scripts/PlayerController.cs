using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float force;
    public bool isWhite;
    public GameObject gun;
    public GameObject bullet;
    public GameObject aimBullet;
    public Transform[] aimPoints;
    public float fireRate;
    public GameObject explosion;
    public int health = 3;
    public float respawnTimer = 3;
    public float powerRange;

    private Rigidbody _rb;
    private BoxCollider[] _bc;
    private float _h, _v;
    private Animator _anim;
    private Transform[] _guns;
    private int _aimPointIdx = 0;
    private float _nextShootTime;
    private bool _isHurt = false;
    private float _nextRespawnTime;


    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _bc = GetComponents<BoxCollider>();
        _anim = GetComponent<Animator>();
        _guns = gun.GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        // Movement
        _h = Input.GetAxis("Horizontal");
        _v = Input.GetAxis("Vertical");

        _rb.velocity = new Vector3(_h, _v, 0) * force;

        _h *= 1 - Time.deltaTime;
        _v *= 1 - Time.deltaTime;

        var currentPos = transform.position;
        var newPosition = new Vector3(
            Mathf.Clamp(currentPos.x, -16, 16),
            Mathf.Clamp(currentPos.y, -31, 31),
            currentPos.z
        );

        transform.position = newPosition;

        // Player Color
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            isWhite = !isWhite;
        }

        _anim.SetBool("isWhite", isWhite);

        // Bullet
        if (Time.time >= _nextShootTime)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                ShootBullet();
                _nextShootTime = Time.time + fireRate;
            }
        }

        // Special Power
        if (Input.GetKeyDown(KeyCode.Space) && FindObjectOfType<GameManager>().playerSubScore / 2000 >= 20)
        {
            FindObjectOfType<GameManager>().AddSubScore(-40000);
            var hitColliders = Physics.OverlapSphere(transform.position, powerRange);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.CompareTag("Enemy"))
                {
                    print(hitCollider.gameObject.name);

                    var newBullet = Instantiate(aimBullet, transform.position, aimBullet.transform.rotation);
                    newBullet.GetComponent<AimBullet>().isWhite = isWhite;
                    newBullet.GetComponent<AimBullet>().wps = new[]
                        {aimPoints[_aimPointIdx], hitCollider.gameObject.transform};
                    _aimPointIdx = ++_aimPointIdx % aimPoints.Length;
                }
            }
        }

        // Check is hurt
        if (_isHurt && _nextRespawnTime < Time.time)
        {
            _isHurt = false;
            foreach (var boxCollider in _bc)
            {
                boxCollider.enabled = true;
            }

            _anim.SetBool("isHurt", false);
        }
    }

    // Shoot Bullet
    private void ShootBullet()
    {
        for (var i = 1; i < _guns.Length; i++)
        {
            var b = Instantiate(bullet, _guns[i].position, new Quaternion(0, 0, 0, 0));
            b.GetComponent<Bullet>().isWhite = isWhite;
        }
        FindObjectOfType<SFX>().PlaySFX(0);
    }

    public void Dies()
    {
        if (!_isHurt)
        {
            health--;
            
            GetComponent<AudioSource>().Play();

            FindObjectOfType<GameManager>().UpdateHealth(health);
            if (health <= 0)
            {
                Instantiate(explosion, transform.position, explosion.transform.rotation);
                FindObjectOfType<GameManager>().GameOver();
                Destroy(gameObject);
            }
            else
            {
                _isHurt = true;
                _nextRespawnTime = Time.time + respawnTimer;
                foreach (var boxCollider in _bc)
                {
                    boxCollider.enabled = false;
                }

                _anim.SetBool("isHurt", true);
            }
        }
    }

    public void Absorb()
    {
        FindObjectOfType<GameManager>().AddSubScore(1000);
    }
}