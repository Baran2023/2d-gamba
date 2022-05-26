using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
   [SerializeField] public Camera cam;
    public Vector2 mouse;
    private Vector2 mouseClicked = new Vector2(-0.02f,0.01f);

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
        
        bool reach;
        if (Input.GetMouseButtonDown(0))
            mouseClicked = Input.mousePosition;
            reach = false;
        GrappleHook(mouseClicked, reach);
        
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

        if(collision.gameObject.tag == "Win")
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        if (collision.gameObject.tag == "die")
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);



    }
    
    private void GrappleHook(Vector2 mouseClick, bool reached)
    {
            mouse = cam.ScreenToWorldPoint(mouseClick);
            if (!(cam.transform.position.Equals(mouseClick)) && !reached)
                body.MovePosition(Vector2.MoveTowards(transform.position, mouse,Time.deltaTime * speed * 3));
            if (cam.transform.position.Equals(mouseClick))
                reached = true;
    }

}
