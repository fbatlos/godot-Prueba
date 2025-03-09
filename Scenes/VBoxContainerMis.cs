using Godot;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using juego.Models;  

public partial class VBoxContainerMis : VBoxContainer
{
	private HttpRequest _httpRequest;
	private string _apiUrl = "https://psp-api-259q.onrender.com/api/Scores/player/";
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void GetForName(string name)
	{
		GD.Print("Estoyyyyy en get by NAMEEEEEEEEEEEE");
		
		// Verificar que el nodo HTTPRequest está presente
		_httpRequest = GetNode<HttpRequest>("HTTPRequest");
		if (_httpRequest == null)
		{
			GD.PrintErr("No se pudo encontrar el nodo HTTPRequest.");
			return;
		}
		
		_httpRequest.RequestCompleted += OnRequestCompletedName;
		GD.Print($"Realizando petición a la API...  {_apiUrl+ name}");
		
		_httpRequest.Request(_apiUrl+ name);
	}
	
	private void OnRequestCompletedName(long result, long responseCode, string[] headers, byte[] body)
	{
		GD.Print("Respuesta recibida de la API...");
		string jsonString = System.Text.Encoding.UTF8.GetString(body);

		if (responseCode != 200)
		{
			GD.PrintErr("Error en la respuesta de la API: " + responseCode);
			return;
		}

		try
		{
			List<ScoreData> scores = JsonSerializer.Deserialize<List<ScoreData>>(jsonString);

			if (scores != null)
			{
				var godotScores = new Godot.Collections.Array();
				foreach (var score in scores)
				{
					var dict = new Godot.Collections.Dictionary
					{
						{ "id", score.Id },
						{ "name", score.Name },
						{ "points", score.Points }
					};
					godotScores.Add(dict);
				}

				GD.PrintErr(godotScores);
				_update_scores_display(godotScores);
				
			}
		}
		catch (Exception e)
		{
			GD.PrintErr("Error al parsear JSON: " + e.Message);
		}
	}
	
	private void _update_scores_display(Godot.Collections.Array scores)
	{
		int totalScores = scores.Count;
		
		if (totalScores == 0)
		{
			GD.Print("No hay puntuaciones disponibles");
			return;
		}
		
		foreach (Node child in this.GetChildren())
		{
			child.QueueFree(); 
		}
		
		if(totalScores >= 5)
		{
			totalScores = 5;
		}
		
		for (int i = 0; i < totalScores; i++)
		{
			var score = (Godot.Collections.Dictionary)scores[i];

			GD.Print("Puntaje recibido:", score);

			var label = new Label
			{
				Text = $"{i+1} - {score["name"]}: {score["points"]}"
			};
			label.AddThemeColorOverride("font_color", new Color(0, 128, 0));  
			this.AddChild(label);
		}

		GD.Print("Puntuaciones actualizadas correctamente");
	}
}
