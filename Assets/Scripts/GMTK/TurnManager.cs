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

    private bool playerEscaped = false;

    private IEnumerator coroutine;
    
    private int index_enemie;
    private int nbEnnemi;
    // Start is called before the first frame update
    void Start()
    {
        
        Debug.Log("STAAAAART");
        
        playerEscape.Register(escape);// s'il s'échappe on fini le tours

        Turn();
    }

    private void Turn()
    {
        nbEnnemi = current_room.Room.Enemies.Count();
        index_enemie = 0;
        coroutine = Turn_Coroutine();
        coroutine.MoveNext();//Tours du Player
        if ((!playerEscaped) && (!current_room.Room.IsRoomEmpty)) // s'il ne s'est pas échappé et qu'il reste des ennemies
        {
            coroutine.MoveNext(); //Tours des enemies
            if (player_holder.player.IsDead())
                return;
            else
                Turn();

        }
        else
            EndTurn();

    }
    

    private IEnumerator Turn_Coroutine()
    {
        
        Debug.Log("Code du joueur");
        player_holder.player.Play();
        //POUR LE TEST
        player_holder.player.Attack(current_room.Room.Enemies.ElementAt(1));
        player_holder.player.Attack(current_room.Room.Enemies.ElementAt(0));
        //FIN DE POUR LE TEST
        yield return null;
        Debug.Log("Code des enemies");
        
        for (; index_enemie < nbEnnemi; index_enemie++)
        {
            if(!current_room.Room.Enemies.ElementAt(index_enemie).IsDead())
                current_room.Room.Enemies.ElementAt(index_enemie).Play();
        }
        yield return null;
    }

    private void EndTurn()
    {
        Debug.Log("FIN du tour");
        if (current_room.Room.IsRoomEmpty)//si la salle est vide = tous les monstres sont morts
        {
            //donne le loot (augumente damage ou health)
            
            //donne points d'action
            player_holder.player.number_action += current_room.Room.Enemies.Count();
            
            //génére une nouvelle salle
            ChangeRoom.Raise();

        }
        else if(playerEscaped) //le joueur à fuit
        {
            ChangeRoom.Raise();
        }
    }

    private void escape()
    {
        playerEscaped = true;
    }
}
