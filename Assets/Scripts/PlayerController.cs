﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody thisRigidBody;
    private GameObject focalPointObject;
    public float speed;
    public float powerUpStrength;
    public bool isPoweredUp = false;

    private void Start() {
        InitializeComponents();
    }

    private void Update() {
        ApplyInputForce();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("PowerUp")) {
            ConsumePowerUp(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (isPoweredUp && other.gameObject.CompareTag("Enemy")) {
            PoweredUpEnemyCollision(other);
        }
    }

    private void InitializeComponents() {
        thisRigidBody = GetComponent<Rigidbody>();
        focalPointObject = GameObject.Find("Focal Point");
    }

    private void ApplyInputForce() {
        // input value is between 0 and 1 
        float verticalInputMagnitude = Input.GetAxis("Vertical");
        thisRigidBody.AddForce(focalPointObject.transform.forward * verticalInputMagnitude * speed);
    }

    private void ConsumePowerUp(GameObject powerUp) {
        isPoweredUp = true;
        Destroy(powerUp);
    }

    private void PoweredUpEnemyCollision(Collision enemyCollision) {
        GameObject enemy = enemyCollision.gameObject;
        Rigidbody enemyRigidbody = enemy.GetComponent<Rigidbody>();

        Debug.Log($"Player collided with ${enemy} with powerup set to ${isPoweredUp}");
        
        Vector3 awayFromPlayer = (enemy.transform.position - transform.position).normalized;
        enemyRigidbody.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
    }
}
