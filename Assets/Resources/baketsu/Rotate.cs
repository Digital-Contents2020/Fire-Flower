using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
	public enum RotateAxis {
		XAxis,
		YAxis,
		ZAxis
	}
 
	public enum RotateMode {
		Local,
		World
	}
	//　回転軸をどこにするか
	public RotateAxis rotateAxis;
	//　ローカルで回転するかワールドで回転するか
	public RotateMode rotateMode;
	//　回転値
	private Space relativeTo;
	//　回転スピード
	public float rotateSpeed;
	
	// Update is called once per frame
	void Update () {
		//　選択した回転モードを設定
		if (rotateMode == RotateMode.Local) {
			relativeTo = Space.Self;
		} else {
			relativeTo = Space.World;
		}
 
		//　回転処理
		float rotation = Time.deltaTime * rotateSpeed;
 
		if (rotateAxis == RotateAxis.XAxis) {
			transform.Rotate (rotation, 0f, 0f, relativeTo);
		} else if (rotateAxis == RotateAxis.YAxis) {
			transform.Rotate (0f, rotation, 0f, relativeTo);
		} else if (rotateAxis == RotateAxis.ZAxis) {
			transform.Rotate (0f, 0f, rotation, relativeTo);
		}
	}

}
