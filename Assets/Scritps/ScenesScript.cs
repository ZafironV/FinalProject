using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonIrAEscena : MonoBehaviour
{
    public void CargarEscena(string nombreDeEscena)
    {
        SceneManager.LoadScene(nombreDeEscena);
    }
}

