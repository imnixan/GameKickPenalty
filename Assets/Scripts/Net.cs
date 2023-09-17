using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : MonoBehaviour
{
    private const float NetWidth = 0.1f;

    [SerializeField]
    private Transform[] NetLines;

    [SerializeField]
    private Material ropeMat;

    public List<LineRenderer> netRopesHotizontal = new List<LineRenderer>();
    public List<LineRenderer> netRopesVertical = new List<LineRenderer>();

    private void Start()
    {
        Application.targetFrameRate = 300;
        CreateNet();
        CreateNetRopes();
    }

    private void CreateNet()
    {
        for (int lineNum = 0; lineNum < NetLines.Length; lineNum++)
        {
            int lineLength = NetLines[lineNum].childCount;
            for (int dotNum = 0; dotNum < lineLength; dotNum++)
            {
                int rightDot = dotNum + 1;
                if (rightDot < lineLength)
                {
                    CharacterJoint horJoint = NetLines[lineNum]
                        .GetChild(dotNum)
                        .gameObject.AddComponent<CharacterJoint>();
                    horJoint.connectedBody = NetLines[lineNum]
                        .GetChild(rightDot)
                        .gameObject.GetComponent<Rigidbody>();
                }
                int downDot = lineNum + 1;
                if (downDot < NetLines.Length && NetLines[lineNum].childCount > dotNum)
                {
                    CharacterJoint vertJoint = NetLines[lineNum]
                        .GetChild(dotNum)
                        .gameObject.AddComponent<CharacterJoint>();
                    vertJoint.connectedBody = NetLines[downDot]
                        .GetChild(dotNum)
                        .gameObject.GetComponent<Rigidbody>();
                }
            }
        }
    }

    private void CreateNetRopes()
    {
        for (int i = 0; i < NetLines.Length; i++)
        {
            netRopesHotizontal.Add(NetLines[i].GetChild(0).gameObject.AddComponent<LineRenderer>());
            netRopesHotizontal[i].positionCount = NetLines[i].childCount;
            netRopesHotizontal[i].startWidth = NetWidth;
            netRopesHotizontal[i].endWidth = netRopesHotizontal[i].startWidth;
            netRopesHotizontal[i].material = ropeMat;
        }

        for (int i = 0; i < NetLines[0].childCount; i++)
        {
            GameObject vertRend = new GameObject("VerticalRenderer");
            vertRend.transform.SetParent(NetLines[0].GetChild(i));
            vertRend.transform.localPosition = Vector3.zero;
            netRopesVertical.Add(vertRend.AddComponent<LineRenderer>());
            netRopesVertical[i].positionCount = NetLines.Length;
            netRopesVertical[i].startWidth = NetWidth;
            netRopesVertical[i].endWidth = netRopesVertical[i].startWidth;
            netRopesVertical[i].material = ropeMat;
        }
    }

    private void LateUpdate()
    {
        for (int i = 0; i < netRopesHotizontal.Count; i++)
        {
            UpdateHorizontalRope(netRopesHotizontal[i], NetLines[i]);
        }

        for (int i = 0; i < netRopesVertical.Count; i++)
        {
            UpdateVerticalRope(netRopesVertical[i], i);
        }
    }

    private void UpdateHorizontalRope(LineRenderer rope, Transform ropeTransform)
    {
        for (int i = 0; i < ropeTransform.childCount; i++)
        {
            rope.SetPosition(i, ropeTransform.GetChild(i).position);
        }
    }

    private void UpdateVerticalRope(LineRenderer rope, int ropeIndex)
    {
        for (int i = 0; i < NetLines.Length; i++)
        {
            rope.SetPosition(i, NetLines[i].GetChild(ropeIndex).position);
        }
    }
}
