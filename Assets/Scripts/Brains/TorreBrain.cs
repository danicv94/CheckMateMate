using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorreBrain : AbstractBrain
{
    private int num_disparos = 8;
    private float rango = 180;
    private Vector2[] array_distancia;

    public override Vector2[] GetShoot(PlayerController player, GameEntityController shot)
    {
        array_distancia = new Vector2[num_disparos];
        Vector2 lookAt = new Vector2(player.transform.position.x - shot.transform.position.x,
            player.transform.position.y - shot.transform.position.y).normalized;

        for (int i = 0; i < num_disparos; i++)
        {
            array_distancia[i] = Rotate(lookAt, (i - num_disparos / 2) * 22.5f);
        }

        return array_distancia;
    }

    public static Vector2 Rotate(Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }
}