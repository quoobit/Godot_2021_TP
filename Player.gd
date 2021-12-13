extends RigidBody

var pos = get_position_in_parent()
func _process(delta):
	if translation.y < -3 :
		get_tree().change_scene("res://Scenes/GameOver.tscn")
