﻿using System.Collections;
using System.Collections.Generic;
using Characters.Movement;
using General;
using NSubstitute;
using NUnit.Framework;
using Player;
using Player.MovementDirection;
using UnityEngine;
using UnityEngine.TestTools;
using static NSubstitute.Substitute;


namespace Tests {
    public class SlidingMovementTest {
        private List<GameObject> gameObjects = new List<GameObject>();
        private GameObject playerGo;
        private IPlayer player;

        [SetUp]
        public void setUpTestScene() {
            gameObjects = TestCommonMethods.init();
            GameObject inputManagerGo = TestCommonMethods.initInputManager();
            InputManager inputManager = inputManagerGo.GetComponent<InputManager>();
            playerGo = TestCommonMethods.initPlayer(inputManager);
            gameObjects.Add(inputManagerGo);
            gameObjects.Add(playerGo);
            player = playerGo.GetComponent<IPlayer>();
        }

        [TearDown]
        public void afterTest() {
            gameObjects.ForEach(Object.Destroy);
            player = null;
        }


        [UnityTest]
        public IEnumerator slideTest() {
            IMovementDirection moveDirection = For<IMovementDirection>();
            var direction = new Vector3(0, 0, 1);
            player.changeMovementDirection(moveDirection);
            moveDirection.getDirection().Returns(direction);
            player.changeMovement(new SlidingMovement(player));
            Vector3 playerInitPosition = playerGo.transform.position;
            Assert.True(player.getMovement() is SlidingMovement);
            yield return new WaitForSeconds(1f);

            Vector3 expectedPosition = playerInitPosition + direction * player.getStats().SlidingSpeed;

            Assert.True(Vector3.Distance(expectedPosition, playerGo.transform.position) <= 1f);
            Assert.True(player.getMovement() is SlidingMovement);
        }
    }
}