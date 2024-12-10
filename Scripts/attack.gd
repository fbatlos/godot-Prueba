extends State

@export var return_state : State
@export var sound:AudioStreamPlayer2D

var return_animation_node:String = "Move"

@onready var timer:Timer = $Timer

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass

func state_input(event:InputEvent):
	if(event.is_action_pressed("attack")):
		timer.start()
		
		
		

func _on_animation_tree_animation_finished(anim_name: StringName) -> void:
	if(anim_name == "attack1"):
		if (timer.is_stopped()):
			print("Estoy sonando")
			sound.play()
			next_state = return_state
			playback.travel(return_animation_node)
		else:
			print("Estoy sonando 3")
			sound.play()
			playback.travel("attack2")
	if( anim_name == "attack2"):
		print("Estoy sonando 2")
		sound.play()
		next_state = return_state
		playback.travel(return_animation_node)
