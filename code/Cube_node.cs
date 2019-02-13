using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets
{

    public class Cube_node
    {
        public string objectName = "";
        public float[] offset;
        public float[] boxmin;
        public float[] boxmax;
        public float[] pose;
        public float[] rotxlimit;
        public float[] rotylimit;
        public float[] rotzlimit;

        public Cube_node father;
        public List<Cube_node> child = new List<Cube_node>();

        public Cube_node(string objectName, Cube_node father, float[] offset, float[] boxmin, float[] boxmax, float[] pose, float[] rotxlimit, float[] rotylimit, float[] rotzlimit)
        {
            this.objectName = objectName;
            this.offset = offset;
            this.boxmin = boxmin;
            this.boxmax = boxmax;
            this.pose = pose;
            this.rotxlimit = rotxlimit;
            this.rotylimit = rotylimit;
            this.rotzlimit = rotzlimit;
            this.father = father;
        }

        public Cube_node(string objectName, float[] offset, float[] boxmin, float[] boxmax, float[] pose, float[] rotxlimit, float[] rotylimit, float[] rotzlimit)
        {
            this.objectName = objectName;
            this.offset = offset;
            this.boxmin = boxmin;
            this.boxmax = boxmax;
            this.pose = pose;
            this.rotxlimit = rotxlimit;
            this.rotylimit = rotylimit;
            this.rotzlimit = rotzlimit;
        }

        public void setFather(Cube_node father)
        {
            this.father = father;
        }

        public void addChild(Cube_node childNode)
        {
            this.child.Add(childNode);
        }
    }

}
