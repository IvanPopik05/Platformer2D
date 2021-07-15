using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverArm : MonoBehaviour
{
    [SerializeField] private Vector2 VectorWithY;
    private Finish _finish;
    private Vector2 _addVector;
    private void Start()
    {
        _finish = GameObject.FindGameObjectWithTag("Finish").GetComponent<Finish>();
        _addVector = new Vector2(transform.position.x, transform.position.y + VectorWithY.y);
    }

    public void ActivateLeverArm() 
    {
        _finish.Activate();
    }
    private void Update()
    {
        if (_finish.IsActiveLevel) 
        {
            //transform.rotation = Quaternion.Slerp(transform.rotation, new Quaternion(transform.rotation.x,transform.rotation.y,Mathf.Deg2Rad * -221,-1),10 * Time.deltaTime);

            Vector3 a = transform.position;
            Vector3 b = new Vector3(transform.position.x, _addVector.y, transform.position.z);
            transform.position = Vector3.Lerp(a, b, Time.deltaTime * 10);
        }
    }

}
