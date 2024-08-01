using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LogDisplay : MonoBehaviour
{
    public Text logText;
    private string logString = "";
    public float clearInterval = 3.0f; // Intervalo en segundos para limpiar el texto

    private void OnEnable()
    {
        Application.logMessageReceived += LogCallback;
    }

    private void OnDisable()
    {
        Application.logMessageReceived -= LogCallback;
    }

    void LogCallback(string logString, string stackTrace, LogType type)
    {
        this.logString += logString + "\n";
        logText.text = this.logString;

        // Si no hay una corutina corriendo, iniciar la corutina para limpiar el texto
        if (!IsInvoking("ClearLog"))
        {
            InvokeRepeating("ClearLog", clearInterval, clearInterval);
        }
    }

    void ClearLog()
    {
        // Limpiar el texto y detener la corutina de limpieza si ya no hay mensajes
        if (!string.IsNullOrEmpty(logText.text))
        {
            logString = ""; // Limpiar el texto
            logText.text = logString;
        }
        else
        {
            CancelInvoke("ClearLog"); // Detener la corutina si el texto está vacío
        }
    }
}
