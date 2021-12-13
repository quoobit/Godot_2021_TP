extends RigidBody

var pos = get_position_in_parent()
var player
func _ready():
	player = get_node("/root/World/Player")
func _process(delta):
	if translation.y < -3 :
		get_tree().change_scene("res://Scenes/GameOver.tscn")
