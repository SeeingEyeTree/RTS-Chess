using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class unit_script : MonoBehaviour
{
    // ref 
    public GameObject controler;
    public GameObject show_move_obj;
    public GameObject hp_bar_disp;
    public GameObject hp_bar_self;
    public GameObject arrow;

    //pos
    private int x_bord=-1;
    private int y_bord=-1;
    private float x_space;
    private float y_space;
    private int skip=0;

    // black or white
    public string player;

    //ref sprits
    public Sprite b_king, b_pawn, b_kight, b_bish, b_queen,b_rook;
    public Sprite w_king, w_pawn, w_kight, w_bish, w_queen,w_rook;

    //all unit stats
    public float move_delay;
    public float heal_precent;
    public float quenn_hp;
    public float queen_DPS;
    public float pawn_hp;
    public float pawn_DPS;
    public float rook_hp;
    public float rook_DPS;
    public float bish_hp;
    public float bish_DPS;
    public float hourse_hp;
    public float hourse_DPS;
    public float king_hp;
    public float king_DPS;



    // this unit stats
    public float HP;
    public float HP_max;
    public float DPS;
    public bool in_motion = false;

    // trake conditions
    public bool is_attacked1=false;
    public bool is_attacked2=false;
    public bool can_move = true;
    public bool can_be_attcked = true;
    public Vector3 direction;

    public void set_sprite()
    {
        controler = GameObject.FindGameObjectWithTag("GameController");

        switch (this.name)
        {
            case "b_queen": this.GetComponent<SpriteRenderer>().sprite = b_queen; player="b";
                HP = quenn_hp;
                DPS =queen_DPS;
                break;
            case "b_king": this.GetComponent<SpriteRenderer>().sprite = b_king; player = "b"; 
                HP=king_hp; DPS =king_DPS;
                break;
            case "b_pawn": this.GetComponent<SpriteRenderer>().sprite = b_pawn; player = "b";
                HP = pawn_hp;
                DPS = pawn_DPS;
                break;

            case "b_knight": this.GetComponent<SpriteRenderer>().sprite = b_kight; player = "b"; 
                HP = hourse_hp; DPS = hourse_DPS;
                break;
            case "b_rook": this.GetComponent<SpriteRenderer>().sprite = b_rook; player = "b"; 
                HP=rook_hp; DPS = rook_DPS;
                break;
            case "b_bish": this.GetComponent<SpriteRenderer>().sprite = b_bish; player = "b"; 
                HP=bish_hp; DPS = bish_DPS;
                break;

            case "w_king": this.GetComponent<SpriteRenderer>().sprite = w_king; player = "w";
                HP = king_hp;DPS= king_DPS;
                break;
            case "w_pawn": this.GetComponent<SpriteRenderer>().sprite = w_pawn; player = "w";
                HP = pawn_hp;
                DPS= pawn_DPS;
                break;
            case "w_knight": this.GetComponent<SpriteRenderer>().sprite = w_kight; player = "w";
                HP = hourse_hp;DPS = hourse_DPS;
                break;
            case "w_queen": this.GetComponent<SpriteRenderer>().sprite = w_queen; player = "w";
                HP = quenn_hp;DPS = queen_DPS;
                break;
            case "w_rook": this.GetComponent<SpriteRenderer>().sprite = w_rook; player = "w"; 
                HP= rook_hp;DPS= rook_DPS;
                break;
            case "w_bish": this.GetComponent<SpriteRenderer>().sprite = w_bish; player = "w"; 
                HP=bish_hp;DPS= bish_DPS;
                break;
          
        }
        HP_max = HP;
        set_cords(Vector3.back,true);
    }

    public void set_cords(Vector3 go_direction,bool begin=false)
    {
        float x = x_bord;
        float y = y_bord;

        x *= 0.66f;
        y *= 0.66f;
        x -= 2.33f;
        y -= 2.33f;
        x_space = x;
        y_space= y;
        //Debug.Log(x_space + " " + y_space);
        if (begin)
        {
            this.transform.position=new Vector3(x, y, -2);
        }
        else
        {
            this.transform.position =transform.position+ go_direction / move_delay / 0.75f*Time.deltaTime;// make move dealy ~secounds
        }
        

    }

    public int get_x()
    {
        return x_bord;
    }

    public int get_y()
    {
        return y_bord;
    }

    public void set_x_cord(int x)
    {
        x_bord = x;
    }

    public void set_y_cord(int y)
    {
        y_bord = y;
    }


    private void OnMouseUp()
    {
        //destroy_show();
        //show_move();
        //Debug.Log(HP);
    }

    public void do_things(string player_pass)
    {
        destroy_show(player_pass);
        show_move();
        //Debug.Log(HP);
    }

    public void destroy_show(string player_pass)
    {
        game sc = controler.GetComponent<game>();
        GameObject[] move_plats= GameObject.FindGameObjectsWithTag("mp_"+player_pass);
        for(int i = 0;i< move_plats.Length; i++)
        {
            Destroy(move_plats[i]);
            sc.set_plat_empty(move_plats[i].GetComponent<show_moves>().mp_x(), move_plats[i].GetComponent<show_moves>().mp_y());

        }
    }


    public void show_move()
    {
        // why cant you do this.name[2,5] to get only some elements?
        if (can_move)
        {
            switch (this.name)
            {
                case "b_pawn":
                    if (y_bord == 6)
                    {
                        pawn_plat(x_bord, y_bord - 1);
                        pawn_plat(x_bord, y_bord - 2);
                    }
                    else
                    {
                        pawn_plat(x_bord, y_bord - 1);
                    }
                    break;
                case "w_pawn":
                    if (y_bord == 1)
                    {
                        pawn_plat(x_bord, y_bord + 1);
                        pawn_plat(x_bord, y_bord + 2);
                    }
                    else
                    {
                        pawn_plat(x_bord, y_bord + 1);
                    }
                    break;
                case "w_rook":
                case "b_rook":
                    //Debug.Log("rook");
                    rook_plat();
                    break;
                case "b_queen":
                case "w_queen":
                    //Debug.Log("quuen");
                    rook_plat();
                    bish_plat();
                    break;
                case "b_knight":
                case "w_knight":
                    //Debug.Log("kight");
                    L_plat();
                    break;
                case "b_bish":
                case "w_bish":
                    bish_plat();
                    //Debug.Log("bis");
                    break;
                case "b_king":
                case "w_king":
                    all_round_plat();
                    break;
                    


            }
        }
    }


  

    //////////////////////////////////////////////////////////////////////////
    // Spawn plate patterns
    public void spawn_plat_m(int mat_x, int mat_y, bool is_attack=false)
    {
        float x = mat_x;
        float y = mat_y;

        x *= 0.66f; y *= 0.66f;
        x += -2.3f; y += -2.3f;

        GameObject mp = Instantiate(show_move_obj, new Vector3(x, y, -0.1f),Quaternion.identity);
        mp.tag = "mp_"+ player;
        if (player == "w")
        {
            mp.GetComponent<SpriteRenderer>().color = Color.black;
        }
        else
        {
            mp.GetComponent<SpriteRenderer>().color = Color.green;
        }
       
        show_moves m_script = mp.GetComponent<show_moves>();
        m_script.set_ref(gameObject);
        m_script.attack = is_attack;
        m_script.set_show_cords(mat_x,mat_y);
        game sc = controler.GetComponent<game>();
        sc.set_plat(mp,mat_x, mat_y);

    }

    public void all_round_plat()
    {
        point_plat(x_bord - 1,y_bord - 1);
        point_plat(x_bord - 1, y_bord);
        point_plat(x_bord - 1, y_bord + 1);
        point_plat(x_bord + 1, y_bord - 1);
        point_plat(x_bord + 1, y_bord);
        point_plat(x_bord + 1, y_bord + 1);
        point_plat(x_bord, y_bord - 1);
        point_plat(x_bord, y_bord + 1);
    }

    public void all_round_RTS()
    {
        point_RTS(x_bord - 1, y_bord - 1);
        point_RTS(x_bord - 1, y_bord);
        point_RTS(x_bord - 1, y_bord + 1);
        point_RTS(x_bord + 1, y_bord - 1);
        point_RTS(x_bord + 1, y_bord);
        point_RTS(x_bord + 1, y_bord + 1);
        point_RTS(x_bord, y_bord - 1);
        point_RTS(x_bord, y_bord + 1);
    }

    public void point_plat(int x,int y)
    {
        //controler = GameObject.FindGameObjectWithTag("GameController");
        game sc = controler.GetComponent<game>();

        if (sc.pos_on_board(x, y))
        {
            GameObject cp = sc.get_pos(x, y);
            if(cp == null)
            {
                spawn_plat_m(x, y);
            }
            else if(cp.GetComponent<unit_script>().player != player)
            {
                spawn_plat_m(x, y,true);
            }
        }
    }

    public void point_RTS(int x, int y)
    {
        //controler = GameObject.FindGameObjectWithTag("GameController");
        game sc = controler.GetComponent<game>();
        
        if (sc.pos_on_board(x, y) && sc.get_pos(x, y) !=null)
        {
            GameObject cp = sc.get_pos(x, y);
            if (!cp.GetComponent<unit_script>().in_motion && cp!=null && !in_motion)
            {
                if (cp != null && cp.GetComponent<unit_script>().player != player)
                {
                    cp.GetComponent<unit_script>().HP -= DPS * Time.deltaTime;
                }
                else if (cp != null && cp.GetComponent<unit_script>().player == player && cp.GetComponent<unit_script>().HP < cp.GetComponent<unit_script>().HP_max)
                {
                    cp.GetComponent<unit_script>().HP += DPS * heal_precent * Time.deltaTime;
                }
            }

        }
    }


    public void line_move(int x_iter, int y_iter)
    {

        game sc = controler.GetComponent<game>();

        int x = x_bord+ x_iter;
        int y = y_bord+ y_iter;
        while (sc.pos_on_board(x, y) && (sc.get_pos(x, y) == null))
        {
            spawn_plat_m(x, y);
            x += x_iter;
            y += y_iter;

        }

        if(sc.pos_on_board(x,y) && sc.get_pos(x, y).GetComponent<unit_script>().player != player)
        {
            spawn_plat_m(x, y,true);
        }

    }

    public void line_move_rts(int x_iter, int y_iter)
    {

        game sc = controler.GetComponent<game>();

        int x = x_bord + x_iter;
        int y = y_bord + y_iter;
        while (sc.pos_on_board(x, y) && (sc.get_pos(x, y) == null))
        {
            x += x_iter;
            y += y_iter;
        }
        
        if (sc.pos_on_board(x,y))
        {
            GameObject unit = sc.get_pos(x, y);
            if (!unit.GetComponent<unit_script>().in_motion && !in_motion)
            {
                if (sc.pos_on_board(x, y) && unit.GetComponent<unit_script>().player != player)
                {
                    if (!unit.GetComponent<unit_script>().in_motion)
                    {
                        unit.GetComponent<unit_script>().HP -= DPS * Time.deltaTime;
                        //unit.GetComponent<unit_script>().hp_bar();
                    }
                }
                else if (sc.pos_on_board(x, y) && unit.GetComponent<unit_script>().player == player && unit.GetComponent<unit_script>().HP < unit.GetComponent<unit_script>().HP_max)
                {
                    if (!unit.GetComponent<unit_script>().in_motion && !in_motion)
                    {
                        unit.GetComponent<unit_script>().HP += DPS * heal_precent * Time.deltaTime;
                    }
                }
            }
        }
    }

    public void L_plat()
    {
        point_plat((x_bord + 1), (y_bord + 2));
        point_plat((x_bord - 1), (y_bord + 2));
        point_plat((x_bord + 2), (y_bord + 1));
        point_plat((x_bord + 2), (y_bord - 1));
        point_plat((x_bord + 1), (y_bord - 2));
        point_plat((x_bord - 1), (y_bord - 2));
        point_plat((x_bord - 2), (y_bord + 1));
        point_plat((x_bord - 2), (y_bord - 1));
    }

    public void L_RTS()
    {
        point_RTS((x_bord + 1), (y_bord + 2));
        point_RTS((x_bord - 1), (y_bord + 2));
        point_RTS((x_bord + 2), (y_bord + 1));
        point_RTS((x_bord + 2), (y_bord - 1));
        point_RTS((x_bord + 1), (y_bord - 2));
        point_RTS((x_bord - 1), (y_bord - 2));
        point_RTS((x_bord - 2), (y_bord + 1));
        point_RTS((x_bord - 2), (y_bord - 1));
    }


    public void rook_plat()
    {
        line_move(1, 0);
        line_move(-1, 0);
        line_move(0, -1);
        line_move(0, 1);
    }

    public void rook_RTS()
    {
        line_move_rts(1, 0);
        line_move_rts(-1, 0);
        line_move_rts(0, -1);
        line_move_rts(0, 1);
    }


    public void bish_plat()
    {
        line_move(1, 1);
        line_move(1, -1);
        line_move(-1, 1);
        line_move(-1, -1);
    }

    public void bish_RTS()
    {
        line_move_rts(1, 1);
        line_move_rts(1, -1);
        line_move_rts(-1, 1);
        line_move_rts(-1, -1);
    }


    public void pawn_plat(int x, int y)
    {
        game sc = controler.GetComponent<game>();
        GameObject cp = sc.get_pos(x, y);
        ;
        if (sc.pos_on_board(x, y))
        {
            if(sc.get_pos(x,y) == null)
            {
                spawn_plat_m(x,y); 
            }
        }
        
    }



    public void pawn_loop(int up_down)
    {
        game sc = controler.GetComponent<game>();

        GameObject[] unit_attack = new GameObject[2];
        GameObject[] unit_prot = new GameObject[2];
        GameObject left_corner = null;
        GameObject right_corner = null;
        int num_attack = 0;

        if (sc.pos_on_board(x_bord - 1, y_bord + up_down))
        {
            left_corner = sc.get_pos(x_bord - 1, y_bord + up_down);
        }
        if (sc.pos_on_board(x_bord + 1, y_bord + up_down))
        {
            right_corner = sc.get_pos(x_bord + 1, y_bord + up_down);
        }



        if (left_corner != null && left_corner.GetComponent<unit_script>().player != player)
        {
            unit_attack[0] = left_corner;
            num_attack++;
        }

        if (right_corner != null && right_corner.GetComponent<unit_script>().player != player)
        {
            unit_attack[1] = right_corner;
            num_attack++;
        }

        if (left_corner != null && left_corner.GetComponent<unit_script>().player == player)
        {
            unit_prot[0] = left_corner;
        }

        if (right_corner != null && right_corner.GetComponent<unit_script>().player == player)
        {
            unit_prot[1] = right_corner;
        }

        //protecting does not have a # disavantgae to creat a defisive advatage

        // have to sum up attacks before doing damge for calculations to work
        if(!in_motion)
        {
            if (unit_attack[0] != null && !left_corner.GetComponent<unit_script>().in_motion)
            {
                left_corner.GetComponent<unit_script>().HP -= DPS / num_attack * Time.deltaTime;// add some time thing *Time.deltaTime
                

            }
            if (unit_attack[1] != null && !right_corner.GetComponent<unit_script>().in_motion)
            {
                right_corner.GetComponent<unit_script>().HP -= DPS / num_attack * Time.deltaTime;//
                                                                                                 //Debug.Log(right_corner.GetComponent<unit_script>().HP);
            }
        }


        if (!in_motion)
        {
            if (unit_prot[0] != null && !left_corner.GetComponent<unit_script>().in_motion)
            {
                if(left_corner.GetComponent<unit_script>().HP < left_corner.GetComponent<unit_script>().HP_max)
                {
                    left_corner.GetComponent<unit_script>().HP += DPS * heal_precent * Time.deltaTime;
                }
                
            }
            if (unit_prot[1] != null && !right_corner.GetComponent<unit_script>().in_motion)
            {
                if (right_corner.GetComponent<unit_script>().HP < right_corner.GetComponent<unit_script>().HP_max)
                {
                    right_corner.GetComponent<unit_script>().HP += DPS * heal_precent * Time.deltaTime;
                }
                
            }
        }
    }

    //// hp bar
    public void hp_bar()
    {
        if (skip < 10)
        {
            skip++;
        }
        else
        {
            //Debug.Log("destroy bar");
            Destroy(hp_bar_self);
            skip = 0;
            float y_disp=0.25f;
            float precent_hp = HP / HP_max;
            /*
            if (player == "w")
            {
                y_disp = 0.25f;
            }
            else
            {
                y_disp = -0.25f;
            }*/
            /*
            if (HP == HP_max)
            {
                this.GetComponent<health_bar_script>().enabled = false;// disable at full hp
            }   
            */

            if (1 <= precent_hp || precent_hp >= 0.75)
            {
                hp_bar_self = Instantiate(hp_bar_disp, new Vector3(transform.position.x, transform.position.y + y_disp, -3), Quaternion.identity);
                hp_bar_self.GetComponent<SpriteRenderer>().color = Color.green;
                hp_bar_self.transform.localScale = new Vector3(0.2244787f * precent_hp * 2, 0.04041638f, 1);
                //Destroy(hp_bar_self);

            }
            else if (0.75 <= precent_hp || precent_hp >= 0.5)
            {
                hp_bar_self = Instantiate(hp_bar_disp, new Vector3(transform.position.x, transform.position.y + y_disp, -3), Quaternion.identity);
                hp_bar_self.GetComponent<SpriteRenderer>().color = Color.yellow;
                hp_bar_self.transform.localScale = new Vector3(0.2244787f * precent_hp * 2, 0.04041638f, 1);
                //Destroy(hp_bar_self);

            }
            else if (0.5 <= precent_hp || precent_hp >= 0.25)
            {
                //this.GetComponent<SpriteRenderer>().enabled = true;
                //this.GetComponent<SpriteRenderer>().color = new Color(255f, 140f, 0f, 1f);
                hp_bar_self = Instantiate(hp_bar_disp, new Vector3(transform.position.x, transform.position.y + y_disp, -3), Quaternion.identity);
                hp_bar_self.GetComponent<SpriteRenderer>().color = new Color(255f, 140f, 0f, 1f);
                hp_bar_self.transform.localScale = new Vector3(0.2244787f * precent_hp * 2, 0.04041638f, 1);
                //Destroy(hp_bar_self);

            }
            else if (0.25 <= precent_hp || precent_hp >= 0)
            {
                //this.GetComponent<SpriteRenderer>().enabled = true;
                //this.GetComponent<SpriteRenderer>().color = Color.red;
                hp_bar_self = Instantiate(hp_bar_disp, new Vector3(transform.position.x, transform.position.y + y_disp, -3), Quaternion.identity);
                hp_bar_self.GetComponent<SpriteRenderer>().color = Color.red;
                hp_bar_self.transform.localScale = new Vector3(0.2244787f * precent_hp * 2, 0.04041638f, 1);
                //Destroy(hp_bar_self);

            }


        }


    }


    public void arrow_update()
    {
        float angle=Mathf.Atan(direction.y / direction.x);
        //Vector3.Distance(this.transform.position, new Vector3(x_space, y_space, -2))
        if(Vector3.Distance(this.transform.position, new Vector3(x_space, y_space, -2)) < 1)
        {
            GameObject dir_arrow= Instantiate(arrow, new Vector3(transform.position.x + 0.1f, transform.position.y + 0.14f + transform.position.z), Quaternion.Euler(0, 0, angle));
           // dir_arrow.transform.localScale = 
        }
        
    }
        

    //////////// every frame
    public void Update()
    {
        game sc = controler.GetComponent<game>();

        if (HP <= 0)
        {
            Destroy(sc.get_pos(x_bord,y_bord));// destroys self if HP<=0
        }
        hp_bar();
        if (Vector3.Distance(this.transform.position, new Vector3(x_space, y_space, -2))>0.01)
        {
            //Debug.Log(Vector3.Distance(this.transform.position, new Vector3(x_space, y_space, -2)));
            if (!in_motion)
            {
                direction = new Vector3(x_space, y_space, -2)-this.transform.position;
                //Debug.Log("seting direct");
            }
            //Debug.Log(this.transform.position + ", " + new Vector3(x_space,y_space, -2));
            set_cords(direction);
            can_move = false;
            in_motion = true;
        }
        else
        {
            can_move = true;
            in_motion = false;
            direction = new Vector3(0,0,0);//not need but I like house keeping

        }
        

        if (is_attacked2)
        {
            can_move = false;
        }

        switch (this.name)
        {
            case "w_pawn":
                pawn_loop(1);
                break;
            case "b_pawn":
                pawn_loop(-1);
                break;
            case "w_rook":
            case "b_rook":
                rook_RTS();
                break;
            case "b_queen":
            case "w_queen":
                rook_RTS();
                bish_RTS();
                break;
            case "b_knight":
            case "w_knight":
                L_RTS();
                break;
            case "b_bish":
            case "w_bish":
                bish_RTS();
                break;
            case "w_king":
            case "b_king":
                all_round_RTS();
                break;
        }
        
    }

}



