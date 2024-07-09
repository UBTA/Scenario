using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using EblanDev.ScenarioCore.UIFramework.UIElements;
using EblanDev.ScenarioCore.UtilsFramework.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EblanDev.ScenarioCore.Systems.TutorialSystemUnit
{
    public class TutorialCanvas : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        [SerializeField] private View hand;
        [SerializeField] private Image shadow;
        [SerializeField] private TextMeshProUGUI sign;
        [SerializeField] private View signView;

        private float handTime;
        private float handDelay;

        public void Init()
        {
            HandAnim();
        }

        public TutorialCanvas ShowSign(string text)
        {
            sign.text = text;
            signView.Show();
            return this;
        }

        public TutorialCanvas HideSign()
        {
            signView.Hide();
            return this;
        }
        
        public TutorialCanvas ShowHand(GameObject worldObj, float tapTime, float tapDelay = 0f)
        {
            var scrPos = cam.WorldToScreenPoint(worldObj.transform.position);
            hand.transform.position = scrPos;
            handTime = tapTime;
            handDelay = tapDelay;
            hand.Show();
            
            return this;
        }
        
        public TutorialCanvas ShowHand(Vector3 pos, float tapTime, float tapDelay = 0f)
        {
            hand.transform.position = pos;
            handTime = tapTime;
            handDelay = tapDelay;
            hand.Show();
            
            return this;
        }
        
        public TutorialCanvas ShowHand(Transform rect, float tapTime, float tapDelay = 0f)
        {
            hand.transform.position = rect.position;
            handTime = tapTime;
            handDelay = tapDelay;
            hand.Show();
            
            return this;
        }

        public TutorialCanvas HideHand()
        {
            hand.Hide();
            handTime = -1f;
            return this;
        }

        public TutorialCanvas ShowShadow(GameObject worldObj, Vector2 size)
        {
            Vector2 screenSize = new Vector2(Screen.width, Screen.height);
            Vector2 screenCenter = screenSize / 2;
            Vector2 rectCenterOnScreen = cam.WorldToScreenPoint(worldObj.transform.position);
            Vector2 differenceFromCenter = rectCenterOnScreen - screenCenter;
            float xPositionPercentage = (differenceFromCenter.x / (screenSize.x / 2)) * 1;
            float yPositionPercentage = (differenceFromCenter.y / (screenSize.y / 2)) * 1;
            
            ShowShadow(new Vector4(xPositionPercentage, yPositionPercentage, size.x, size.y));
            return this;
        }
        
        public TutorialCanvas ShowShadow(Vector3 worldPos, Vector2 size)
        {
            Vector2 screenSize = new Vector2(Screen.width, Screen.height);
            Vector2 screenCenter = screenSize / 2;
            Vector2 rectCenterOnScreen = cam.WorldToScreenPoint(worldPos);
            Vector2 differenceFromCenter = rectCenterOnScreen - screenCenter;
            float xPositionPercentage = (differenceFromCenter.x / (screenSize.x / 2)) * 1;
            float yPositionPercentage = (differenceFromCenter.y / (screenSize.y / 2)) * 1;
            
            ShowShadow(new Vector4(xPositionPercentage, yPositionPercentage, size.x, size.y));
            return this;
        }

        public TutorialCanvas ShowShadow(RectTransform rect)
        {
            Vector2 screenSize = new Vector2(Screen.width, Screen.height);

            Vector3[] corners = new Vector3[4];

            rect.GetWorldCorners(corners);
            Vector2 bottomLeft = RectTransformUtility.WorldToScreenPoint(cam, corners[0]);
            Vector2 topRight = RectTransformUtility.WorldToScreenPoint(cam, corners[2]);

            Vector2 rectSizeOnScreen = topRight - bottomLeft;

            float widthPercentage = (rectSizeOnScreen.x / screenSize.x) * 1;
            float heightPercentage = (rectSizeOnScreen.y / screenSize.y) * 1;

            Vector2 screenCenter = screenSize / 2;

            Vector2 rectCenterOnScreen;
            rectCenterOnScreen = RectTransformUtility.WorldToScreenPoint(cam, rect.position);

            Vector2 differenceFromCenter = rectCenterOnScreen - screenCenter;

            float xPositionPercentage = (differenceFromCenter.x / (screenSize.x / 2)) * 1;
            float yPositionPercentage = (differenceFromCenter.y / (screenSize.y / 2)) * 1;

            ShowShadow(new Vector4(xPositionPercentage, yPositionPercentage, widthPercentage, heightPercentage));
            return this;
        }

        private Tween shadowAnim;
        private void ShowShadow(Vector4 rect)
        {
            shadowAnim?.Kill();
            var start = shadow.material.GetVector("_RectCenterSize");
            
            shadow.gameObject.SetActive(true);
            
            shadowAnim = DOVirtual.Float(0f, 1f, 0.5f, (v) =>
            {
                shadow.material.SetVector("_RectCenterSize", Vector4.Lerp(start, rect, v));
            });
        }

        public TutorialCanvas HideShadow()
        {
            shadowAnim?.Kill();
            var hidenRect = new Vector4(0, 0, 1, 1);
            var currRect = shadow.material.GetVector("_RectCenterSize");
            
            shadowAnim = DOVirtual.Float(0f, 1f, 0.3f, (v) =>
                {
                    shadow.material.SetVector("_RectCenterSize", Vector4.Lerp(currRect, hidenRect, v));
                })
                .OnComplete(() =>
                {

                    shadow.gameObject.SetActive(true);
                });
            return this;
        }

        private async void HandAnim()
        {
            while (true)
            {
                if (hand.isActiveAndEnabled && handTime > 0f)
                {
                    await hand.transform.DOPunchScale(Vector3.one * 0.3f, handTime, 3).AsyncWaitForCompletion(CancellationToken.None);
                    await UniTask.Delay((int) (handDelay * 1000));
                }
                else
                {
                    await UniTask.Delay(100);
                }
            }
        }
    }
}