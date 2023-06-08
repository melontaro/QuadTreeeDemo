using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Coo;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace WCC.QuadTree
{
    public enum SceneObjStatus
    {
        Loading,    //加载中
        Loaded  //加载完毕
    }


    public class SceneObjData
    {
        public ObjData objData;
        public SceneObjStatus status;
        public GameObject obj;

        public SceneObjData(ObjData objData)
        {
            this.objData = objData;
            this.obj = null;
        }
    }

    public class ObjManager : MonoBehaviour
    {
        private Dictionary<int, SceneObjData> activeSceneObjDatas = new Dictionary<int, SceneObjData>();
        private List<int> unloadUids = new List<int>();

        private void Start()
        {
            UniTaskAsyncMessageBus.Inst.Subscribe<LoadObjMessage>(OnLoadObjMessage);
            UniTaskAsyncMessageBus.Inst.Subscribe<UnLoadObjMessage>(OnUnLoadObjMessage);
        }

        private UniTask OnUnLoadObjMessage(UnLoadObjMessage message, CancellationToken arg2)
        {
            Unload(message.ObjData.uid);
            return UniTask.CompletedTask;
        }

        private UniTask OnLoadObjMessage(LoadObjMessage message, CancellationToken arg2)
        {
         
            LoadAsync(message.ObjData);
           return UniTask.CompletedTask;
        }

         void LoadAsync(ObjData objData)
        {
            if (activeSceneObjDatas.ContainsKey(objData.uid))
                return;
            StartCoroutine(LoadObj(objData));
        }

         void Unload(int uid)
        {
            if (activeSceneObjDatas.ContainsKey(uid) && unloadUids.Contains(uid) == false)
            {
                unloadUids.Add(uid);
            }
            for (int i = 0; i < unloadUids.Count; i++)
            {
                if (activeSceneObjDatas[unloadUids[i]].status == SceneObjStatus.Loaded)
                {
                  //  Destroy(activeSceneObjDatas[unloadUids[i]].obj);
                    ObjData objData = activeSceneObjDatas[unloadUids[i]].objData;
                    Main.Inst.tilemap.SetTile(new Vector3Int((int)objData.pos.x,(int)objData.pos.y),null);
                    activeSceneObjDatas.Remove(unloadUids[i]);
                    unloadUids.RemoveAt(i--);
                }
            }
        }

        private IEnumerator LoadObj(ObjData obj)
        {
            SceneObjData sceneObjData = new SceneObjData(obj);
            sceneObjData.status = SceneObjStatus.Loading;
            activeSceneObjDatas.Add(obj.uid, sceneObjData);
            GameObject resObj = null;
            ResourceRequest request = Resources.LoadAsync<GameObject>(obj.resPath);
            yield return request;
            resObj = request.asset as GameObject;
            yield return new WaitUntil(() => resObj != null);

            sceneObjData.status = SceneObjStatus.Loaded;
           // SetObjTransfrom(resObj, sceneObjData);
           int num= Random.Range(1,3);
           var tilebase=  AssetDatabase.LoadAssetAtPath<TileBase>(string.Format("Assets/_test/{0}.asset",num) );
           Main.Inst.tilemap.SetTile(sceneObjData.objData.pos,tilebase);
        }

        private void SetObjTransfrom(GameObject prefab, SceneObjData sceneObj)
        {
            sceneObj.obj = Instantiate(prefab);
            sceneObj.obj.transform.position = sceneObj.objData.pos;
            sceneObj.obj.transform.rotation = sceneObj.objData.rot;
            sceneObj.obj.transform.localScale = sceneObj.objData.scale;
            var tilebase=  AssetDatabase.LoadAssetAtPath<TileBase>("Assets/_test/1.asset");
            Main.Inst.tilemap.SetTile(sceneObj.objData.pos,tilebase);
        }
    }

}