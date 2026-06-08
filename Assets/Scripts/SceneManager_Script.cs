using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager_Script : MonoBehaviour
{
    [SerializeField] float TempoTransicao;
    [SerializeField] Animator TelaTransicao;

    private void Start()
    {
        //TelaTransicao = GetComponent<Animator>();
    }
    public void SceneChange()
    {
        StartCoroutine(Transicao());
    }

    IEnumerator Transicao()
    {
        TelaTransicao.SetBool("On", true);
        yield return new WaitForSeconds(TempoTransicao);
        SceneManager.LoadScene(1);
    }
}
