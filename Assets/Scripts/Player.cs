using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using Assets.Scripts.Constants;

public class Player : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float rotationSpeed = 0.8f;
    
    public GameObject bulletPrefab;
    public Transform[] placeHolders;
    public float fireRate;
    private int firePower;
    private float fireCount;
    //public AudioSource bulletSound;
    
    public int currentHealth;
    public int maxHealth = 5;
    public float hitShieldTime = 1f;
    private bool hitShield;
    
    private float verticalSize;
    private float horizontalSize;
    private float spriteVerticalSize;
    private float spriteHorizontalSize;

    private TimeScale timeScale;
    private UI ui;

    // Use this for initialization
    void Start()
    {
        this.name = GameObjectNames.PlayerShip;
        this.LoadTimeScale();
        this.LoadUi();
        this.LoadHealth();
        this.LoadCameraSize();
        this.LoadShip();
        this.LoadPlaceHolders();
    }

    // Update is called once per frame
    void Update()
    {
        this.CheckMovement();
        this.CheckRotation();        

        if (CheckFireRate() && Input.GetAxis("Fire1") == 1 && this.timeScale.playerScale != 0)
        {
            this.Shoot();
        }
    }      

    void LoadHealth()
    {
        this.currentHealth = maxHealth;
        this.hitShield = false;    
    }

    void LoadCameraSize()
    {
        verticalSize = Camera.main.orthographicSize;
        horizontalSize = Screen.width * verticalSize / Screen.height;
    }

    void LoadShip()
    {       
        var sprite = this.gameObject.GetComponent<SpriteRenderer>();

        sprite.sortingOrder = 1;
        spriteVerticalSize = sprite.bounds.size.y / 2;
        spriteHorizontalSize = sprite.bounds.size.x / 2;

        this.LoadBullet(this.bulletPrefab);
    }

    void LoadBullet(GameObject bulletPrefab)
    {
        this.bulletPrefab = bulletPrefab;
        this.fireRate = this.bulletPrefab.GetComponent<Bullet>().fireRate;
        this.firePower = 1;
    }
    
    void LoadPlaceHolders()
    {
        var phName = "PH_" + this.firePower;
        
        var childs = this.transform.FindChild(phName).childCount;

        placeHolders = new Transform[childs];

        for (int i = 0; i < placeHolders.Length; i++)
        {
            placeHolders[i] = this.transform.FindChild(phName).GetChild(i);
        }
    }

    void LoadTimeScale()
    {
        this.timeScale = GameObject.Find(GameObjectNames.TimeScale).GetComponent<TimeScale>();
    }

    void LoadUi()
    {
        this.ui = GameObject.Find(GameObjectNames.Ui).GetComponent<UI>();
        this.ui.UpdateActiveBullet(this.bulletPrefab);
    }

    void CheckMovement()
    {
        if ((Input.GetAxis("Vertical") > 0 && (this.transform.position.y + spriteVerticalSize) < this.verticalSize) 
            || (Input.GetAxis("Vertical") < 0 && (this.transform.position.y - spriteVerticalSize) > -this.verticalSize))
        {
            this.transform.position += Vector3.up * movementSpeed * Time.deltaTime * Input.GetAxis("Vertical") * this.timeScale.playerScale;
        }
            
        if ((Input.GetAxis("Horizontal") > 0 && (this.transform.position.x + spriteHorizontalSize) < this.horizontalSize) 
            || (Input.GetAxis("Horizontal") < 0 && (this.transform.position.x - spriteHorizontalSize) > -this.horizontalSize))
        {
            this.transform.position += Vector3.right * movementSpeed * Time.deltaTime * Input.GetAxis("Horizontal") * this.timeScale.playerScale;
        }

        this.timeScale.slowMotionGaugeSprite.rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, this.transform.position);
    }

    void CheckRotation()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        var direction = (mousePosition - this.transform.position).normalized;

        this.transform.up = Vector3.Lerp(this.transform.up, direction, rotationSpeed * this.timeScale.playerScale);
    }

    bool CheckFireRate()
    {
        if (fireCount >= fireRate)
        {
            return true;
        }

        fireCount += Time.deltaTime;

        return false;
    }

    void Shoot()
    {
        for (var i = 0; i < placeHolders.GetLength(0); i++)
        {
            var bullet = GameObject.Instantiate(this.bulletPrefab);
                        
            bullet.transform.position = this.placeHolders[i].position;
            bullet.transform.up = this.placeHolders[i].up;
            bullet.gameObject.layer = this.gameObject.layer;
        }

        fireCount = 0;

        //this.bulletSound.Play();
    }

    void BulletHit(GameObject bullet)
    {
        if (bullet.layer != this.gameObject.layer)
        {
            this.HitTaken();
        }
    }

    void EnemyHit()
    {
        this.HitTaken();
    }

    void ApplyPowerUp(GameObject powerUp)
    {
        var container = powerUp.GetComponent<PowerUpContainer>();

        var bulletPrefab = container.bulletPrefabs[container.activePrefab];

        if (this.bulletPrefab.GetComponent<Bullet>().type == bulletPrefab.GetComponent<Bullet>().type)
        {
            if (this.firePower < 3)
            {
                this.firePower++;
            }
        }
        else
        {
            this.LoadBullet(bulletPrefab);
            this.ui.UpdateActiveBullet(container.bulletPrefabs[container.activePrefab]);
        }

        this.LoadPlaceHolders();
    }

    void ApplyHealPack()
    {
        if(currentHealth < maxHealth)
        {
            this.ui.UpdatePlayerHealth(this.currentHealth, true);

            this.currentHealth++;            
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Bullet":
                this.BulletHit(collision.gameObject);
                break;

            case "Buff":
                this.ApplyPowerUp(collision.gameObject);
                break;

            case "HealPack":
                this.ApplyHealPack();
                break;

            case "Shield":
                this.EnemyHit();
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            this.EnemyHit();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            this.EnemyHit();
        }
    }

    private void HitTaken()
    {
        if(!hitShield)
        {           
            this.currentHealth--;

            this.ui.UpdatePlayerHealth(this.currentHealth, false);

            if (this.currentHealth <= 0)
            {
                this.TriggerDeathSecuence();
            }
            else
            {                
                this.hitShield = true;

                this.InvisibleOn();
                Invoke("ResetHitShield", hitShieldTime);
            }
        }        
    }

    private void TriggerDeathSecuence()
    {
        SceneManager.LoadScene("GameOver");
        GameObject.Destroy(this.gameObject);
    }

    private void ResetHitShield()
    {
        this.hitShield = false;
        this.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void InvisibleOn()
    {
        if (this.hitShield)
        {
            this.GetComponent<SpriteRenderer>().enabled = false;       
            Invoke("InvisibleOff", 0.1f);
        }
    }

    private void InvisibleOff()
    {
        if (this.hitShield)
        {
            this.GetComponent<SpriteRenderer>().enabled = true;
            Invoke("InvisibleOn", 0.1f);
        }
    }


}
