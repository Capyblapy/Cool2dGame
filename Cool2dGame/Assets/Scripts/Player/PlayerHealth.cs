using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class PlayerHealth : MonoBehaviour
{
    public float Health = 100f;
    public GameObject DeathHUD;
    [SerializeField] TMP_Text HealthUI;
    // Start is called before the first frame update
    void Start()
    {
        DeathHUD.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 100f/2)
            HealthUI.text = "<color=red>" + Health.ToString() + "</color>" + "/" + 100f.ToString();
        else
            HealthUI.text = Health.ToString() + "/" + 100f.ToString();

        if (Health <= 0)
        {
            DeathHUD.SetActive(true);
            gameObject.GetComponent<PlayerControler>().enabled = false;
            HealthUI.text = "</color> Health: " + "<color=red>" + 0.ToString();
        }
        else
        {
            if (Health <= 100f / 4)
                HealthUI.text = "</color> Health: " + "<color=red>" + Health.ToString();
            else
                HealthUI.text = "</color> Health: " + Health.ToString();
        }
    }
     public void OnClick() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
