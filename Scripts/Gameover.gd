extends Node2D


# Declare member variables here. Examples:
# var a = 2
# var b = "text"
var scoreLabel

# Called when the node enters the scene tree for the first time.
func _ready():
	var score = Scores.score
	scoreLabel=get_node("Base/Score")
	scoreLabel.text="Score : "+String(score)
	print(Scores.score)


# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass
