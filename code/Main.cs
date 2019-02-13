using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets;
public class Main : MonoBehaviour
{
    public bool myLock = false;

    void Start()
    { 
        
    }



    void OnGUI()
    {
        if (GUILayout.Button("Rotate"))
        {
            Object Script = GameObject.Find("obj").GetComponent("CubeScript");
            if (Script != null) return;
            GameObject.Find("obj").AddComponent<CubeScript>();
        }
        if (GUILayout.Button("Stop"))
        {
            GameObject gameObject1 = GameObject.Find("obj");
            Object Script = gameObject1.GetComponent("CubeScript");
            Destroy(Script);
        }
    }
    public void OnSelectChanged()
    {
        myLock = true;
        Dropdown dd = GameObject.Find("Dropdown").GetComponent<Dropdown>();
        Transform tf = GameObject.Find(dd.options[dd.value].text).GetComponent<Transform>();
        GameObject.Find("Slider_x").GetComponent<Slider>().value = tf.localEulerAngles.x/ (float)360;
        GameObject.Find("Slider_y").GetComponent<Slider>().value = tf.localEulerAngles.y / (float)360;
        GameObject.Find("Slider_z").GetComponent<Slider>().value = tf.localEulerAngles.z/ (float)360;
        Debug.Log(tf.localEulerAngles.x+" "+tf.localEulerAngles.x + " "+ tf.localEulerAngles.z);
        myLock = false;
        //  .GetComponent<Dropdown>().value * 360;
    }
    public void OnSliderChanged()
    {
        if (myLock) return;
        Dropdown dd = GameObject.Find("Dropdown").GetComponent<Dropdown>();
        Transform tf = GameObject.Find(dd.options[dd.value].text).GetComponent<Transform>();
        tf.localEulerAngles = new Vector3(GameObject.Find("Slider_x").GetComponent<Slider>().value * (float)360,
            GameObject.Find("Slider_y").GetComponent<Slider>().value * (float)360,
            GameObject.Find("Slider_z").GetComponent<Slider>().value * (float)360);

        Debug.Log(tf.localEulerAngles.x + " " + tf.localEulerAngles.x + " " + tf.localEulerAngles.z);
    }

    void Update()
    {

    }
}