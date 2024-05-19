using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int rewindLimit = 3;
    public float rewindTime = 2f;

    private int rewindsRemaining;
    private List<Vector3> pastPositions = new List<Vector3>();
    private bool isRewinding = false;
    private bool isTimeStopped = false;
    public DialogueManager dialogueManager;

    void Start()
    {
        rewindsRemaining = rewindLimit;
    }

    void Update()
    {
        if (!dialogueManager.DialogueActive)
        {
            // Movement
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(horizontal, 0f, vertical) * moveSpeed * Time.deltaTime;
            transform.Translate(movement);
        }

        // Time Manipulation
        if (Input.GetKeyDown(KeyCode.R) && rewindsRemaining > 0 && !isRewinding)
        {
            StartCoroutine(Rewind());
            rewindsRemaining--;
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            isTimeStopped = !isTimeStopped;
            Time.timeScale = isTimeStopped ? 0f : 1f;
        }
    }

    private IEnumerator Rewind()
    {
        isRewinding = true;
        for (int i = pastPositions.Count - 1; i >= 0; i--)
        {
            transform.position = pastPositions[i];
            pastPositions.RemoveAt(i);
            yield return new WaitForSeconds(rewindTime / pastPositions.Count);
        }
        isRewinding = false;
    }

    private void FixedUpdate()
    {
        if (!isRewinding && !isTimeStopped)
        {
            pastPositions.Add(transform.position);
            if (pastPositions.Count > Mathf.RoundToInt(rewindTime / Time.fixedDeltaTime))
            {
                pastPositions.RemoveAt(0);
            }
        }
    }
}