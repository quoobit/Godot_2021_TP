extends Node


# Declare member variables here. Examples:
# var a = 2
# var b = "text"
var scoreLabel

# Called when the node enters the scene tree for the first time.
func _ready():
	Scores.reset_score()
	scoreLabel=get_node("/root/World/Control/ScoreLabel")
		
func score_increment():
		Scores.add_score()
		scoreLabel.text=String(Scores.score)
