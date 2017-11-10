using UnityEngine;

public class ColorIndicator : MonoBehaviour {

	HSBColor color;
	public ColorManager cm;
	void Start() {
		color = HSBColor.FromColor(GetComponent<Renderer>().sharedMaterial.GetColor("_Color"));
		transform.parent.BroadcastMessage("SetColor", color);
	}

	void ApplyColor ()
	{
		GetComponent<Renderer>().sharedMaterial.SetColor ("_Color", color.ToColor());
		transform.parent.BroadcastMessage("OnColorChange", color, SendMessageOptions.DontRequireReceiver);
		cm.onColorChange(color);
	}

	void SetHue(float hue)
	{
		color.h = hue;
		ApplyColor();

    }	

	void SetSaturationBrightness(Vector2 sb) {
		color.s = sb.x;
		color.b = sb.y;
		ApplyColor();
	}
}
