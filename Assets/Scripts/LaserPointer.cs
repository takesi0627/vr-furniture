using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using hekira.Utilities;

namespace hekira
{
    public class LaserPointer : OVRCursor
    {
        static LaserPointer _instance;
        public static LaserPointer instance
        {
            get { return _instance; }
        }


        [SerializeField]
        private Transform _RightHandAnchor; // 右手

        [SerializeField]
        private Transform _LeftHandAnchor;  // 左手

        [SerializeField]
        private Transform _CenterEyeAnchor; // 目の中心

        [SerializeField]
        private float _MaxDistance = 100.0f; // 距離

        [SerializeField]
        private LineRenderer _LaserPointerRenderer; // LineRenderer

        GameObject _HitObj;
        public GameObject hitObj
        {
            get { return _HitObj; }
        }

        // コントローラー
        private Transform Pointer
        {
            get
            {
                // 現在アクティブなコントローラーを取得
                var controller = OVRInput.GetActiveController();
                if (controller == OVRInput.Controller.RTrackedRemote)
                {
                    return _RightHandAnchor;
                }
                else if (controller == OVRInput.Controller.LTrackedRemote)
                {
                    return _LeftHandAnchor;
                }
                // どちらも取れなければ目の間からビームが出る
                return _CenterEyeAnchor;
            }
        }

        public Transform Controller
        {
            get { return Pointer; }
        }

        void Start()
        {
            if (_instance == null)
            {
                _instance = this;
            }
        }

        void Update()
        {
            var pointer = Pointer; // コントローラーを取得
                                   // コントローラーがない or LineRendererがなければ何もしない
            if (pointer == null || _LaserPointerRenderer == null)
            {
                return;
            }
            // コントローラー位置からRayを飛ばす
            Ray pointerRay = new Ray(pointer.position, pointer.forward);

            // レーザーの起点
            _LaserPointerRenderer.SetPosition(0, pointerRay.origin);

            RaycastHit hitInfo;
            if (Physics.Raycast(pointerRay, out hitInfo, _MaxDistance))
            {
                // Rayがヒットしたらそこまで
                _LaserPointerRenderer.SetPosition(1, hitInfo.point);
                /* ---以下追加した部分--- */
                // ヒットしたオブジェクトを取得
                _HitObj = hitInfo.collider.gameObject;

            }
            else
            {
                // Rayがヒットしなかったら向いている方向にMaxDistance伸ばす
                _LaserPointerRenderer.SetPosition(1, pointerRay.origin + pointerRay.direction * _MaxDistance);

                // Rayがヒットしてないときヒットしたオブジェクトを空にする
                _HitObj = null;
            }
        }

        public override void SetCursorRay(Transform ray)
        {

        }

        public override void SetCursorStartDest(Vector3 start, Vector3 dest, Vector3 normal)
        {

        }
    }
}