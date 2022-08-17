using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Npc;

namespace Npc.Cathedral
{
    public class Cathedral : NpcManager
    {
        protected override void GameStartFunc()
        {
            base.GameStartFunc();
            GameManager.SceneChange("Scene_Runing");
        }
    }
}

