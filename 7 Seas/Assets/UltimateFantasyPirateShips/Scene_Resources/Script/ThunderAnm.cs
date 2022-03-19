using UnityEngine;
using System.Collections;

public class ThunderAnm : MonoBehaviour {
	float timer=0;
	bool isRun=false;
	float timer2=1;

	void OnEnable(){
		timer =0;
		isRun=false;
		timer2=Random.Range(.5f,1);
	}

	void Update () {
		timer -= Time.deltaTime;
		if (timer<0) {
			isRun=true;
			timer = Random.Range (5, 10);
		}

		if (isRun) {
			RenderSettings.skybox.SetFloat("_Exposure",Random.Range(2,3.5f));
			timer2-=Time.deltaTime;
		}

		if (timer2<0) {
			RenderSettings.skybox.SetFloat("_Exposure",2);
			isRun=false;
			timer2=Random.Range(.5f,1);
		}
	}
}
