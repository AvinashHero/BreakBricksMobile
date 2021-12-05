using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Vector2 direction { get; private set; }

    //public float speed = 40f;
    public float maxBounceAngle = 75f;

    public float minX, maxX;
    Vector3 touchPosition;

    private void Update()
    {
        //float x = Input.GetAxisRaw("Horizontal");
        //this.direction = new Vector2(x, 0f);
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            this.touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            this.touchPosition.y = this.transform.position.y;
            this.touchPosition.z = 0f;
            this.direction = this.touchPosition;
        }


    }

    private void FixedUpdate()
    {
        if(this.direction != Vector2.zero)
        {
            this.transform.position = this.direction;
        }
    }
    public void ResetPlayer()
    {
        this.transform.position = new Vector2(0f, this.transform.position.y);
        this.touchPosition = Vector2.zero;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if(ball != null)
        {
            Vector3 paddlePosition = this.transform.position;
            Vector2 contactPoint = collision.GetContact(0).point;

            float offset = paddlePosition.x - contactPoint.x;

            float width = collision.otherCollider.bounds.size.x / 2;
            float currentAngle = Vector2.SignedAngle(Vector2.up, ball.rb.velocity);

            float bounceAngle = (offset / width) * this.maxBounceAngle;
            float newBounceAngle = Mathf.Clamp(currentAngle + bounceAngle, -this.maxBounceAngle, this.maxBounceAngle);

            Quaternion rotation = Quaternion.AngleAxis(newBounceAngle, Vector3.forward);
            ball.rb.velocity = rotation * Vector2.up * ball.rb.velocity.magnitude;
        }
    }
}
