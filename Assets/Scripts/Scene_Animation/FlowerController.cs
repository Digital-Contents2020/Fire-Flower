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
    // Vector3 respawnPos = new Vector3(0f,1.3f,0f);
    MeshRenderer mr;
    float fadeSpeed = 0.002f;
    float alfa;
    bool isActive = false;
    bool isFadeIn = false;
    bool isFadeOut = false;
    bool isRespawn = false;

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
        mr = GetComponent<MeshRenderer>();
        alfa = mr.material.color.a;
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive){
            var direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"),0).normalized;
            var dv = 6f * direction;
            rb.velocity = new Vector3(dv.x, dv.y, 0f);

        }

        // if(Input.GetKeyDown("t")){
        //     if(state == State.start){
        //         particle.gameObject.SetActive(true);
        //         state = State.fire;
        //         Debug.Log(state);
        //     }
        //     else if(state == State.fire){
        //         particle.gameObject.SetActive(false);
        //         state = State.end;
        //         Debug.Log(state);
        //     }
        // }

        if(state == State.end){
            transform.position = new Vector3(0,0,0);
            state = State.start;
        }

        if(isFadeOut) FadeOut();
        if(isFadeIn) FadeIn();
        if(isRespawn) Respawn();

        prevPos = transform.position;
    }

    void OnMouseDown()
    {
        Debug.Log("mouse down");    
    }

    private void OnMouseDrag() {
        if(isActive){

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 pos = ray.GetPoint(5f);
            transform.position = new Vector3(pos.x, pos.y-4f, 0f);

            if(Mathf.Abs((prevPos-transform.position).magnitude) > 1f){
                Debug.Log("accel");
                BurnOut();
            }
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
        // transform.position = defaultPos;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Fire"){
            Ignition();
        }
        else if(other.tag == "Water"){
            BurnOut();
        }
    }

    //火が付いたとき
    void Ignition(){
        particle.SetActive(true);
    }

    // 火が消えたとき
    void BurnOut(){
        particle.SetActive(false);
        isActive = false;
        if(!isRespawn) isFadeOut=true;
    }

    void Respawn(){
        transform.position = defaultPos;
        this.gameObject.SetActive(true);
        isFadeIn = true;
        isRespawn = false;
    }

    public void FadeOut(){
        alfa = alfa - fadeSpeed;
        mr.material.color = mr.material.color - new Color(0,0,0,fadeSpeed);
        if(alfa <= 0f){
            isFadeOut = false;
            isRespawn = true;
        }
    }

    public void FadeIn(){
        alfa = alfa + fadeSpeed;
        mr.material.color = mr.material.color + new Color(0,0,0,fadeSpeed);
        if(alfa >= 1f){
            isFadeIn = false;
            isActive = true;
        }
    }
}
