using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

public class RemoteCharacter : Character {
	public Online online;
	public int id;
	
	public UdpClient client;
	IPEndPoint endPoint;
	
	Thread thread;
	
	Vector3 rposition = Vector3.zero;
	Vector3 rvelocity = Vector3.zero;
	Vector3 reulers = Vector3.zero;
	float dir = 0f;
	bool on = false;
	
	long t;
	long timeDifference;
	long lowestPing = 100000000L;
	
	long arrived;
	long lastArrivedSent;
	public Hashtable clock = new Hashtable();
	
	public bool use = false;
	
	public void init(Online online, int id) {
		this.online = online;
		this.id = id;
		this.clock["id"] = this.id;
	}

	public void setData(Hashtable data) {
		long t = (long) (double) data["t"];
		if (t > this.t) {
			this.t = t;
			this.use = true;
			
			this.arrived = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
			ArrayList clocks = (ArrayList) data["c"];
			foreach (Hashtable clock in clocks) {
				int id = (int) (double) clock["id"];
				if (id == this.online.id) {
					long clockT = (long) (double) clock["t"];
					long st = (long) (double) clock["st"];
					long ping = this.arrived - clockT - st;
					if (ping < this.lowestPing) {
						this.lowestPing = ping;
						long approximatedTime = t + (ping / 2);
						this.timeDifference = approximatedTime - this.arrived;
					}
				}
			}
			
			Hashtable pos = (Hashtable) data["pos"];
			this.rposition.x = (float) (double) pos["x"];
			this.rposition.y = (float) (double) pos["y"];
			this.rposition.z = (float) (double) pos["z"];
			
			Hashtable vel = (Hashtable) data["vel"];
			this.rvelocity.x = (float) (double) vel["x"];
			this.rvelocity.y = (float) (double) vel["y"];
			this.rvelocity.z = (float) (double) vel["z"];
			
			Hashtable rot = (Hashtable) data["rot"];
			this.reulers.x = (float) (double) rot["x"];
			this.reulers.y = (float) (double) rot["y"];
			this.reulers.z = (float) (double) rot["z"];
			
			this.dir = (float) (double) data["dir"];
			this.on = (bool) data["on"];
		}
	}

	long getTime() {
		long now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
		long time = now + this.timeDifference;
		return time;
	}

	void FixedUpdate() {
		if (use) {
			this.use = false;
			
			long time = this.getTime();
			long timeDifference = (time - this.t) / 1000;
			Vector3 updatedPosition = this.rposition + timeDifference * this.rvelocity;
			Vector3 diff = this.transform.position - updatedPosition;
			if (0.7f < diff.magnitude) {
				this.transform.position = this.rposition;
				this.body.velocity = this.rvelocity;
			}
			Quaternion rotation = this.transform.rotation;
			rotation.eulerAngles = this.reulers;
			this.transform.rotation = rotation;
			
		}
	}
	
	public Hashtable getSendClock() {
		Hashtable clock = null;
		if (this.arrived > this.lastArrivedSent) {
			this.lastArrivedSent = this.arrived;
			this.clock["t"] = this.t;
			long now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
			this.clock["st"] = now - this.arrived; // Storage Time
			clock = this.clock;
		}
		return clock;
	}
	
	public float getDirection() {
		return this.dir;
	}
	
	public bool isMotorON() {
		return this.on;
	}
}
