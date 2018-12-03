using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(LineRenderer))]
[ExecuteInEditMode]
public class Water : MonoBehaviour {

    public float speed = 0.1f;
    public int resolution = 50;
    public Material WaterMaterial;

    private WaterParticle[] springs;

    public float Dampening = 0.1f;
    public float Tension = 1;
    public float TargetHeight = 1;
    public float Spread = 0.3f;
    public float splashForce = -2f;

    public float spacing = 1.0f;

    private Mesh waterMesh;
    private Vector3[] vertices = null;
    private int[] triangles = null;
    private Vector3[] uvs;
    private LineRenderer lineRenderer;
    public bool simulating = true;

    // Use this for initialization
    void Start () {
        springs = new WaterParticle[resolution];
        for (int i = 0; i < resolution; i++) {
            springs[i] = new WaterParticle(TargetHeight);
        }
        GetComponent<MeshFilter>().mesh = waterMesh = new Mesh();
        lineRenderer = GetComponent<LineRenderer>();
        waterMesh.name = "Procedural water";
        StartCoroutine(SimulateStep());
        StartCoroutine(Wave());
        //Splash(20, 15);
    }

    // Update is called once per frame
    void Update()
    {
        
        UpdateMesh();
    }


    private void FixedUpdate()
    {
       
    }




    public void Splash(int index, float speed)
    {
        if (index >= 0 && index < springs.Length)
            springs[index].Speed = speed;
    }

    public void UpdateMesh() {
        vertices = new Vector3[(resolution +3)*2];
        Vector3[] surfacevertices = new Vector3[resolution];
        triangles = new int[(resolution-2)* 6];

        //create vertices
        for (int i = 0; i < resolution; i++) {
            //create vertices
            vertices[i*2] = new Vector3(i * spacing,0,0);

            vertices[i*2+1] = new Vector3(i * spacing, (float)springs[i].Height, 0);
            surfacevertices[i] = vertices[i * 2 + 1] + transform.position;
        }
        //create triangles
        for (int i = 0; i < resolution -2 ; i++)
        {

            triangles[i * 6] = i * 2;
            triangles[i * 6 + 1] = i * 2 + 1;
            triangles[i * 6 + 2] = (i + 1) * 2;

            triangles[i * 6 + 3] = (i + 1) * 2;
            triangles[i * 6 + 4] = i * 2 + 1;
            triangles[i * 6 + 5] = (i + 1) * 2 + 1;
        }


        //update water mesh
        waterMesh.vertices = vertices;
        waterMesh.triangles = triangles;
        waterMesh.RecalculateNormals();
        //update line renderer
        lineRenderer.SetVertexCount(resolution);
        lineRenderer.SetPositions(surfacevertices);
    }

    /*private void OnDrawGizmos()
    {
        
        if (vertices == null) return;
        //Debug.Log("Drawing water");
        Gizmos.color = Color.black;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
            //Debug.Log(vertices[i]);
        }
    }*/

    private IEnumerator Wave() {
        while (simulating)
        {
            Splash(Random.Range(0,resolution-1), -0.2f);

            yield return new WaitForSeconds(Random.Range(0.5f,2.0f));
        }
        }

    private IEnumerator SimulateStep() {
        while (simulating)
        {
            //Debug.Log("Simulating step");
            float iniTime = Time.time;
            for (int i = 0; i < springs.Length; i++)
                springs[i].simulate(TargetHeight, Dampening);

            float[] leftDeltas = new float[springs.Length];
            float[] rightDeltas = new float[springs.Length];

            // do some passes where springs pull on their neighbours
            for (int j = 0; j < 8; j++)
            {
                for (int i = 0; i < springs.Length; i++)
                {
                    if (i > 0)
                    {
                        leftDeltas[i] = Spread * (springs[i].Height - springs[i - 1].Height);
                        springs[i - 1].Speed += leftDeltas[i];
                    }
                    if (i < springs.Length - 1)
                    {
                        rightDeltas[i] = Spread * (springs[i].Height - springs[i + 1].Height);
                        springs[i + 1].Speed += rightDeltas[i];
                    }
                }

                for (int i = 0; i < springs.Length; i++)
                {
                    if (i > 0)
                        springs[i - 1].Height += leftDeltas[i];
                    if (i < springs.Length - 1)
                        springs[i + 1].Height += rightDeltas[i];
                }
            }
            float deltaTime = Time.time - iniTime;
            yield return new WaitForSeconds(0.016666f - deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        int colitionPos = (int)Mathf.Clamp(getIndex(other.gameObject.transform.position.x), 0, resolution - 1);
        if (other.tag != "Boat")
        {
           
            // Debug.Log(getIndex(other.gameObject.transform.position.x));
            Splash(colitionPos, splashForce);
        }
        else {
            Splash(colitionPos, -0.5f);
        }
    }

    private float getIndex(float xpos) {
        return Mathf.Abs(transform.position.x - xpos) / spacing;
    }
}
