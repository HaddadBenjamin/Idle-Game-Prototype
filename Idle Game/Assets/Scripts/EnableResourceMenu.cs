using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnableResourceMenu : AResourceMenu
{
	void Start ()
    {
        GetComponent<Button>().onClick.AddListener(() =>
            {
                base.EnableResourceAnimationMenu();
            });
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
