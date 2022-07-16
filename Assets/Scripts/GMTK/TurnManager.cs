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
    // Start is called before the first frame update
    void Start()
    {
        // player_holder.player.Play();
        // for (int i = 0; i < current_room.Room.Enemies.Count(); i++)
        // {
        //     current_room.Room.Enemies
        // }
        IEnumerator coroutine = Turn();
        playerEscape.Register(EndTurn);
        if ((!playerEscaped) && (!current_room.Room.IsRoomEmpty))
        {
            while (!player_holder.player.IsDead())
            {
                coroutine.MoveNext();
            }

        }
    }

    public IEnumerator Turn()
    {
        Debug.Log("Code du joueur");
        player_holder.player.Play();
        yield return null;
        int nbEnnemi = current_room.Room.Enemies.Count();
        for (int i = 0; i < nbEnnemi; ++i)
        {
            if(!current_room.Room.Enemies.ElementAt(i).IsDead())
                current_room.Room.Enemies.ElementAt(i).Play();
            yield return null;
        }
    }

    void EndTurn()
    {
        if (current_room.Room.IsRoomEmpty)//si la salle est vide = tous les monstres sont morts
        {
            //donne le loot (augumente damage ou health)
            
            //donne points d'action
            player_holder.player.number_action += current_room.Room.Enemies.Count();
            
            //génére une nouvelle salle
            ChangeRoom.Raise();

        }
        else //le joueur à fuit
        {
            playerEscaped = true;
            ChangeRoom.Raise();
        }
    }
}
