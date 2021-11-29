using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

public class LSystemGenerator : MonoBehaviour
{
    public string axiom = "";
    public Dictionary<char, string> rules;
    public int iterations = 1;
    public float angle = 30f;
    public float width = 10f;
    public float length = 10f;
    public GameObject LSystem = null;

    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject line;
    [SerializeField] private HUDScript HUD;

    private string currentStr = string.Empty;
    private Vector3 initPos = Vector3.zero;
    private Stack<TransformInfo> transformStack;
    public const int MAX_ITERATIONS = 8;

    // Start is called before the first frame update
    void Start()
    {
        ResetTreeValues();
        rules = new Dictionary<char, string>();
        transformStack = new Stack<TransformInfo>();
        Generate();
    }

    // Update is called once per frame
    void Update()
    {
        if (HUD.hasGenerateBeenPressed || Input.GetKeyDown(KeyCode.G)) {
            HUD.hasGenerateBeenPressed = false;
            Generate();
        }

        if (HUD.hasResetBeenPressed || Input.GetKeyDown(KeyCode.R)) {
            ResetTreeValues();
            rules.Clear();
            HUD.hasResetBeenPressed = false;
            HUD.Start();
        }
    }

    void Generate(){

        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;

        Destroy(LSystem);

        LSystem = Instantiate(parent);

        currentStr = axiom;

        //calculate the string 
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < iterations; i++) {
            foreach (char c in currentStr) {
                sb.Append(rules.ContainsKey(c) ? rules[c] : c.ToString());
            }

            currentStr = sb.ToString();
            sb = new StringBuilder();
        }

        Debug.Log("current string: " + currentStr);
        Debug.Log("Axiom: " + axiom);
        foreach(var item in rules) {
            Debug.Log("rules: " + item.Key + " => " + item.Value);
        }


        /* render
         * F:draw the line upward, any other input: ignore
         */
        for (int i = 0; i < currentStr.Length; i++) {
            switch (currentStr[i]) {
                case 'F':
                    initPos = transform.position;
                    transform.Translate(Vector3.up * length);

                    GameObject renderedLine = Instantiate(line);
                    renderedLine.transform.SetParent(LSystem.transform);
                    renderedLine.GetComponent<LineRenderer>().SetPosition(0, initPos);
                    renderedLine.GetComponent<LineRenderer>().SetPosition(1, transform.position);
                    renderedLine.GetComponent<LineRenderer>().startWidth = width;
                    renderedLine.GetComponent<LineRenderer>().endWidth = width;
                    break;

                case '+':
                    transform.Rotate(Vector3.back * angle);
                    break;

                case '-':
                    transform.Rotate(Vector3.forward * angle);
                    break;

                case '[':
                    transformStack.Push(new TransformInfo()
                    {
                        position = transform.position,
                        rotation = transform.rotation
                    });
                    break;

                case ']':
                    TransformInfo ti = transformStack.Pop();
                    transform.position = ti.position;
                    transform.rotation = ti.rotation;
                    break;

                default:
                    break;
            }
        }
    }

    private void ResetTreeValues() {
        iterations = 3;
        angle = 30f;
        width = 1f;
        length = 2.5f;
    }
}