using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIFollowCamera : MonoBehaviour
{
    Camera camera;
    [SerializeField] float speed = 0.8f;

    [SerializeField] TextMeshProUGUI textMesh;
    public GameObject mainMenuUI;
    public GameObject loadingUI;
    public GameObject winUI;
    public GameObject loseUI;

    void Start()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        float angleDif = Quaternion.Angle(transform.rotation, camera.transform.rotation);
        var step = (angleDif / speed) * Time.unscaledDeltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, new Quaternion(0, camera.transform.rotation.y, 0, camera.transform.rotation.w), step);
    }

    public void SetLoadingText(string text)
    {
        textMesh.text = text;
    }


    public void ShowLose()
    {
        loseUI.SetActive(false);
    }

    public void ShowWin()
    {
        winUI.SetActive(true);
    }


}
