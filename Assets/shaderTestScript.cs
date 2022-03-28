using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shaderTestScript : MonoBehaviour
{
    private MeshRenderer mesh;

    [SerializeField] private float dissolveSpeed = 0.2f;
    [SerializeField] private float value = 0;
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        //mesh.sharedMaterial.SetFloat("_Dissolve", 0.5f);

        //value += Time.deltaTime;

        //StartCoroutine(Dissolve());

        //Dissolve();
    }

    private void Update()
    {
        value += dissolveSpeed * Time.deltaTime;
        mesh.material.SetFloat("_Dissolve", value);
    }




}
