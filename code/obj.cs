using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using System.Text;
using Assets;
using UnityEngine.UI;

[RequireComponent(typeof(SkinnedMeshRenderer))]

public class obj : MonoBehaviour {

    private string firstName;
    float start_time, end_time;
    int numChannels;
    public Material mat;
    public OneJointAnim[] anims;
    public Transform[] bones;
    List<Cube_node> cnodes = new List<Cube_node>();
    public Transform[] parseSkelFile(string filePath)
    {
        List<Transform> bones = new List<Transform>();
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
        Stack<Cube_node> stackCube = new Stack<Cube_node>();

        List<string> lines = new List<string>(File.ReadAllLines(filePath));

        string childName = "";
        Cube_node root = null;
        //Cube_node childNode;
        string[] childSplit;
        float[] nowOffset = { 0, 0, 0 };
        float[] nowBoxmin = { -0.1f, -0.1f, -0.1f };
        float[] nowBoxmax = { 0.1f, 0.1f, 0.1f };
        float[] nowPose = { 0, 0, 0 };
        float[] nowRotxlimit = { -100000, 100000 };
        float[] nowRotylimit = { -100000, 100000 };
        float[] nowRotzlimit = { -100000, 100000 };

        for (int i = 0; i < lines.Count; i++)
        {

            if (lines[i].Contains("{") && i == 0)
            {
                childSplit = lines[i].Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                childName = childSplit[1];
                firstName = childSplit[1];
                i = i + 1;
                //print (childName);
                while (!lines[i].Contains("{") && !lines[i].Contains("}"))
                {
                    if (lines[i].Contains("offset"))
                    {
                        childSplit = lines[i].Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        nowOffset[0] = float.Parse(childSplit[1]);
                        nowOffset[1] = float.Parse(childSplit[2]);
                        nowOffset[2] = float.Parse(childSplit[3]);
                        i = i + 1;
                        continue;
                    }

                    if (lines[i].Contains("boxmin"))
                    {
                        childSplit = lines[i].Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        nowBoxmin[0] = float.Parse(childSplit[1]);
                        nowBoxmin[1] = float.Parse(childSplit[2]);
                        nowBoxmin[2] = float.Parse(childSplit[3]);
                        i = i + 1;
                        continue;
                    }

                    if (lines[i].Contains("boxmax"))
                    {
                        childSplit = lines[i].Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        nowBoxmax[0] = float.Parse(childSplit[1]);
                        nowBoxmax[1] = float.Parse(childSplit[2]);
                        nowBoxmax[2] = float.Parse(childSplit[3]);
                        i = i + 1;
                        continue;
                    }

                    if (lines[i].Contains("pose"))
                    {
                        childSplit = lines[i].Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        nowPose[0] = float.Parse(childSplit[1]);
                        nowPose[1] = float.Parse(childSplit[2]);
                        nowPose[2] = float.Parse(childSplit[3]);
                        i = i + 1;
                        continue;
                    }

                    if (lines[i].Contains("rotxlimit"))
                    {
                        childSplit = lines[i].Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        nowRotxlimit[0] = float.Parse(childSplit[1]);
                        nowRotxlimit[1] = float.Parse(childSplit[2]);
                        i = i + 1;
                        continue;
                    }

                    if (lines[i].Contains("rotylimit"))
                    {
                        childSplit = lines[i].Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        nowRotylimit[0] = float.Parse(childSplit[1]);
                        nowRotylimit[1] = float.Parse(childSplit[2]);
                        i = i + 1;
                        continue;
                    }

                    if (lines[i].Contains("rotzlimit"))
                    {
                        childSplit = lines[i].Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        nowRotzlimit[0] = float.Parse(childSplit[1]);
                        nowRotzlimit[1] = float.Parse(childSplit[2]);
                        i = i + 1;
                        continue;
                    }
                }

                root = new Cube_node(childName, nowOffset, nowBoxmin, nowBoxmax, nowPose, nowRotxlimit, nowRotylimit, nowRotzlimit);
                //tree = new Create_tree (root);
                drawMesh(options,bones, root);
                stackCube.Push(root);
                //continue;
            }
            
            Cube_node childNode = null;

            if (lines[i].Contains("{"))
            {
                childSplit = lines[i].Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                childName = childSplit[1];
                i = i + 1;

                while (!lines[i].Contains("{") && !lines[i].Contains("}"))
                {
                    if (lines[i].Contains("offset"))
                    {
                        childSplit = lines[i].Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        nowOffset[0] = float.Parse(childSplit[1]);
                        nowOffset[1] = float.Parse(childSplit[2]);
                        nowOffset[2] = float.Parse(childSplit[3]);
                        i = i + 1;
                        continue;
                    }

                    if (lines[i].Contains("boxmin"))
                    {
                        childSplit = lines[i].Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        nowBoxmin[0] = float.Parse(childSplit[1]);
                        nowBoxmin[1] = float.Parse(childSplit[2]);
                        nowBoxmin[2] = float.Parse(childSplit[3]);
                        i = i + 1;
                        continue;
                    }

                    if (lines[i].Contains("boxmax"))
                    {
                        childSplit = lines[i].Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        nowBoxmax[0] = float.Parse(childSplit[1]);
                        nowBoxmax[1] = float.Parse(childSplit[2]);
                        nowBoxmax[2] = float.Parse(childSplit[3]);
                        i = i + 1;
                        continue;
                    }

                    if (lines[i].Contains("pose"))
                    {
                        childSplit = lines[i].Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        nowPose[0] = float.Parse(childSplit[1]);
                        nowPose[1] = float.Parse(childSplit[2]);
                        nowPose[2] = float.Parse(childSplit[3]);
                        i = i + 1;
                        continue;
                    }

                    if (lines[i].Contains("rotxlimit"))
                    {
                        childSplit = lines[i].Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        nowRotxlimit[0] = float.Parse(childSplit[1]);
                        nowRotxlimit[1] = float.Parse(childSplit[2]);
                        i = i + 1;
                        continue;
                    }

                    if (lines[i].Contains("rotylimit"))
                    {
                        childSplit = lines[i].Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        nowRotylimit[0] = float.Parse(childSplit[1]);
                        nowRotylimit[1] = float.Parse(childSplit[2]);
                        i = i + 1;
                        continue;
                    }

                    if (lines[i].Contains("rotzlimit"))
                    {
                        childSplit = lines[i].Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        nowRotzlimit[0] = float.Parse(childSplit[1]);
                        nowRotzlimit[1] = float.Parse(childSplit[2]);
                        i = i + 1;
                        continue;
                    }
                }
                Cube_node fatherNode = stackCube.Peek();
                childNode = new Cube_node(childName, fatherNode, nowOffset, nowBoxmin, nowBoxmax, nowPose, nowRotxlimit, nowRotylimit, nowRotzlimit);
                drawMesh(options,bones, childNode);
                stackCube.Push(childNode);
                i = i - 1;
                continue;
            }
            if (lines[i].Contains("}") && stackCube.Count != 0)
            {
                stackCube.Pop();
            }
            
        }
        GameObject.Find("Dropdown").GetComponent<Dropdown>().AddOptions(options);
        return bones.ToArray();
    }

    public void drawMesh(List<Dropdown.OptionData> options,List<Transform> bones, Cube_node cnode)
    {
        GameObject gameObject = new GameObject(cnode.objectName);
        options.Add(new Dropdown.OptionData(cnode.objectName));
        if (cnode.objectName != firstName)
        {
            string fatherName = cnode.father.objectName;
            gameObject.transform.SetParent(GameObject.Find(fatherName).transform);
        }
        gameObject.transform.localPosition = new Vector3(cnode.offset[0], cnode.offset[1], cnode.offset[2]);

        float rotx = Mathf.Clamp(cnode.pose[0] * Mathf.Rad2Deg, cnode.rotxlimit[0] * Mathf.Rad2Deg, cnode.rotxlimit[1] * Mathf.Rad2Deg);
        float roty = Mathf.Clamp(cnode.pose[1] * Mathf.Rad2Deg, cnode.rotylimit[0] * Mathf.Rad2Deg, cnode.rotylimit[1] * Mathf.Rad2Deg);
        float rotz = Mathf.Clamp(cnode.pose[2] * Mathf.Rad2Deg, cnode.rotzlimit[0] * Mathf.Rad2Deg, cnode.rotzlimit[1] * Mathf.Rad2Deg);

        gameObject.transform.localRotation = Quaternion.AngleAxis(rotz, Vector3.forward) * Quaternion.AngleAxis(roty, Vector3.up) * Quaternion.AngleAxis(rotx, Vector3.right);
        bones.Add(gameObject.transform);
        Debug.Log(cnode.objectName);
        cnodes.Add(cnode);
        //Debug.Log("*******************************"+bones.Count);
    }

    public void Read(string path, List<Vector3> verticesList, List<Vector3> normalsList, List<BoneWeight> boneWeights, 
        List<int> trangles, List<Matrix4x4> bindposes)
    {
        StreamReader sr = new StreamReader(path, Encoding.Default);
        String line;
        String[] strs;
        int count2 = 0;
        int flag = -1;
        while ((line = sr.ReadLine()) != null)
        {
            if (line.IndexOf("positions") == 0)
            {
                flag = 0;
                continue;
            }
            else if (line.IndexOf("}") == 0)
            {
                flag = -1;
                continue;
            }
            else if(line.IndexOf("normals") == 0)
            {
                flag = 1;
                continue;
            }
            else if(line.IndexOf("skinweights") == 0)
            {
                flag = 2;
                continue;
            }
            else if (line.IndexOf("triangles") == 0)
            {
                flag = 3;
                continue;
            }
            else if (line.IndexOf("bindings") == 0)
            {
                flag = 4;
                continue;
            }
            
            switch (flag)
            {
                case 0:
                    {
                        strs = line.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        float a, b, c;
                        a = float.Parse(strs[0]);
                        b = float.Parse(strs[1]);
                        c = float.Parse(strs[2]);
                        verticesList.Add(new Vector3(a, b, c));
                        break;
                    }
                case 1:
                    {
                        strs = line.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        float a, b, c;
                        a = float.Parse(strs[0]);
                        b = float.Parse(strs[1]);
                        c = float.Parse(strs[2]);
                        normalsList.Add(new Vector3(a, b, c));
                        break;
                    }
                case 2:
                    {
                        strs = line.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        int n = int.Parse(strs[0]);
                        BoneWeight bw = new BoneWeight();
                        for (int i = 0; i < n; i++)
                        {
                            int a = int.Parse(strs[i*2+1]);
                            float b = float.Parse(strs[i*2+2]);
                            switch (i)
                            {
                                case 0:
                                    bw.boneIndex0 = a;
                                    bw.weight0 = b;
                                    break;
                                case 1:
                                    bw.boneIndex1 = a;
                                    bw.weight1 = b;
                                    break;
                                case 2:
                                    bw.boneIndex2 = a;
                                    bw.weight2 = b;
                                    break;
                                case 3:
                                    bw.boneIndex3 = a;
                                    bw.weight3 = b;
                                    break;
                            }
                        }
                        boneWeights.Add(bw);
                        count2++;
                        break;
                    }
                case 3:
                    {
                        strs = line.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        int a, b, c;
                        a = int.Parse(strs[0]);
                        b = int.Parse(strs[1]);
                        c = int.Parse(strs[2]);
                        trangles.Add(a);
                        trangles.Add(b);
                        trangles.Add(c);
                        break;
                    }
                case 4:
                    {
                        Matrix4x4 newMatrix = new Matrix4x4();
                        newMatrix.m30 = 0;
                        newMatrix.m31 = 0;
                        newMatrix.m32 = 0;
                        newMatrix.m33 = 1;
                        if (line.IndexOf("matrix") != -1)
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                line = sr.ReadLine();
                                strs = line.Split(new Char[] { ' ','\t' }, StringSplitOptions.RemoveEmptyEntries);
                                float a, b, c;
                                a = float.Parse(strs[0]);
                                b = float.Parse(strs[1]);
                                c = float.Parse(strs[2]);
                                switch(i)
                                {
                                    case 0:
                                        newMatrix.m00 = a;
                                        newMatrix.m10 = b;
                                        newMatrix.m20 = c;
                                        break;
                                    case 1:
                                        newMatrix.m01 = a;
                                        newMatrix.m11 = b;
                                        newMatrix.m21 = c;
                                        break;
                                    case 2:
                                        newMatrix.m02 = a;
                                        newMatrix.m12 = b;
                                        newMatrix.m22 = c;
                                        break;
                                    case 3:
                                        newMatrix.m03 = a;
                                        newMatrix.m13 = b;
                                        newMatrix.m23 = c;
                                        break;
                                }
                            }
                            newMatrix = newMatrix.inverse;
                            //Debug.logger.Log(newMatrix.m00 + " " + newMatrix.m01 + " " + newMatrix.m02 + " " + newMatrix.m03);
                            //Debug.logger.Log(newMatrix.m10 + " " + newMatrix.m11 + " " + newMatrix.m12 + " " + newMatrix.m13);
                            //Debug.logger.Log(newMatrix.m20 + " " + newMatrix.m21 + " " + newMatrix.m22 + " " + newMatrix.m23);
                            //Debug.logger.Log(newMatrix.m30 + " " + newMatrix.m31 + " " + newMatrix.m32 + " " + newMatrix.m33);
                            bindposes.Add(newMatrix);
                        }
                        sr.ReadLine();
                        break;
                    }
            }
        }
        Debug.logger.Log(normalsList.Count);

        Debug.logger.Log(verticesList.Count);

        Debug.logger.Log(boneWeights.Count);

        Debug.logger.Log(trangles.Count);

        Debug.logger.Log(bindposes.Count);

    }
    // Use this for initialization
    void Start () {
        StartView("Assets/wasp_walk");
    }
	public void StartView(string filename)
    {
        bones = parseSkelFile(filename + ".skel");
        GetComponent<SkinnedMeshRenderer>().bones = bones;
        Mesh mesh = parseSkinFile(filename + ".skin");
        GetComponent<SkinnedMeshRenderer>().sharedMesh = mesh;

        anims = parseAnimFile(filename + ".anim");
        Debug.Log("------------:" + anims.Length);
        preCal();
    }
    void preCal()
    {
        foreach(OneJointAnim ani in anims)
        {
            preCal(ani.channel_x.keyList.ToArray());
            preCal(ani.channel_y.keyList.ToArray());
            preCal(ani.channel_z.keyList.ToArray());
        }
    }

    void preCal(KeyFrame[] kf)
    {
        for(int i = 0; i < kf.Length; i++)
        {
            

            if (i == 0)
            {
                switch (kf[i].tangent_out)
                {
                    case tangent_type.flat:
                        kf[i].tan_out_val = 0;
                        break;
                    case tangent_type.linear:
                        kf[i].tan_out_val = (kf[i + 1].value - kf[i].value) / (kf[i + 1].time - kf[i].time);
                        break;
                }
                kf[i].tan_in_val = kf[i].tan_out_val;
            }
            else if(i == kf.Length-1)
            {
                switch (kf[i].tangent_in)
                {
                    case tangent_type.flat:
                        kf[i].tan_in_val = 0;
                        break;
                    case tangent_type.linear:
                        kf[i].tan_in_val = (kf[i].value - kf[i - 1].value) / (kf[i].time - kf[i - 1].time);
                        break;
                }
                kf[i].tan_out_val = kf[i].tan_in_val;
            }
            else
            {
                switch (kf[i].tangent_out)
                {
                    case tangent_type.flat:
                        kf[i].tan_out_val = 0;
                        break;
                    case tangent_type.linear:
                        kf[i].tan_out_val = (kf[i + 1].value - kf[i].value) / (kf[i + 1].time - kf[i].time);
                        break;
                    case tangent_type.smooth:
                        kf[i].tan_out_val = (kf[i + 1].value - kf[i - 1].value) / (kf[i + 1].time - kf[i - 1].time);
                        break;
                }
                switch (kf[i].tangent_in)
                {
                    case tangent_type.flat:
                        kf[i].tan_in_val = 0;
                        break;
                    case tangent_type.linear:
                        kf[i].tan_in_val = (kf[i].value - kf[i - 1].value) / (kf[i].time - kf[i - 1].time);
                        break;
                    case tangent_type.smooth:
                        kf[i].tan_in_val = (kf[i + 1].value - kf[i - 1].value) / (kf[i + 1].time - kf[i - 1].time);
                        break;
                }
            }
                    
                
        }
        for (int i = 0; i < kf.Length - 1; i++)
        {
            if (kf[i].tangent_in == tangent_type.flat)
            {
                kf[i].a = kf[i].b = kf[i].c = 0;
                kf[i].d = kf[i].value;
                continue;
            }
            Matrix4x4 v = new Matrix4x4();
            v.m00 = kf[i].value;
            v.m01 = kf[i + 1].value;
            v.m02 = (kf[i + 1].time - kf[i].time) * kf[i].tan_out_val;
            v.m03 = (kf[i + 1].time - kf[i].time) * kf[i + 1].tan_in_val;
            Matrix4x4 magic;
            magic.m00 = 2;
            magic.m01 = -3;
            magic.m02 = 0;
            magic.m03 = 1;
            magic.m10 = -2;
            magic.m11 = 3;
            magic.m12 = 0;
            magic.m13 = 0;
            magic.m20 = 1;
            magic.m21 = -2;
            magic.m22 = 1;
            magic.m23 = 0;
            magic.m30 = 1;
            magic.m31 = -1;
            magic.m32 = 0;
            magic.m33 = 0;
            Matrix4x4 res = v * magic;
            kf[i].a = res.m00;
            kf[i].b = res.m01;
            kf[i].c = res.m02;
            kf[i].d = res.m03;
        }
    }
    OneJointAnim[] parseAnimFile(string filename)
    {
        List<OneJointAnim> anims = new List<OneJointAnim>();
        ReadAnimFile(filename, anims);
        return anims.ToArray();
    }

    public void ReadAnimFile(string fileName, List<OneJointAnim> anims)
    {
        StreamReader sr = new StreamReader(fileName, Encoding.Default);
        String line;
        String[] strs;
        OneJointAnim one = new OneJointAnim();
        Channel channel = new Channel();
        List<KeyFrame> kl = new List<KeyFrame>();
        int count = 0;
        while ((line = sr.ReadLine()) != null)
        {
            if (line.IndexOf("range") != -1)
            {
                strs = line.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                start_time = float.Parse(strs[1]);
                end_time = float.Parse(strs[2]);
               
            }
            else if(line.IndexOf("numchannels") != -1)
            {
                strs = line.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                numChannels = int.Parse( strs[1]);
            }
            else if(line.IndexOf("channel") != -1)
            {

                count++;
               // Debug.Log(count);
                if (kl.Count == 0)
                {
                    continue;
                }
                else
                {
                    channel.keyList = kl;
                    kl = new List<KeyFrame>();
                    if ((count-1)%3 == 1)
                    {
                        one.channel_x = channel;
                        channel = new Channel();
                    }
                    else if((count - 1) % 3 == 2)
                    {
                        one.channel_y = channel;
                        channel = new Channel();
                    }
                    else if ((count - 1) % 3 == 0)
                    {
                        one.channel_z = channel;
                        anims.Add(one);
                        channel = new Channel();
                        one = new OneJointAnim();
                    }
                }
            }
            else if(line.IndexOf("extrapolate") != -1)
            {
                strs = line.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (strs[1].Contains("constant"))
                    channel.extrap_in = extrapolate_type.constant;
                
                else if (strs[1].Contains("cycle_offset"))
                    channel.extrap_in = extrapolate_type.cycle_offset;
                else if (strs[1].Contains("cycle"))
                    channel.extrap_in = extrapolate_type.cycle;
                else if (strs[1].Contains("linear"))
                    channel.extrap_in = extrapolate_type.linear;
                else if (strs[1].Contains("bounce"))
                    channel.extrap_in = extrapolate_type.bounce;

                if (strs[2].Contains("constant"))
                    channel.extrap_out = extrapolate_type.constant;
                else if (strs[2].Contains("cycle_offset"))
                    channel.extrap_out = extrapolate_type.cycle_offset;
                else if (strs[2].Contains("cycle"))
                    channel.extrap_out = extrapolate_type.cycle;
                else if (strs[2].Contains("linear"))
                    channel.extrap_out = extrapolate_type.linear;
                else if (strs[2].Contains("bounce"))
                    channel.extrap_out = extrapolate_type.bounce;
            }
            else if(line.IndexOf("keys") != -1)
            {
                line = sr.ReadLine();
                //count++;
                while (!line.Contains("}"))
                {
                    KeyFrame kf = new KeyFrame();
                    
                    strs = line.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    //Debug.Log(count.ToString());
                    if(strs[2].Contains("smooth"))
                    {
                        kf.tangent_in = tangent_type.smooth;
                    }
                    else if(strs[2].Contains("flat"))
                    {
                        kf.tangent_in = tangent_type.flat;
                    }
                    else if (strs[2].Contains("linear"))
                    {
                        kf.tangent_in = tangent_type.linear;
                    }

                    if (strs[3].Contains("smooth"))
                    {
                        kf.tangent_out = tangent_type.smooth;
                    }
                    else if (strs[3].Contains("flat"))
                    {
                        kf.tangent_out = tangent_type.flat;
                    }
                    else if (strs[3].Contains("linear"))
                    {
                        kf.tangent_out = tangent_type.linear;
                    }

                    kf.time = float.Parse(strs[0]);
                    kf.value = float.Parse(strs[1]);

                    kl.Add(kf);
                    line = sr.ReadLine();
                    //count++;
                }
            }
        }
        channel.keyList = kl;
        one.channel_z = channel;
        anims.Add(one);
    }

    public Mesh parseSkinFile(string fileName)
    {
        List<Vector3> verticesList = new List<Vector3>();
        List<Vector3> normalsList = new List<Vector3>();
        List<int> trangles = new List<int>();
        List<BoneWeight> boneWeights = new List<BoneWeight>();
        List<Matrix4x4> bindposes = new List<Matrix4x4>();
        Read(fileName, verticesList, normalsList, boneWeights, trangles, bindposes);
        Mesh mesh = new Mesh();
        mesh.name = "myMesh";
        mesh.SetVertices(verticesList);
        mesh.SetNormals(normalsList);
        mesh.boneWeights = boneWeights.ToArray();
        mesh.triangles = trangles.ToArray();
        mesh.bindposes = bindposes.ToArray();
        return mesh;
    }

    // Update is called once per frame
    void Update () {
        //Debug.Log("current time: "+Time.time+", value: "+ CalValue(Time.time, anims[anims.Length-3].channel_x));

        //GameObject.Find("root").transform.position = new Vector3(Time.time, animation.)
        //return;
        
        for(int i = 0; i < bones.Length; i++)
        {
            int tmp = i+1;
            if (i == 0) tmp = 0;
            //bones[i].transform.localPosition = (new Vector3(CalValue(Time.time, anims[tmp].channel_x),\
            //     CalValue(Time.time, anims[tmp].channel_y), CalValue(Time.time, anims[tmp].channel_z)));
            
            cnodes[i].pose[0] = CalValue(Time.time, anims[tmp].channel_x);
            cnodes[i].pose[1] = CalValue(Time.time, anims[tmp].channel_y);
            cnodes[i].pose[2] = CalValue(Time.time, anims[tmp].channel_z);
            if (i == 0)
            {
                bones[i].transform.localPosition = new Vector3(cnodes[i].pose[0], cnodes[i].pose[1], cnodes[i].pose[2]);
                continue;
            }
            float rotx = Mathf.Clamp(cnodes[i].pose[0] * Mathf.Rad2Deg, cnodes[i].rotxlimit[0] * Mathf.Rad2Deg, cnodes[i].rotxlimit[1] * Mathf.Rad2Deg);
            float roty = Mathf.Clamp(cnodes[i].pose[1] * Mathf.Rad2Deg, cnodes[i].rotylimit[0] * Mathf.Rad2Deg, cnodes[i].rotylimit[1] * Mathf.Rad2Deg);
            float rotz = Mathf.Clamp(cnodes[i].pose[2] * Mathf.Rad2Deg, cnodes[i].rotzlimit[0] * Mathf.Rad2Deg, cnodes[i].rotzlimit[1] * Mathf.Rad2Deg);

            bones[i].transform.localRotation = Quaternion.AngleAxis(rotz, Vector3.forward) * Quaternion.AngleAxis(roty, Vector3.up) * Quaternion.AngleAxis(rotx, Vector3.right);
        
        }

    }

    float CalValue(float time, Channel channel)
    {
        if (time < start_time ) return 0;
        KeyFrame[] karr = channel.keyList.ToArray();
        float ret = 0;
        float retoffset = 0;
        if(time < karr[0].time)
        {
            //Debug.Log("time < karr[0].time");
            switch(channel.extrap_in)
            {
                case extrapolate_type.constant:
                    return karr[0].value;
                case extrapolate_type.cycle:
                    while (time < karr[0].time)
                        time += karr[karr.Length - 1].time - karr[0].time;
                    break;
                case extrapolate_type.cycle_offset:
                    while (time < karr[0].time)
                    {
                        time += karr[karr.Length - 1].time - karr[0].time;
                        retoffset -= karr[karr.Length - 1].value - karr[0].value;
                    }
                    break;
                case extrapolate_type.linear:
                    retoffset = (karr[0].time - time) * karr[0].tan_in_val;
                    time = karr[0].time;
                    break;
            }
        }
        else if(time > karr[karr.Length-1].time)
        {
            //Debug.Log("time > karr[karr.Length-1].time");
            //Debug.Log("-----------------------------"+channel.extrap_out);
            switch (channel.extrap_out)
            {
                case extrapolate_type.constant:
                    return karr[karr.Length - 1].value;
                case extrapolate_type.cycle:
                    while (time > karr[karr.Length - 1].time)
                        time -= karr[karr.Length - 1].time - karr[0].time;
                    break;
                case extrapolate_type.cycle_offset:
                    while (time > karr[karr.Length - 1].time)
                    {
                        time -= karr[karr.Length - 1].time - karr[0].time;
                        retoffset += karr[karr.Length - 1].value - karr[0].value;
                    }
                    break;
                case extrapolate_type.linear:
                    retoffset = (karr[karr.Length - 1].time - time) * karr[karr.Length - 1].tan_out_val;
                    time = karr[karr.Length - 1].time;
                    break;
            }
        }
        //Debug.Log("-----------------------------"+time);
        for(int i = 0; i < karr.Length - 1; i++)
        {
            if(time>=karr[i].time && time <= karr[i+1].time)
            {
                float u = (time - karr[i].time) / (karr[i + 1].time - karr[i].time);
                ret = u * u * u * karr[i].a + u * u * karr[i].b + u * karr[i].c + karr[i].d;
                break;
            }
        }
        //Debug.Log("ret: " + ret + "offset: " + retoffset);
        return ret+retoffset;
    }


}
