extends Control

export (int) var sec = 5;

func _physics_process(delta):
	$sec.set_text(str(sec))
	if sec == 0:
		$sec.set_text("Go!")
	if sec<0:
		get_tree().change_scene("res://Scenes/World.tscn")

func _on_Timer_timeout():
	sec -= 1
	pass # Replace with function body.
