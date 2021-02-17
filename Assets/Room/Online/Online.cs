using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class Online : MonoBehaviour {
	public int id;
	public Room room;
	public GameObject remoteCharacterPrefab;
	
	public string host = "pulsartronic.com";
	UdpClient client;
	IPEndPoint endPoint;
	
	bool running = false;
	Thread thread;
	
	bool create = false;
	int createID = 0;
	List<RemoteCharacter> remoteCharacters = new List<RemoteCharacter>();

	Hashtable data;

	long s;
	long a;
	
	void Awake() {
		this.client = new UdpClient(0);
		this.endPoint = new IPEndPoint(IPAddress.Parse(this.host), 1700);
		
		if (!PlayerPrefs.HasKey("id")) {
			this.id = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
			PlayerPrefs.SetInt("id", this.id);
		} else {
			this.id = PlayerPrefs.GetInt("id");
		}
		
		this.data = new Hashtable();
		this.data["id"] = this.id;
		this.data["pos"] = new Hashtable();
		this.data["rot"] = new Hashtable();
		this.data["vel"] = new Hashtable();
		this.data["dir"] = 0f;
		this.data["on"] = false;
		this.data["t"] = 0L;
		this.data["c"] = new ArrayList();
	}

	void OnEnable() {
		this.running = true;
		this.thread = new Thread(Run);
		this.thread.Start();
	}

	void OnDisable() {
		this.running = false;
	}

	void Run() {
		Debug.Log("Starting thread ...");
		while(this.running) {
			try {
				byte[] bytes = this.client.Receive(ref this.endPoint);
				var dataSTR = System.Text.Encoding.UTF8.GetString(bytes);
				Hashtable data = (Hashtable) JSON.decode(dataSTR);
				int id = (int) (double) data["id"];
				RemoteCharacter remoteCharacter = this.remoteCharacters.Find(x => x.id == id);
				if (!remoteCharacter) {
					if (!create) {
						create = true;
			 			createID = id;
			 		}
				} else {
					remoteCharacter.setData(data);
				}
			} catch(Exception e) {
				Debug.LogError(e);
			}
		}
		Debug.Log("Releasing thread ... ::::::::::::");
	}

	void FixedUpdate() {
		if (create) {
			GameObject remoteCharacterObject = GameObject.Instantiate(this.remoteCharacterPrefab, this.transform);
			RemoteCharacter remoteCharacter = remoteCharacterObject.GetComponent<RemoteCharacter>();
			this.remoteCharacters.Add(remoteCharacter);
			remoteCharacter.init(this, createID);
 			create = false;
 		}
	 	
		try {
			Hashtable pos = (Hashtable) this.data["pos"];
			pos["x"] = this.room.player.transform.position.x;
			pos["y"] = this.room.player.transform.position.y;
			pos["z"] = this.room.player.transform.position.z;
			
			Hashtable vel = (Hashtable) this.data["vel"];
			vel["x"] = this.room.player.body.velocity.x;
			vel["y"] = this.room.player.body.velocity.y;
			vel["z"] = this.room.player.body.velocity.z;
			
			Hashtable rot = (Hashtable) this.data["rot"];
			rot["x"] = this.room.player.transform.eulerAngles.x;
			rot["y"] = this.room.player.transform.eulerAngles.y;
			rot["z"] = this.room.player.transform.eulerAngles.z;
			
			this.data["dir"] = this.room.player.direction;
			this.data["bea"] = this.room.player.beating;
			
			this.data["t"] = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
			
			ArrayList clocks = (ArrayList) this.data["c"];
			clocks.Clear();
			foreach(RemoteCharacter remoteCharacter in this.remoteCharacters) {
				Hashtable clock = remoteCharacter.getSendClock();
				if (null != clock) {
					clocks.Add(clock);
				}
			}
			
			string dataSTR = JSON.encode(this.data);
			byte[] bytes = System.Text.Encoding.UTF8.GetBytes(dataSTR);

			this.client.Send(bytes, bytes.Length, this.endPoint);
		} catch (Exception e) {
			Debug.LogError(e);
		}
	}
}

