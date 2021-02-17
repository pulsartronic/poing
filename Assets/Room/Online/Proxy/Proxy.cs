using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class Proxy : MonoBehaviour {
/*
	Dictionary<string, Device> devices = new Dictionary<string, Device>();
	
	UdpClient client;
	IPEndPoint endPoint;
	
	bool running = false;
	Thread thread;
	
	void Awake() {
		this.client = new UdpClient(1700);
		
		this.endPoint = new IPEndPoint(IPAddress.Any, 1900);
		
		this.running = true;
		this.thread = new Thread(Run);
		this.thread.Start();
	}
	
	void Run() {
		Debug.Log("Starting thread ...");
		while(this.running) {
			try {
				byte[] bytes = this.client.Receive(ref this.endPoint);
				var dataSTR = System.Text.Encoding.UTF8.GetString(bytes);
				Hashtable data = (Hashtable) JSON.decode(dataSTR);
				string id = (string) data["id"];
				bool exists = this.devices.HasKey(id);
				// Device device = 
			} catch(Exception e) {
				Debug.LogError(e);
			}
		}
		Debug.Log("Releasing thread ... ::::::::::::");
	}
	*/
}
