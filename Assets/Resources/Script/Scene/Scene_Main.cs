using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Customs;

namespace Scene
{
    [System.Serializable]
    public class Main_UI
    {
        public Slider R, G, B;
        public Button Right, Left;
        public Toggle Active;
    }


    public class Scene_Main : SceneManager
    {
        private Vector2 Key_Input;
        public Dictionary<Parts,Main_UI> Uiset;
        
        protected override void Awake()
        {
            base.Awake();
            Awake_Object();
            Awake_Listener();
            Custom.Init(mPlayer.Find("Player").GetComponent<Transform>());
            Awake_UI();
        }

        private void FixedUpdate()
        {
            if (Input.GetMouseButton(0))
            {
                Key_Input.x = Input.GetAxis("Mouse X");
                if (Key_Input.magnitude == 0) { return; }
                mPlayer.eulerAngles = new Vector3(mPlayer.eulerAngles.x, (mPlayer.eulerAngles.y + Key_Input.x * 10), mPlayer.eulerAngles.z);
            }
        }

        private void Awake_Object()
        {
            mPlayer = GameObject.Find("Characters").GetComponent<Transform>();
        }

        private void Awake_Listener()
        {
            GameObject.Find("Start_Button").GetComponent<Button>().onClick.AddListener(()=>GameManager.SceneChange("Scene_Adventure"));
        }

        private void Awake_UI()
        {
            Uiset = new Dictionary<Parts, Main_UI>();
            foreach (Parts part in System.Enum.GetValues(typeof(Parts)))
            {
                Uiset.Add(part, setUI(part));
            }
        }

        private Main_UI setUI(Parts part)
        {
            Main_UI reuslt = new Main_UI();
            Transform find = Canvas.Find("Setting").transform.GetChild((int)part);
            Option option = Custom.Option(part);
            
            reuslt.R = find.Find("R").GetComponent<Slider>();
            reuslt.R.value = option.Color.r;
            reuslt.R.onValueChanged.AddListener(delegate { RGB_ValueSlider(part); });

            reuslt.G = find.Find("G").GetComponent<Slider>();
            reuslt.G.value = option.Color.g;
            reuslt.G.onValueChanged.AddListener(delegate { RGB_ValueSlider(part); });

            reuslt.B = find.Find("B").GetComponent<Slider>();
            reuslt.B.value = option.Color.b;
            reuslt.B.onValueChanged.AddListener(delegate { RGB_ValueSlider(part); });

            if (part == Parts.BODY) { return reuslt; }
            
            reuslt.Right = find.Find("Right").GetComponent<Button>();
            reuslt.Right.onClick.AddListener(delegate { isRight_Button(part, true); });

            reuslt.Left = find.Find("Left").GetComponent<Button>();
            reuslt.Left.onClick.AddListener(delegate { isRight_Button(part, false); });

            if (part == Parts.EYES) { return reuslt; }

            //≈‰±€≈∞ 


            return reuslt;
        }

        private void RGB_ValueSlider(Parts part)
        {
            Color color = new Color() { r = Uiset[part].R.value, g = Uiset[part].G.value, b = Uiset[part].B.value, a = 1 };
            Custom.SetColor(part, color);
        }

        private void isRight_Button(Parts part, bool isRight)
        {
            Custom.SetObject(part, isRight);
        }

    }
}
