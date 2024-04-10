using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;

public class game : MonoBehaviour
{

    public GameObject unit;
    public GameObject selctor_b;

    private GameObject[,] pos = new GameObject[8,8];
    private GameObject[] b_player = new GameObject[16];
    private GameObject[] w_player = new GameObject[16];
    private GameObject[,] move_plates = new GameObject[8,8];

    private string player = "w";

    private bool gg = false;

    // Start is called before the first frame update
    void Start()
    {
        /*
        for(int i=0; i<8; i++)
        {
            for (int j=0; j < 8; j++)
            {
                pos[i, j] = null;
            }
        }*/

        GameObject playwer_w_sel=Instantiate(selctor_b);
        playwer_w_sel.GetComponent<selector_b>().x_borad = 3;
        playwer_w_sel.GetComponent<selector_b>().y_borad = 0;
        playwer_w_sel.GetComponent<selector_b>().player = "w";
        GameObject playwer_b_sel = Instantiate(selctor_b);
        playwer_b_sel.GetComponent<selector_b>().x_borad = 3;
        playwer_b_sel.GetComponent<selector_b>().y_borad = 7;
        playwer_b_sel.GetComponent<selector_b>().player = "b";

        w_player = new GameObject[]
        {
            create("w_rook",0,0),
            create("w_rook",7,0),
            create("w_knight",6,0),
            create("w_knight",1,0),
            create("w_queen",3,0),
            create("w_king",4,0),
            create("w_bish",5,0),
            create("w_bish",2,0),
            
            create("w_pawn",0,1),
            create("w_pawn",1,1),
            create("w_pawn",2,1),
            create("w_pawn",3,1),
            create("w_pawn",4,1),
            create("w_pawn",5,1),
            create("w_pawn",6,1),
            create("w_pawn",7,1),
            
           
        };
        /*
        for(int i=0; i < 8; i++)
        {
            w_player.Append(create("w_pawn", i, 1));
        }*/

        b_player = new GameObject[]
        {
            
            create("b_pawn",0,6),
            create("b_pawn",1,6),
            create("b_pawn",2,6),
            create("b_pawn",3,6),
            create("b_pawn",4,6),
            create("b_pawn",5,6),
            create("b_pawn",6,6),
            create("b_pawn",7,6),
            
            create("b_rook",0,7),
            create("b_rook",7,7),
            create("b_knight",6,7),
            create("b_knight",1,7),
            create("b_queen",3,7),
            create("b_king",4,7),
            create("b_bish",5,7),
            create("b_bish",2,7),
        };
        /*
        for (int i = 0; i < 8; i++)
        {
            b_player.Append(create("b_pawn", i, 6));
        }
        */
        for (int i = 0; i < w_player.Length; i++)
        {
            set_pos(w_player[i]);
            set_pos(b_player[i]);
        }
    }

    public GameObject create(string name, int x, int y)
    {
        GameObject obj = Instantiate(unit, new Vector3(0,0-1), Quaternion.identity);
        unit_script us= obj.GetComponent<unit_script>();
        us.name = name;
        us.set_x_cord(x);
        us.set_y_cord(y);
        us.set_sprite();
        return obj;
    }

    public void set_pos(GameObject obj)
    {
        unit_script us = obj.GetComponent<unit_script>();
        pos[us.get_x(), us.get_y()] = obj;
    }

    public void set_empty(int x, int y)
    {
        pos[x,y]=null;
    }

    public GameObject get_pos(int x, int y)
    {
        return pos[x,y];
    }

    public void set_plat(GameObject obj, int x,int y)
    {
        Debug.Log(x + " " + y +"plat loc");
        move_plates[x, y] = obj;
    }

    public void set_plat_empty(int x, int y)
    {
        move_plates[x, y] = null;
    }

    public GameObject get_plat(int x, int y)
    {
        return move_plates[x,y];
    }

    public bool pos_on_board(int x, int y)
    {
        if (x < 0 || y < 0 || x >= pos.GetLength(0) || y >= pos.GetLength(1))
        {
            return false;
        }
        else
        {
            return true;
        }
       
    }
}
