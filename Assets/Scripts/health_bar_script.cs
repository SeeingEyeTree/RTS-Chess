using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health_bar_script : MonoBehaviour
{
    GameObject controller;
    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
    }

    void Update()
    {

        float current_hp = this.GetComponent<unit_script>().HP;
        float max_hp= this.GetComponent<unit_script>().HP_max;
        float precent_hp = current_hp/max_hp;
        if(current_hp == max_hp)
        {
            this.GetComponent<SpriteRenderer>().enabled=false;// put in backgroond so you can't so cant see
        }
        else if (1 <= precent_hp && precent_hp >= 0.75)
        {
            this.GetComponent<SpriteRenderer>().enabled = true;
            this.GetComponent<SpriteRenderer>().color = Color.green;

        }
        else if (0.75 <= precent_hp && precent_hp >= 0.5)
        {
            this.GetComponent<SpriteRenderer>().enabled = true;
            this.GetComponent<SpriteRenderer>().color = Color.yellow;

        }
        else if (0.5 <= precent_hp && precent_hp >= 0.25)
        {
            this.GetComponent<SpriteRenderer>().enabled = true;
            this.GetComponent<SpriteRenderer>().color = new Color(255f, 140f, 0f,1f);

        }
        else if (0.25 <= precent_hp && precent_hp >= 0)
        {
            this.GetComponent<SpriteRenderer>().enabled = true;
            this.GetComponent<SpriteRenderer>().color = Color.red;

        }
    }
}
