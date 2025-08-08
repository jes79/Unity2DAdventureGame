using System.Collections;
using UnityEngine;

public enum FollowPath_State { Idle = 0, Move }

public class FollowPath : MonoBehaviour
{
	[SerializeField]
	private	Transform	target;				// 실제 이동하는 대상의 Transform
	[SerializeField]
	private	Transform[]	wayPoints;			// 이동 가능한 지점
	[SerializeField]
	private	float		waitTime;			// wayPoint 도착 후 대기시간
	[SerializeField]
	private	float		timeOffset;			// 이동시간 = 거리 * timeOffset

	private	int			wayPointCount;		// 이동 가능한 wayPoint 개수
	private	int			currentIndex = 0;   // 현재 wayPoint 인덱스

	private	int			direction;
	public	int			Direction => direction;

	public	FollowPath_State State { private set; get; } = FollowPath_State.Idle;

	private void Awake()
	{
		target.position = wayPoints[currentIndex].position;
		wayPointCount	= wayPoints.Length;

		currentIndex ++;

		StartCoroutine(nameof(Process));
	}

	private IEnumerator Process()
	{
		while ( true )
		{
			// wayPoints[currentIndex].position 위치까지 이동
			yield return StartCoroutine(MoveAToB(target.position, wayPoints[currentIndex].position));

			// waitTime 시간 동안 대기
			yield return new WaitForSeconds(waitTime);

			// 다음 이동 지점(wayPoint) 설정
			if ( currentIndex < wayPointCount - 1 )	currentIndex ++;
			else									currentIndex = 0;
		}
	}

	private IEnumerator MoveAToB(Vector3 start, Vector3 end)
	{
		float percent	= 0;
		float moveTime	= Vector3.Distance(start, end) * timeOffset;

		SetDirection(start.x, end.x);
		State = FollowPath_State.Move;

		while ( percent < 1 )
		{
			percent += Time.deltaTime / moveTime;
			target.position = Vector3.Lerp(start, end, percent);

			yield return null;
		}

		State = FollowPath_State.Idle;
	}

	private void SetDirection(float start, float end)
	{
		if ( end - start != 0 )	direction = (int)Mathf.Sign(end - start);
		else					direction = 0;
	}

	public void Stop()
	{
		StopAllCoroutines();
	}
}

