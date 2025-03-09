extends Control

var line_edit : LineEdit
var puntuaciones : Control
var mios_label : Label
var vbox_container_mis : VBoxContainer
var _http_request : HTTPRequest
var api_url : String = "https://psp-api-259q.onrender.com/api/Scores"

func _ready():
	pass

	

func _on_button_pressed():
	line_edit = $ColorRect/LineEdit
	vbox_container_mis = $"../VBoxContainerMis"
	mios_label = $"../Mios"
	_http_request = $HTTPRequest
	puntuaciones = $".."
	
	var name = line_edit.text

	if name != "":
		var points = Global.score  # Asegúrate de que 'Global' esté correctamente definido como un Singleton

		var json_data = {
			"playerName": name,
			"points": points
		}

		# Serializar el objeto a JSON
		var json_string =  JSON.stringify(json_data)
		printerr(json_string)
		_http_request.request_completed.connect(self._on_request_completedPost)
		# Hacer la solicitud HTTP POST
		_http_request.request(api_url, ["Content-Type: application/json"], HTTPClient.METHOD_POST, json_string)
	else:
		print("Por favor ingresa un nombre en el LineEdit.")

func _on_request_completedPost(result, response_code, headers, body):
	if response_code == 201:
		print("Datos enviados correctamente.")
		puntuaciones.GetAll()
		mios_label.visible = true
		vbox_container_mis.GetForName(line_edit.text)
		vbox_container_mis.visible = true
		self.visible = false
	else:
		printerr("Error al enviar los datos add: %d" % response_code)
