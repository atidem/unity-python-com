using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetMQ;
using NetMQ.Sockets;
using AsyncIO;

public class playerMovement : MonoBehaviour
{

    public Vector3 capsulePoint
    { get; set; }


    CharacterController _controller;

    private HelloRequester _helloRequester;

    [SerializeField]
    float _moveSpeed =5.0f;

    [SerializeField]
    float _jumpSpeed =20.0f;

    [SerializeField]
    float _gravity =1.0f;

    [SerializeField]
    float _yvelocity =0.0f;

    // Start is called before the first frame update
    void Start()
    {
        ////tryna something
        _helloRequester = new HelloRequester();
        _helloRequester.Start();

        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //_helloRequester.point = GetComponent<Transform>();
        

        _helloRequester.Point = GameObject.Find("Capsule").transform.position;
        

        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 velocity = direction * _moveSpeed;

        if (_controller.isGrounded)
        {
            if (Input.GetButton("Jump"))
            {
                _yvelocity = _jumpSpeed;
            }
        }
        else
        {
            _yvelocity -= _gravity;
        }
        velocity.y = _yvelocity;
        _controller.Move(velocity * Time.deltaTime);

        
    }
    private void OnDestroy()
    {
        _helloRequester.Stop();
    }
}
