using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{

    public GameObject bladeTrialPrefab;
    public float minCuttingVelocity = .001f;

    bool isCutting = false;

    GameObject currentBladeTrial;

    Vector2 previousPosition;

    Rigidbody2D rb;
    Camera cam;
    CircleCollider2D circleCollider;

    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartCutting();
        } else if (Input.GetMouseButtonUp(0))
        {
            StopCutting();
        }

        if (isCutting)
        {
            UpdateCut();
        }
        
    }

    void UpdateCut()
    {
        Vector2 newPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        rb.position = newPosition;

        float velocity = (newPosition - previousPosition).magnitude * Time.deltaTime;
        previousPosition = newPosition;

        if (velocity > minCuttingVelocity)
        {
            circleCollider.enabled = true;
        } else
        {
            circleCollider.enabled = false;
        }
    }

    void StartCutting()
    {
        isCutting = true;
        currentBladeTrial = Instantiate(bladeTrialPrefab, transform);
        previousPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        circleCollider.enabled = false;

    }

    void StopCutting()
    {
        isCutting = false;
        currentBladeTrial.transform.SetParent(null);
        Destroy(currentBladeTrial, 2f);
        circleCollider.enabled = false;
    }
}
