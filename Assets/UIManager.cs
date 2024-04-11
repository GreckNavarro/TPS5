using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject textGO;

    private void OnEnable()
    {
        GameManager.Special += ActiveSpecialMns;
        Debug.Log("suscribiendo");
        PlayerController.DesactiveSpecial += DesactiveSpecialMns;
    }

    private void OnDisable()
    {
        GameManager.Special -= ActiveSpecialMns;
        Debug.Log("desuscribiendo");
        PlayerController.DesactiveSpecial -= DesactiveSpecialMns;
    }

    public void ActiveSpecialMns()
    {
        textGO.SetActive(true);
    }

    public void DesactiveSpecialMns()
    {
        //Destroy(textGO);
        textGO.SetActive(false);
    }
}
