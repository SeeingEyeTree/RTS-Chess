using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class selector_b : MonoBehaviour
{
    

    public int x_borad=3;
    public int y_borad=0;
    public GameObject controler;
    public float x_space;
    public float y_space;
    public string player;

    //
    private void Start()
    {
        if (player == "b")
        {
            this.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }


    public void Update()
    {
        controler = GameObject.FindGameObjectWithTag("GameController");
        game sc = controler.GetComponent<game>();
        if(player == "w")
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                //Debug.Log("W");
                if (sc.pos_on_board(x_borad, y_borad + 1))
                {
                    y_borad += 1;
                }

            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                //Debug.Log("S");
                if (sc.pos_on_board(x_borad, y_borad - 1))
                {
                    y_borad -= 1;
                }
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                //Debug.Log("D");
                if (sc.pos_on_board(x_borad + 1, y_borad))
                {
                    x_borad += 1;
                }
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                //Debug.Log("A");
                if (sc.pos_on_board(x_borad - 1, y_borad))
                {
                    x_borad -= 1;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                GameObject unit = sc.get_pos(x_borad, y_borad);
                GameObject move_plat = sc.get_plat(x_borad, y_borad);
                if (unit != null && unit.GetComponent<unit_script>().player == player)
                {
                    unit.GetComponent<unit_script>().do_things("w");
                }
                if (move_plat != null && move_plat.GetComponent<show_moves>().tag == "mp_" + player)
                {
                    Debug.Log("move plate selectred");
                    move_plat.GetComponent<show_moves>().move_plate_click("w");
                }


            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                //Debug.Log("W");
                if (sc.pos_on_board(x_borad, y_borad + 1))
                {
                    y_borad += 1;
                }

            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                //Debug.Log("S");
                if (sc.pos_on_board(x_borad, y_borad - 1))
                {
                    y_borad -= 1;
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                //Debug.Log("D");
                if (sc.pos_on_board(x_borad + 1, y_borad))
                {
                    x_borad += 1;
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                //Debug.Log("A");
                if (sc.pos_on_board(x_borad - 1, y_borad))
                {
                    x_borad -= 1;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject unit = sc.get_pos(x_borad, y_borad);
                GameObject move_plat=sc.get_plat(x_borad,y_borad);
                if (unit != null && unit.GetComponent<unit_script>().player==player)
                {
                    unit.GetComponent<unit_script>().do_things("b");
                }
                if (move_plat != null && move_plat.GetComponent<show_moves>().tag=="mp_"+player)
                {
                    Debug.Log("move plate selectred");
                    move_plat.GetComponent<show_moves>().move_plate_click("b");
                }


            }
        }

        x_space = x_borad * 0.66f;
        x_space -= 2.3f;
        y_space = y_borad * 0.66f;
        y_space -= 2.33f;

        transform.position=new Vector3(x_space, y_space, -3);
    }
}
