using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// 어플리케이션 타겟 프레임 레이트 
// 버튼 좌우 틀림

namespace Npc
{
    public class UI
    {
        public Transform main;
        public (Text name, Text text) Label;
        public (Button Right, Button Left, Button Start) Button;
    }

    public enum SkipOption
    {
        RIGHT, LEFT, STOP, END
    }

    public class NpcManager : MonoBehaviour
    {
        public string Name;
        public string[] Say;

        protected GameManager GameManager { get { return GameManager.OnlyClass; } }
        protected SceneManager Scene { get { return GameManager.OnlyClass.Scene; } }
        protected Transform Player;
        protected float mRotation;
        protected float ToDistance;
        protected UI Ui;
        protected bool touch;

        protected (IEnumerator System, SkipOption skip) Reding;

        protected virtual void Start()
        {
            Start_Object();
            Start_Ui();
            touch = false;
        }

        private void FixedUpdate()
        {
            ToDistance = Vector3.Distance(Player.position, transform.position);
            if(ToDistance < 6)
            {
                transform.LookAt(Player);
                if (ToDistance < 2)
                {
                    if (!Ui.main.gameObject.activeSelf)
                    {
                        touch = true;
                        Animation_In();
                        Ui.main.gameObject.SetActive(touch);
                        PlayerLookAt();
                    }
                }
            }
            else if (touch)
            {
                touch = false;
                Animation_Out();
                Ui.main.gameObject.SetActive(touch);
                StopCoroutine(Reding.System);
            } 
        }

        private void Start_Object()
        {
            Player = Scene.mPlayer;
            Reding.System = TextReding();
        }

        private void Start_Ui()
        {
            Ui = new UI();
            Ui.main = Scene.Canvas.Find("NPC");
            Ui.Label.text = Ui.main.Find("Label_Text").GetComponent<Text>();
            Ui.Label.name = Ui.main.Find("Label_Name").GetComponent<Text>();
            Ui.Button.Right = Ui.main.Find("Right").GetComponent<Button>();
            Ui.Button.Left = Ui.main.Find("Left").GetComponent<Button>();
            Ui.Button.Start = Ui.main.Find("Start").GetComponent<Button>();
        }

        private IEnumerator TextReding()
        {
            int state = 0;
            Ui.Label.name.text = name;
            Ui.Label.text.text = Say[state];
            Reding.skip = SkipOption.STOP;
            WaitForFixedUpdate fixedUpate = new WaitForFixedUpdate();
            while (Reding.skip != SkipOption.END)
            {
                if (Reding.skip != SkipOption.STOP)
                {
                    if (Reding.skip == SkipOption.RIGHT) { state--; } else { state++; }
                    Reding.skip = SkipOption.STOP;
                    if (state == 0 || state == Say.Length - 1)
                    {
                        Ui.Label.text.text = Say[state];
                    }
                }
                yield return fixedUpate;
            }
        }

        Action action; 

        protected virtual void PlayerLookAt()
        {
            Ui.Label.name.text = "< " + Name + " >";
            StartCoroutine(Reding.System);
            Ui.Button.Right.onClick.RemoveAllListeners();
            Ui.Button.Right.onClick.AddListener(()=> SkipOptionFunc(SkipOption.RIGHT));
            Ui.Button.Left.onClick.RemoveAllListeners();
            Ui.Button.Left.onClick.AddListener(() => SkipOptionFunc(SkipOption.LEFT));
            
            Ui.Button.Start.onClick.RemoveAllListeners();
            Ui.Button.Start.onClick.AddListener(GameStartFunc);
        }

        protected void SkipOptionFunc(SkipOption option)
        {
            Reding.skip = option;    
        }

        protected virtual void GameStartFunc()
        {
            Debug.Log("게임 씬으로 넘겨짐");
        }
        
        protected virtual void Animation_In()
        {

        }

        protected virtual void Animation_Out()
        {

        }

    }
}

