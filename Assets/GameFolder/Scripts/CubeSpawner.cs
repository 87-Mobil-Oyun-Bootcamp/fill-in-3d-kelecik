using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CubeSpawner : MonoBehaviour
{
    public static CubeSpawner instance;
    [SerializeField]
    List<Texture2D> texture2Ds = new List<Texture2D>();
    [SerializeField]
    List<GameObject> inSceneObject = new List<GameObject>();
    List<GameObject> inpasifObject = new List<GameObject>();
    [SerializeField]
    GameObject cube;
    [SerializeField]
    public float explosionForce, explosionRadius, upwardsModifier;
    private int cubeCount = 0;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        SpawnCube();
        SpawnPasifCube();
    }
    private void Start()
    {
      Invoke("SetRigidbody",3f);
    }

    void SpawnCube()
    {
        for (int t = 0; t < texture2Ds.Count; t++)
        {
            for (int z = 0; z < 16; z++)
            {
                for (int x = 0; x < 16; x++)
                {
                    Color color = texture2Ds[t].GetPixel(x,z);
                    if (color.a == 0)
                    {
                        continue;
                    }
                    GameObject pixelCube = Instantiate(cube, new Vector3((x-texture2Ds[t].width * 0.5f), t+5,z-texture2Ds[t].height * .5f), Quaternion.identity);
                    pixelCube.transform.parent = gameObject.transform;
                    pixelCube.GetComponent<Renderer>().material.color = color;
                    inSceneObject.Add(pixelCube);
                }
            }
        }     
    }
    void SpawnPasifCube()
    {
        for (int t = 0; t < texture2Ds.Count; t++)
        {
            for (int z = 0; z < 16; z++)
            {
                for (int x = 0; x < 16; x++)
                {
                    Color color = texture2Ds[t].GetPixel(x, z);
                    if (color.a == 0)
                    {
                        continue;
                    }
                    byte r = (byte)Random.Range(0, 255);
                    byte g = (byte)Random.Range(0, 255);
                    byte b = (byte)Random.Range(0, 255);
                    GameObject pixelCube = Instantiate(cube, new Vector3((x - texture2Ds[t].width * 0.5f), t + 5, z - texture2Ds[t].height * .5f), Quaternion.identity);
                    pixelCube.transform.parent = gameObject.transform;
                    pixelCube.GetComponent<Renderer>().material.color = color;
                    inpasifObject.Add(pixelCube);
                }
            }
        }
    }

    void SetRigidbody()
    {
        for (int i = 0; i < inSceneObject.Count; i++)
        { 
            var rbCube = inSceneObject[i].GetComponent<Rigidbody>();
            rbCube.isKinematic = false;
            rbCube.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            rbCube.AddExplosionForce(explosionForce, transform.position + new Vector3(0,5,0), explosionRadius, upwardsModifier, ForceMode.Impulse);
        }

        for (int i = 0; i < inpasifObject.Count; i++)
        {
            inpasifObject[i].GetComponent<MeshRenderer>().enabled = false;
            inpasifObject[i].GetComponent<BoxCollider>().enabled = false;
        }
        GameManager.instance.gameStatus = GameStatus.PLAYABLE; // oynanabilir kılıyor;
    }
    public void EnableMeshRenderer(GameObject transportObject)
    {
        GameObject refTransformObject = transportObject;
        inpasifObject[cubeCount].GetComponent<MeshRenderer>().enabled = true;
        inpasifObject[cubeCount].GetComponent<BoxCollider>().isTrigger = true;
        if (refTransformObject != null)
        {
            refTransformObject.transform.DOMove(inpasifObject[cubeCount].transform.position, 0.5f).OnComplete(() => Destroy(refTransformObject));
        }
        cubeCount++;
        if (inpasifObject.Count == cubeCount)
        {
            cubeCount = 0;
        }
    }
}