using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] int hp = 10;
    //[SerializeField] Image 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddDamage(int damage) 
    { 
        hp -= damage;
        if (hp < 0)
        {
            hp = 0;
            die();
        }
    }

    public void die() 
    { 
        Destroy(gameObject);
    }
}
