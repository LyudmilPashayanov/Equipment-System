using UnityEngine;

public class GunController : Item
{
    private const int MAGAZINE_SIZE = 10;

    [SerializeField] private GunView _view;
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private BulletFactory _bulletFactory;
    [SerializeField] private float _bulletSpeed = 10f;
    [SerializeField] private float _fireRate = 0.333f; // 3 bullets per second

    private bool _isShooting;
    private float _fireTimer;
    private int _magazine = MAGAZINE_SIZE;
    private bool _automaticShoot = true;

    private void Start()
    {
        _view.SetRemainingBullets(_magazine.ToString());
        _view.ToggleAutomaticModeText(_automaticShoot);
    }

    private void Update()
    {
        if (_itemEquipped)
        {
            ShootingTick();
        }
    }

    private void ShootingTick()
    {
        if (_fireTimer > 0)
        {
            _fireTimer -= Time.deltaTime;
        }
        if (_isShooting && _fireTimer <= 0 && _magazine > 0)
        {
            FireBullet();
            _fireTimer = _fireRate;
        }
    }

    private void FireBullet()
    {
        GameObject bullet = _bulletFactory.GetBullet();
        bullet.transform.position = _bulletSpawnPoint.position;
        bullet.transform.rotation = _bulletSpawnPoint.rotation;
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            bulletRigidbody.velocity = bullet.transform.forward * _bulletSpeed;
        }
        _view.SetRemainingBullets((--_magazine).ToString());
        if (!_automaticShoot) 
        {
            StopShooting();
        }
    }

    private void StartShooting()
    {
        _isShooting = true;
    }

    private void StopShooting()
    {
        _isShooting = false;
    }

    public void ReloadBullets(int bulletsBeingReloaded)
    {
        _magazine += bulletsBeingReloaded;
        if (_magazine > MAGAZINE_SIZE)
            _magazine = MAGAZINE_SIZE;
        _view.SetRemainingBullets(_magazine.ToString());
    }

    public override void ToggleMode()
    {
        _automaticShoot = !_automaticShoot;
        _view.ToggleAutomaticModeText(_automaticShoot);
    }

    public override void StartUse()
    {
        StartShooting();
    }

    public override void StopUse()
    {
        StopShooting();
    }

    public override void Equip(Hand _currentHand)
    {
        base.Equip(_currentHand);
    }

    public override void Unequip(Vector3 throwDirection, float throwForce)
    {
        base.Unequip(throwDirection, throwForce);
        StopShooting();
    }
}
