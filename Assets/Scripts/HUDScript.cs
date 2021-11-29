using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour
{
    public bool hasGenerateBeenPressed = false;
    public bool hasResetBeenPressed = false;

    [SerializeField] private LSystemGenerator LSystem;
    [SerializeField] private InputField axiom;
    [SerializeField] private InputField rule1;
    [SerializeField] private InputField rule2;
    [SerializeField] private InputField rule3;
    [SerializeField] private InputField iterations;
    [SerializeField] private InputField angle;
    [SerializeField] private InputField length;
    [SerializeField] private InputField width;

    private int tempInt;
    private float tempFloat;
    private char tempChar;


	public void Start ()
    {
        iterations.text = LSystem.iterations.ToString();
        angle.text = LSystem.angle.ToString() + "°";
        length.text = LSystem.length.ToString("F1");
        width.text = LSystem.width.ToString("F1");

    }

    public void GenerateNew() {
        hasGenerateBeenPressed = true;
    }
    public void ResetValues() {
        hasResetBeenPressed = true;
    }

    public void IterationsUp() {
        if (LSystem.iterations < LSystemGenerator.MAX_ITERATIONS) {
            LSystem.iterations++;
            iterations.text = LSystem.iterations.ToString();
        }
    }
    public void IterationsDown() {
        if (LSystem.iterations > 1) {
            LSystem.iterations--;
            iterations.text = LSystem.iterations.ToString();
        }
    }
    public void AngleUp() {
        LSystem.angle++;
        angle.text = LSystem.angle.ToString() + "°";
    }
    public void AngleDown() {
        LSystem.angle--;
        angle.text = LSystem.angle.ToString() + "°";
    }

    public void LengthUp() {
        LSystem.length += .1f;
        length.text = LSystem.length.ToString("F1");
    }
    public void LengthDown() {
        if (LSystem.length > 0){
            LSystem.length -= .1f;
            length.text = LSystem.length.ToString("F1");
        }
    }
    public void WidthUp() {
        LSystem.width += .1f;
        width.text = LSystem.width.ToString("F1");
    }
    public void WidthDown() {
        if (LSystem.width > 0) {
            LSystem.width -= .1f;
            width.text = LSystem.width.ToString("F1");
        }
    }


    //input fields on value change and on end edit methods 
    public void AxiomInputOVC() {
        LSystem.axiom = axiom.text;
    }
    public void AxiomInputOEE() {
        axiom.text = LSystem.axiom.ToString();
    }

    public void Rule1InputOVC() {

    }
    public void Rule1InputOEE() {
        string[] tempRule = rule1.text.Split(',');
        if (char.TryParse(tempRule[0], out tempChar))
            LSystem.rules.Add(tempChar, tempRule[1]);
    }
    public void Rule2InputOVC() {

    }
    public void Rule2InputOEE() {
        string[] tempRule = rule2.text.Split(',');
        if (char.TryParse(tempRule[0], out tempChar))
            LSystem.rules.Add(tempChar, tempRule[1]);
    }
    public void Rule3InputOVC() {

    }
    public void Rule3InputOEE() {
        string[] tempRule = rule3.text.Split(',');
        if (char.TryParse(tempRule[0], out tempChar))
            LSystem.rules.Add(tempChar, tempRule[1]);
    }

    public void IterationsInputOVC() {
        if (int.TryParse(iterations.text, out tempInt)) {
            LSystem.iterations = Mathf.Clamp(tempInt, 1, LSystemGenerator.MAX_ITERATIONS);
        }
    }
    public void IterationsInputOEE() {
        iterations.text = LSystem.iterations.ToString();
    }

    public void AngleInputOVC() {
        if (int.TryParse(angle.text, out tempInt)) {
            LSystem.angle = tempInt;
        }
    }
    public void AngleInputOEE() {
        angle.text = LSystem.angle.ToString() + "°";
    }

    public void LengthInputOVC() {
        if (float.TryParse(length.text, out tempFloat)) {
            LSystem.length = tempFloat;
        }
    }
    public void LengthInputOEE() {
        length.text = LSystem.length.ToString("F1");
    }

    public void WidthInputOVC() {
        if (float.TryParse(width.text, out tempFloat)) {
            LSystem.width = tempFloat;
        }
    }
    public void WidthInputOEE() {
        width.text = LSystem.width.ToString("F1");
    }
}
