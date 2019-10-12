using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Vector3 p_Movement;
    Quaternion p_Rotation = Quaternion.identity;
    Animator p_Animator;
    Rigidbody p_Rigidbody;
    AudioSource p_FootAudio;
    /// <summary>
    /// 旋转速度
    /// </summary>
    public float rotaSpeed=20f;
    // Start is called before the first frame update
    void Start()
    {
        p_FootAudio = transform.GetComponent<AudioSource>();
        p_Rigidbody = GetComponent<Rigidbody>();
        p_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");//获取横向，最大为1
        float vertical = Input.GetAxis("Vertical");//获取纵向，最大为1

        p_Movement.Set(horizontal,0,vertical);//把横纵向值赋给一个vector3变量
        //这个变量就是一个模向量
        p_Movement.Normalize();//同时向两边前进（斜着前进），那么斜边就大于1（速度会大于直走），把该向量归一化

        //判断动画是否播放
        bool hasHInput = !Mathf.Approximately(horizontal,0);
        bool hasVInput = !Mathf.Approximately(vertical,0);
        bool iswalking = hasHInput || hasVInput;

        p_Animator.SetBool("isWalking",iswalking);
        if (iswalking)
        {
            if (!p_FootAudio.isPlaying)
            p_FootAudio.Play();
        }
        else {
            p_FootAudio.Stop();
        }
        //设置角色旋转
        Vector3 Rota = Vector3.RotateTowards(transform.forward,p_Movement,rotaSpeed*Time.deltaTime,0);
        p_Rotation = Quaternion.LookRotation(Rota);//看向得到的旋转度，产生相应的旋转
    }
    private void OnAnimatorMove()
    {
        //deltaPosition获取动画因为根运动而导致的坐标变化，然后用magnitude获取其中的长度(相当于用了animator自身的位移，用了自己获取的方向)
        p_Rigidbody.MovePosition(p_Rigidbody.position+p_Movement*p_Animator.deltaPosition.magnitude);
        p_Rigidbody.MoveRotation(p_Rotation);
    }
}
