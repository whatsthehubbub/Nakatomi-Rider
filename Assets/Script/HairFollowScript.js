#pragma strict

var followTarget : Transform;

var xOffset = 1;
var yOffset = 1;

var followSpeed = 2;

function Start () {
}

static function Berp(start : float, end : float, value : float) : float
{
    value = Mathf.Clamp01(value);
    value = (Mathf.Sin(value * Mathf.PI * (0.2 + 2.5 * value * value * value)) * Mathf.Pow(1 - value, 2.2) + value) * (1 + (1.2 * (1 - value)));
    return start + (end - start) * value;
}

function Update () {
	transform.position.x = Berp(transform.position.x, followTarget.position.x+xOffset, Time.deltaTime * followSpeed);
	transform.position.y = Berp(transform.position.y, followTarget.position.y+yOffset, Time.deltaTime * followSpeed);

	// transform.RotateAround(followTarget.position, Vector3.back, followSpeed);
}