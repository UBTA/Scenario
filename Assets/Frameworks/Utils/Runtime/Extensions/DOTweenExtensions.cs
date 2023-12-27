using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace EblanDev.ScenarioCore.UtilsFramework.Extensions
{
	public static class DOTweenExtensions
	{
		public static async UniTask AsyncWaitForCompletion(this Tween t, CancellationToken token)
		{
			while (t.active && !t.IsComplete())
			{
				await UniTask.Yield(PlayerLoopTiming.FixedUpdate, token);
				
				if (token.IsCancellationRequested)
				{
					t.Kill();
				}
			}
		}

		public static Tween DOFrameMoveBy(this Transform tr, Vector3 offset, float duration)
		{
			var prevValue = 0.0f;
			
			return DOVirtual.Float(0.0f, 1.0f, duration, value =>
			{
				var valueOffset = value - prevValue;
				prevValue = value;
				
				tr.position += offset * valueOffset;
			});
		}
	}
}