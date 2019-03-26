using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClapScript : MonoBehaviour
{

    public float right_arm_clap_point; //rest and clap points based on z rotation
    public float right_arm_rest_point;
    public float left_arm_clap_point;
    public float left_arm_rest_point;
    public float clap_animation_rate;

    public GameObject Body;
    public GameObject RightArm;
    public GameObject LeftArm;

    public Quaternion temporary_right_arm_rotation;
    

    // Start is called before the first frame update
    void Start()
    {
        Body = this.gameObject;
        RightArm = Body.transform.GetChild(1).gameObject;
        temporary_right_arm_rotation = RightArm.transform.rotation;

        LeftArm = Body.transform.GetChild(2).gameObject;
        Debug.Log(RightArm);

        right_arm_rest_point = 1.2f;
        left_arm_rest_point = -1.2f;
        right_arm_clap_point = -19.35f;
        left_arm_clap_point = 17.4f;
        clap_animation_rate = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (temporary_right_arm_rotation.y > right_arm_clap_point)
        {
            temporary_right_arm_rotation.y -= clap_animation_rate;
        }
    }
}
