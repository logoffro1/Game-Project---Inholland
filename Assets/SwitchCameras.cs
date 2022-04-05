using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCameras : MonoBehaviour
{
    public GameObject cameraPrefab;
    private GameObject instantiation;
    private Vector3 target;

    void Start()
    {
        instantiation = Instantiate(cameraPrefab);
        target = cameraPrefab.transform.position;
        target.x += 10f;
    }
    private void Update()
    {
        if (instantiation != null) instantiation.transform.position = Vector3.MoveTowards(transform.position, target, 2 * Time.deltaTime);
    }

    public void DestroyAllRelated()
    {
        Destroy(instantiation);
        //DestroyImmediate(this, true);
    }

    /*
    public List<Vector3> locations;
    public List<Quaternion> rotations;

    private int startingAmountSeconds;
    private float amountLeft;
    public List<float> CutOffs;

    // Start is called before the first frame update
    void Start()
    {
        startingAmountSeconds = FindObjectOfType<TimerCountdown>().StartCountDownLeft;
        amountLeft = 0;
        CutOffs = new List<float>();

        for (int i = 0; i < locations.Count + 1; i++)
        {
            CutOffs.Add((startingAmountSeconds / locations.Count) * i);
        }

        Instantiate(cameraPrefab);

        StartCoroutine(SwitchCameraPlacement());
    }

    public void SwitchCameraLocationAtCutoff(float cutoff)
    {
        if(locations.Count <= 0)
        {
            Destroy(cameraPrefab);
            Destroy(this);
        }
        else
        {
            cameraPrefab.transform.position = locations[0];
            cameraPrefab.transform.rotation = rotations[0];

            CutOffs.RemoveAt(0);
            locations.RemoveAt(0);
            rotations.RemoveAt(0);
        }
    }

 
    private IEnumerator SwitchCameraPlacement()
    {
        while (amountLeft <= startingAmountSeconds && CutOffs.Count > 0)
        {
            yield return new WaitForSeconds(0.5f);

            amountLeft += 0.5f;

            if (amountLeft <= CutOffs[0])
            {   
                if (CutOffs.Count == 1)
                {
                    Destroy(cameraPrefab);
                    yield break;
                }
                else
                {
                    cameraPrefab.transform.position = locations[0];
                    cameraPrefab.transform.rotation = rotations[0];

                    CutOffs.RemoveAt(0);
                    locations.RemoveAt(0);
                    rotations.RemoveAt(0);
                }
            }
        }

    */

}
