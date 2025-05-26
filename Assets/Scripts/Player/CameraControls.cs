using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    [Header("References")]
    [SerializeField] ParticleSystem fxZoomEffect;

    [Header("Camera Settings")]
    [SerializeField] float minFOV = 30f;
    [SerializeField] float maxFOV = 60f; 
    [SerializeField] float zoomDuration = 0.5f; // Duration of the FOV change
    CinemachineCamera cinemachineCamera;

    void Awake()
    {
        cinemachineCamera = GetComponent<CinemachineCamera>();
    }

    public void ChangeFOV(float newSpeed)
    {
        StopAllCoroutines(); // Stop any ongoing FOV change coroutine, prevents overlapping FOV changes
        StartCoroutine(ChangeFOVco(newSpeed));

        if (newSpeed > 0)
        {
            fxZoomEffect.Play();
        }

    }

    IEnumerator ChangeFOVco(float newSpeed)
    {
        float startFOV = cinemachineCamera.Lens.FieldOfView;
        float targetFOV = Mathf.Clamp(startFOV + newSpeed, minFOV, maxFOV); 

        float elapsedTime = 0f; 

        while (elapsedTime < zoomDuration)
        {
            elapsedTime += Time.deltaTime; 

            float t = elapsedTime / zoomDuration;

            float newFOV = Mathf.Lerp(startFOV, targetFOV, t);
            cinemachineCamera.Lens.FieldOfView = newFOV;

            yield return null; // Wait for the next frame
        }
        
        cinemachineCamera.Lens.FieldOfView = targetFOV; // Ensure the final FOV is set to the target

    }


}
