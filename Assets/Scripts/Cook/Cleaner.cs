using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : MonoBehaviour {

    public Plate plate;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Plate")
        {
            Destroy(coll.gameObject);
            plate.MakeNewPlate(new Vector2(20.85f, -40.34f));
        }

        else if (coll.gameObject.tag == "OldPlate")
        {
            Destroy(coll.gameObject);
        }
    }
}
