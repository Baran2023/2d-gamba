using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
   [SerializeField] public Camera cam;
    public Vector2 mouse;

    private void Awake()
    {
        //get refrences froom game obj
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        // cam = GetComponent<Camera>();
    }

    private void Update()
    {

        Vector2 mouse = cam.ScreenToWorldPoint(Input.mousePosition);


        float horozontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horozontalInput * speed, body.velocity.y);

        //direction of the character
        if (horozontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horozontalInput < -0.01f)
            transform.localScale = new Vector3(-1,1,1);

        if (Input.GetKey(KeyCode.Space) && grounded)
            Jump();
     

        anim.SetBool("run", horozontalInput != 0);
        anim.SetBool("grounded", grounded);

        if (Input.GetMouseButton(0))
            GrappleHook();
    }





    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        grounded = false;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
        else
            grounded = false;



    }
    
    private void GrappleHook()
    {
            mouse = cam.ScreenToWorldPoint(Input.mousePosition);
            body.MovePosition(Vector2.MoveTowards(transform.position, mouse ,Time.deltaTime * speed));
    }

}
