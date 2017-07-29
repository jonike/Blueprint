﻿using System;
using System.Collections;
using System.Runtime.Serialization;
using UnityEngine;

[Serializable]
public class MapObject : ISerializable {
	public const string KEY_ID = "ID";
	public const string KEY_POS = "POS";

	//TODO オブジェクト別にPrefabを作成
	public static MapEntity objPrefab;

	public MapEntity obj { get; private set; }

	//TODO private setにする必要があるが、Chunkにて使用されているので改良する必要がある
	public Chunk chunk;
	//public int id;
	public Vector3 pos { get; private set; }

	//TODO バウンディングボックス

	public MapObject (Chunk chunk/*, int id*/, Vector3 pos) {
		this.chunk = chunk;
		//this.id = id;
		this.pos = pos;
	}

	protected MapObject (SerializationInfo info, StreamingContext context) {
		if (info == null)
			throw new ArgumentNullException ("info");
		//id = info.GetInt32 (KEY_ID);
		pos = ((SerializableVector3)info.GetValue (KEY_POS, typeof(SerializableVector3))).toVector3 ();
	}

	public virtual void GetObjectData (SerializationInfo info, StreamingContext context) {
		if (info == null)
			throw new ArgumentNullException ("info");
		//info.AddValue (KEY_ID, id);
		if (obj != null) {
			pos = obj.transform.position;
		}
		info.AddValue (KEY_POS, new SerializableVector3 (pos));
	}

	//TODO
	/*public void moveToChunk (Chunk chunk) {
		this.chunk = chunk;
	}*/

	public void generate () {
		if (obj == null) {
			(obj = GameObject.Instantiate (objPrefab)).init (this);
		}
	}

	//時間が経過するメソッド。ticksには経過時間を指定。
	public void TimePasses (long ticks) {
		//TODO
	}
}
