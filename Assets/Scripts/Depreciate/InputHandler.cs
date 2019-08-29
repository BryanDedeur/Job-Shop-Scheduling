using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    public JobShopAlgorithm jsa;

	public void SubmitButtonClicked() {
		jsa.RestartAlgorithm = true;
	}
}
