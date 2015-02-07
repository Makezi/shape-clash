using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class Shape : MonoBehaviour {

	public float rotateSpeed = 50f;				// Speed in which to rotate object

	private SpriteRenderer spriteRenderer;		// Reference to object's SpriteRenderer
	private PolygonCollider2D myCollider;			// Reference to object's collider
	private Transform tr;						// Cached transform

	protected void Awake () {
		spriteRenderer = GetComponent<SpriteRenderer>();
		tr = transform;
	}

	void LateUpdate(){
		Rotate();
	}

	/* Rotate object at set speed */
	private void Rotate(){
		tr.Rotate(Vector3.forward * -rotateSpeed * Time.deltaTime);
	}

	/* Create polygon collider depending on current shape sprite */
	private void CreateCollider(){
		myCollider = gameObject.AddComponent("PolygonCollider2D") as PolygonCollider2D;
	}

	/* Destroys current collider before setting shape sprite and creating a new collider */
	public void SetShape(Sprite sprite){
		if(myCollider != null){ Destroy(myCollider); }
		spriteRenderer.sprite = sprite;
		CreateCollider();
	}

	public Sprite GetShape(){
		return spriteRenderer.sprite;
	}

	public SpriteRenderer SpriteRenderer {
		get { return spriteRenderer; }
	}

}
