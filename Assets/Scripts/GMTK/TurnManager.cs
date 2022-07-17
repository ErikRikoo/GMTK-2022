using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GMTK;
using GMTK.LevelHandling;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private Player_holder player_holder;
    [SerializeField] private RoomHolder current_room;
    [SerializeField] private VoidEvent ChangeRoom ;
    [SerializeField] private VoidEvent playerEscape;
    [SerializeField] private VoidEvent endPlayerTurn_s;
    [SerializeField] private VoidEvent goLoop;
    [SerializeField] private VoidEvent end_room_spawn;

    private bool playerEscaped = false;

    private IEnumerator coroutine;
    private IEnumerator move_player;

    private bool end_turn_player = false;
    private int index_enemie;
    private int nbEnnemi;
    // Start is called before the first frame update
    void Start()
    {
        playerEscape.Register(escape);
        endPlayerTurn_s.Register(EndTurnPlayer);
        goLoop.Register(GoTurn);
        end_room_spawn.Register(InANewRoom);
        //coroutine = Turn();
        InANewRoom();
    }

    private void InANewRoom()
    {
        player_holder.player.transform.position = current_room.Room.Entry.position;
        GoTurn();
    }

    private IEnumerator Turn()
    {
        nbEnnemi = current_room.Room.Enemies.Count();
        index_enemie = 0;
        // coroutine = Turn_Coroutine();
        // coroutine.MoveNext();//Tours du Player
        Debug.Log("Code du player");
        player_holder.player.Play();
        while (!end_turn_player)
        {
            yield return null;
        }

        end_turn_player = false;

        if ((!playerEscaped) && (!current_room.Room.IsRoomEmpty)) // s'il ne s'est pas échappé et qu'il reste des ennemies
        {
           // coroutine.MoveNext(); //Tours des enemies
           Debug.Log("Code des enemies");
        
           for (; index_enemie < nbEnnemi; index_enemie++)
           {
               if(!current_room.Room.Enemies.ElementAt(index_enemie).IsDead())
                   current_room.Room.Enemies.ElementAt(index_enemie).Play();
           }
           Debug.Log("fin Code des enemies");
            if (player_holder.player.IsDead())
                yield break;
            else
            {
                Debug.Log("on looooop");
                goLoop.Raise();
            }

        }
        else
        {
            EndTurn();
            yield return null;
        }

    }

    private void GoTurn()
    {
        StartCoroutine(coroutine=Turn());
    }

    // private IEnumerator Turn_Coroutine()
    // {
    //     
    //     Debug.Log("Code du joueur");
    //     player_holder.player.Play();
    //     yield return null;
    //     Debug.Log("Code des enemies");
    //     
    //     for (; index_enemie < nbEnnemi; index_enemie++)
    //     {
    //         if(!current_room.Room.Enemies.ElementAt(index_enemie).IsDead())
    //             current_room.Room.Enemies.ElementAt(index_enemie).Play();
    //     }
    //     yield return null;
    // }

    private void EndTurn()
    {
        if (current_room.Room.IsRoomEmpty)//si la salle est vide = tous les monstres sont morts
        {
            Debug.Log("Salle nettoyée!!");

            var loot = current_room.Room.Loot;
             while (loot.MoveNext())
             {
                 loot.Current.ExecuteOn(player_holder.player);
            }
            //donne points d'action
            player_holder.player.number_action += current_room.Room.Enemies.Count();
            
            //deplacement jusqu'a la sortie
            move_player = player_holder.player.movement(current_room.Room.Exit,(() => ChangeRoom.Raise()));
            StartCoroutine(move_player);//deplacement du player 
            Debug.Log("player est à la sortie");
            
            //génére une nouvelle salle
            
           

        }
        else if(playerEscaped) //le joueur à fuit
        {
            Debug.Log("C'est la fouite");
            move_player = player_holder.player.movement(current_room.Room.Entry,(() => ChangeRoom.Raise()));
            StartCoroutine(move_player);//deplacement du player 
            
            Debug.Log("player est à l'entrée");
            
        }
    }

    private void escape()
    {
        playerEscaped = true;
    }

    private void EndTurnPlayer()
    {
        end_turn_player = true;
    }
}
