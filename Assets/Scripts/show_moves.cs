using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class show_moves : MonoBehaviour
{
    public GameObject controler;

    GameObject parent_ref = null;

    //bord pos
    int mat_x;
    int mat_y;

    // false can move true attacking
    public bool attack = false;

    public void Start()
    {
        if (attack)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }


    public void move_plate_click(string player_pass)
    {

        controler = GameObject.FindGameObjectWithTag("GameController");
        
        /*
        if (attack)
        {
            GameObject cp = controler.GetComponent<game>().get_pos(mat_x, mat_y);
            Destroy(cp);
        }
        */

        controler.GetComponent<game>().set_empty(parent_ref.GetComponent<unit_script>().get_x(), 
                                                 parent_ref.GetComponent<unit_script>().get_y());
        if(!attack)
        {
            parent_ref.GetComponent<unit_script>().set_x_cord(mat_x);
            parent_ref.GetComponent<unit_script>().set_y_cord(mat_y);
        }
        
        parent_ref.GetComponent<unit_script>().set_cords(parent_ref.GetComponent<unit_script>().direction);

        controler.GetComponent<game>().set_pos(parent_ref);
        //controler.GetComponent<game>().set_plat(gameObject,mat_x,mat_y);

        parent_ref.GetComponent<unit_script>().destroy_show(player_pass);

    }

    public void set_show_cords(int x, int y)
    {
        mat_x= x;
        mat_y= y;
        controler = GameObject.FindGameObjectWithTag("GameController");
        game sc =controler.GetComponent<game>();
        sc.get_plat(x, y);
    }


    public void set_ref(GameObject obj)
    {
        parent_ref = obj;
    }
    

    public GameObject get_ref()
    {
        return parent_ref;
    }

    public int mp_x()
    {
        return mat_x;
    }
    public int mp_y()
    {
        return mat_y;
    }
}

