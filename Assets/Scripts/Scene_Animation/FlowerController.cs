using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerController : MonoBehaviour
{
    [SerializeField]
    GameObject particle;
    Rigidbody rb;
    State state;
    Vector3 defaultPos;
    Vector3 prevPos;

    enum State{
        start,
        fire,
        end,
        miss
    }

    // Start is called before the first frame update
    void Start()
    {
        state = State.start;
        rb = GetComponent<Rigidbody>();
        particle.SetActive(false);
        defaultPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"),0).normalized;
        var dv = 6f * direction;
        rb.velocity = new Vector3(dv.x, dv.y, 0f);

        if(Input.GetKeyDown("t")){
            if(state == State.start){
                particle.gameObject.SetActive(true);
                state = State.fire;
                Debug.Log(state);
            }
            else if(state == State.fire){
                particle.gameObject.SetActive(false);
                state = State.end;
                Debug.Log(state);
            }
        }

        if(state == State.end){
            transform.position = new Vector3(0,0,0);
            state = State.start;
        }

        prevPos = transform.position;
    }

    void OnMouseDown()
    {
        Debug.Log("mouse down");    
    }

    private void OnMouseDrag() {

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        transform.position = ray.GetPoint(5f)- new Vector3(0f,4f,0f);

        if(Mathf.Abs((prevPos-transform.position).magnitude) > 2f){
            Debug.Log("accel");
            particle.gameObject.SetActive(true);
        }
    }

    void OnMouseEnter()
    {
        Debug.Log("mouse enter");
    }

    void OnMouseExit()
    {
        Debug.Log("mouse exit");
    }

    void OnMouseUp()
    {
        transform.position = defaultPos;
    }
}
