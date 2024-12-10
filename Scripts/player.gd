extends CharacterBody2D

@export var speed : float = 200.0

@onready var sprite : Sprite2D = $Sprite2D
@onready var animation_tree : AnimationTree = $AnimationTree
@onready var state_machine : CharacterStateMachine = $CharacterStateMachine
@onready var sword : Area2D = $Sword
@onready var damageable : Node = $Damageable


# Get the gravity from the project settings to be synced with RigidBody nodes.
var gravity = ProjectSettings.get_setting("physics/2d/default_gravity")
var direction : Vector2 = Vector2.ZERO

func _ready():
	animation_tree.active = true
	sword.monitoring = false
	

func _physics_process(delta):
	# Add the gravity.
	if not is_on_floor():
		velocity.y += gravity * delta

	if damageable.dead:
		direction = Vector2(0,0).normalized()
	
	if damageable.hit :
		animation_tree.anim_player = "hit"
		
		if sprite.flip_h:
			velocity += Vector2(50,-60)
			position += Vector2(30,0)
		else:
			velocity += Vector2(50,-60)
			position += Vector2(-30,0)
		
		damageable.hit = false
		
	# Get the input direction and handle the movement/deceleration.
	# As good practice, you should replace UI actions with custom gameplay actions.
	direction = Input.get_vector("left", "right", "up", "down")
	
	# Control whether to move or not to move
	if direction.x != 0 && state_machine.check_if_can_move() && damageable.hit == false:
		velocity.x = direction.x * speed
	else:
		velocity.x = move_toward(velocity.x, 0, speed)

	move_and_slide()
	update_animation_parameters()
	update_facing_direction()
	
func update_animation_parameters():
	animation_tree.set("parameters/Move/blend_position", direction.x)

func update_facing_direction():
	if direction.x > 0:
		sprite.flip_h = false
		sword.position.x = 0 
	elif direction.x < 0:
		sprite.flip_h = true
		sword.position.x =-52.4
