using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildDoor : MonoBehaviour
{
    public GameObject pieceOne;
    public GameObject pieceTwo;
    public GameObject pieceThree;
    public GameObject pieceFour;

  
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Gold")
        {
            pieceOne.SetActive(true);
        }
        else if (other.gameObject.tag == "Silver")
        {
            pieceTwo.SetActive(true);
        }
        else if (other.gameObject.tag == "Blue")
        {
            pieceThree.SetActive(true);
        }
        else if (other.gameObject.tag == "Red")
        {
            pieceFour.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Gold")
        {
            pieceOne.SetActive(false);
        }
        else if (other.gameObject.tag == "Silver")
        {
            pieceTwo.SetActive(false);
        }
        else if (other.gameObject.tag == "Blue")
        {
            pieceThree.SetActive(false);
        }
        else if (other.gameObject.tag == "Red")
        {
            pieceFour.SetActive(false);
        }
    }


}
