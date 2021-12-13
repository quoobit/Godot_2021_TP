extends MeshInstance

export var speed = 3
var floorTexture
var mat

# Called when the node enters the scene tree for the first time.
func _ready():
	floorTexture = get_node("/root/World/Floor/FloorTexture")
	mat = floorTexture.get_surface_material(0)
	pass # Replace with function body.

func _process(delta):
	if mat != null:
		var currOffset = mat.get_uv1_offset()
		mat.set_uv1_offset(currOffset - Vector3(0.001 * speed, 0, 0))
		
		#floorMesh.MeshInstance.Material.Uv1.Offset.x =- 0.01
	pass


func _on_FloorSpeedTimer_timeout():
	speed += 0.5
	pass # Replace with function body.
