using UnityEngine;

public class Brick : MonoBehaviour
{
    public new Renderer renderer { get; private set; }

    public Material[] materials;
    public int health { get; private set; }

    public bool unbreakable;

    public int points = 100;

    private void Awake()
    {
        this.renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        if(!this.unbreakable)
        {
            this.health = this.materials.Length;
            this.renderer.material = this.materials[this.health - 1];
        }
    }

    private void Hit()
    {
        if (this.unbreakable)
            return;

        this.health--;
        if(this.health <= 0)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.renderer.material = this.materials[this.health - 1];
        }

        FindObjectOfType<GameManager>().Hit(this);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Ball")
        {
            Hit();
        }
    }

}
