using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Customs
{
    public enum Parts
    {
        BODY, EYES, BODYPART, HEADPARTS
    }

    [System.Serializable]
    public class Option
    {
        public int Number;
        public Color Color;  
    }

    [System.Serializable]
    public class Viwe : Option
    {
        public Parts part;
    }

    // ±≥√º πÿ ¿˙¿Â 
    [CreateAssetMenu(fileName = "Custom", menuName = "ScriptableObject/PlayerCustom")]
    public class Custom : ScriptableObject
    {
        [SerializeField]
        private Material baseMaterial;

        [SerializeField]
        private List<Viwe> viwe;

        private Transform custom;

        public Option Option(Parts part) 
        {
            Option result = null;
            foreach (var temp in viwe)
            {
                if (temp.part == part)
                {
                    result = new Option() { Color = temp.Color, Number = temp.Number };
                    break;
                }
            }
            return result;
        }


        public void Init(Transform tans)
        {
            custom = tans;
            for (int i = 0; i < viwe.Count; i++)
            {
                SetColor(viwe[i].part,viwe[i].Color);
                SetObject(viwe[i].part);
            }
        }

        public void SetColor(Parts part, Color color)
        {
            switch(part)
            {
                case Parts.BODY: { baseMaterial.SetColor("_Color04", color); } break;
                case Parts.EYES: { baseMaterial.SetColor("_Color07", color); } break;
                case Parts.BODYPART: { baseMaterial.SetColor("_Color02", color); } break;
                case Parts.HEADPARTS: { baseMaterial.SetColor("_Color03", color); } break;
            }

            for(int i =0; i < viwe.Count; i++)
            {
                if (viwe[i].part == part)
                {
                    viwe[i].Color = color;  return;
                }
            }
        }

        public void SetObject(Parts part, bool isRight)
        {
            int increase = 0;
            Transform result = custom.GetChild((int)part);
            Option option = Option(part);

            if (!isRight)
            {
                if (option.Number + 1 < result.childCount) { increase = 1; }
            }
            else
            {
                if (option.Number - 1 >= 0) {  increase = -1; }
            }

            if (increase != 0)
            {
                result.GetChild(option.Number).gameObject.SetActive(false);
                result.GetChild(option.Number + increase).gameObject.SetActive(true);
            }

            for (int i = 0; i < viwe.Count; i++)
            {
                if (viwe[i].part == part)
                {
                    viwe[i].Number += increase;
                }
            }

        }

        public void SetObject(Parts part)
        {
            Transform result = custom.GetChild((int)part);
            Option option = Option(part);

            result.GetChild(option.Number).gameObject.SetActive(false);
            result.GetChild(option.Number).gameObject.SetActive(true);
        }
    }

}