﻿using System.Collections.Generic;
using General;
using NUnit.Framework;
using Player;
using UnityEngine;

namespace Tests {
    public class AbstractTest {
        protected List<GameObject> gameObjects = new List<GameObject>();
        protected GameObject playerGo;
        protected IPlayer player;

        [SetUp]
        public void setUpTestScene() {
            gameObjects = init();
            
            GameObject inputManagerGo = initInputManager();
            InputManager inputManager = inputManagerGo.GetComponent<InputManager>();
            playerGo = initPlayer(inputManager);
            gameObjects.Add(inputManagerGo);
            gameObjects.Add(playerGo);
            player = playerGo.GetComponent<IPlayer>();
        }

        [TearDown]
        public void afterTest() {
            gameObjects.ForEach(Object.Destroy);
            player = null;
        }
        
        
        
        protected static List<GameObject> init() {
            List<GameObject> gameObjects = new List<GameObject> {
                Object.Instantiate(Resources.Load<GameObject>("Prefabs/Cameras/Main Camera")),
                Object.Instantiate(Resources.Load<GameObject>("Prefabs/Cameras/Always Forward Camera")),
                Object.Instantiate(Resources.Load<GameObject>("Prefabs/Cameras/Camera Manager")),
                Object.Instantiate(Resources.Load<GameObject>("Prefabs/Enviroments/Test Floor"))
            };
            return gameObjects;
        }

        protected static GameObject initInputManager() {
            return Object.Instantiate(Resources.Load<GameObject>("Prefabs/General/Input Manager"));
        }

        protected static GameObject initPlayer(InputManager inputManager) {
            var playerGo = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Player/Player"));
            inputManager.playerGameObject = playerGo;
            return playerGo;
        }
    }
}