using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaballoBrain : AbstractBrain
{
    public int RadioEmbiste = 5;

    public float speedExterna = 2f;
    public float speedInterna = 5f;
    public float CoolDown = 1f;
    private bool _embiste = false;
    private Vector3 _aux_move;
    public float durada_embestir = 1f;
    public float durada_L = 0.5f;

    private bool _vertical;

    public override Vector3 GetMovement(PlayerController player, GameEntityController enemy)
    {
        if (_embiste)
        {
            if (TimeCounter < durada_embestir)
            {
                return _aux_move;
            }
            else
            {
                //termina embestir
                _embiste = false;
                return Vector3.zero;
            }
        }
        else
        {
            Vector3 position_with_radio = player.transform.position;
            Vector3 distancia = player.transform.position - enemy.transform.position;
            if (distancia.magnitude < RadioEmbiste)
            {
                _embiste = true;
                _aux_move = Vector3DistanceObjectToPlayer(player.transform.position, enemy.transform.position)
                    .normalized;
                TimeCounter = 0;
                return _aux_move;
            }
            else
            {
                if (_embiste)
                {
                    _embiste = false;
                    _vertical = true;
                    _aux_move = player.transform.position - enemy.transform.position;
                }

                if (_vertical)
                {
                    if (TimeCounter < durada_L)
                    {
                        _aux_move.x = 0;
                        if ((int) _aux_move.y == 0)
                        {
                            _aux_move.y = 2;
                        }

                        return _aux_move.normalized;
                    }
                    else
                    {
                        //termina embestir
                        _vertical = false;
                        _aux_move = player.transform.position - enemy.transform.position;
                        TimeCounter = 0;
                        return Vector3.zero;
                    }
                }
                else
                {
                    if (TimeCounter < durada_L)
                    {
                        _aux_move.y = 0;
                        if ((int) _aux_move.x == 0)
                        {
                            _aux_move.x = 2;
                        }

                        return _aux_move.normalized;
                    }
                    else
                    {
                        //termina embestir
                        _vertical = true;
                        _aux_move = player.transform.position - enemy.transform.position;
                        TimeCounter = 0;
                        return Vector3.zero;
                    }
                }
            }
        }
    }

    public override float GetSpeed()
    {
        if (_embiste)
        {
            return speedInterna;
        }
        else
        {
            return speedExterna;
        }
    }
}