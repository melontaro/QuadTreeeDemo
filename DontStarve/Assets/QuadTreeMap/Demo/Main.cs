using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace WCC.QuadTree
{
    public class Main : MonoBehaviour
    {
        public static Main Inst;
        public Bounds bounds;
        private Tree tree;
        [SerializeField] int objCount = 10000;
        [SerializeField] float viewRatio = 1;

        

        public Tilemap tilemap;
        // Start is called before the first frame update
        void Start()
        {
            if (Inst==null)
            {
                Inst= this;
            }
         
            tree = new Tree(bounds);
            /*
       for (int i = 0; i < objCount; i++)
       {
           Vector3 randomPosition = new Vector3(Random.Range(-1000f, 1000f), 0, Random.Range(-1000f, 1000f));
           Vector3 randomScale = Vector3.one * Random.Range(0.5f, 2f);
           ObjData objData = new ObjData("Cube", randomPosition, Quaternion.identity, randomScale, Vector3.one);
           objData.uid = i;
           tree.InsertObjData(objData);
       }
  */
            /*
            int uid = 0;
            var size = tilemap.size;
            int width= size.x/2;
            int height= size.y/2;
            for (int i = (width*-1); i < width; i++)
            {
                for (int j = (height*-1); j < height; j++)
                {
                    Vector3Int randomPosition = new Vector3Int(i, j);
                    Vector3 randomScale = Vector3.one;
                    ObjData objData = new ObjData("Cube", randomPosition, Quaternion.identity, randomScale,
                        Vector3.one);
                    objData.uid = uid;
                    uid++;
                    tree.InsertObjData(objData);
                }
            }*/
            int uid = 0;

            for (int i = (20*-1); i < 20; i++)
            {
                for (int j = (20*-1); j < 20; j++)
                {
                    Vector3Int randomPosition = new Vector3Int(i, j);
                    Vector3 randomScale = Vector3.one;
                    ObjData objData = new ObjData("Cube", randomPosition, Quaternion.identity, randomScale,
                        Vector3.one);
                    objData.uid = uid;
                    uid++;
                    tree.InsertObjData(objData);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            tree.viewRatio = viewRatio;
            tree.Inside(Camera.main);
        }


        private void OnDrawGizmos()
        {
            if (tree != null)
            {
                tree.DrawBound();
            }
            else
            {
                Gizmos.DrawWireCube(bounds.center, bounds.size);
            }
        }
    }
}