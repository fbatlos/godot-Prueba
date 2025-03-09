using Godot;
using System;
using System.Text.Json;
using System.Text.Json.Serialization; 
using System.Collections.Generic; 
using juego.Models; 

public partial class ControlApi : Control
{
	private HttpRequest _httpRequest;
	private string _apiUrl = "https://psp-api-259q.onrender.com/api/Scores";

	public override void _Ready()
	{
		// Verificar que el nodo HTTPRequest está presente
		_httpRequest = GetNode<HttpRequest>("HTTPRequest");
		if (_httpRequest == null)
		{
			GD.PrintErr("No se pudo encontrar el nodo HTTPRequest.");
			return;
		}

		_httpRequest.RequestCompleted += OnRequestCompleted;

		// Intentar hacer la solicitud
		GetAllScores();
	}


	public void GetAllScores()
	{
		GD.Print("✅ Realizando petición a la API...");
		_httpRequest.Request(_apiUrl);
	}

	private void OnRequestCompleted(long result, long responseCode, string[] headers, byte[] body)
	{
		GD.Print("✅ Respuesta recibida de la API...");
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
		if (scores.Count == 0)
		{
			GD.Print("⚠ No hay puntuaciones disponibles");
			return;
		}

		var allContainer = GetNode<HBoxContainer>("HBoxContainer/VBoxContainerAll");
		var youContainer = GetNode<HBoxContainer>("HBoxContainer/VBoxContainerYou");

		foreach (var child in allContainer.GetChildren())
		{
			(child as Node).QueueFree();
		}

		foreach (var child in youContainer.GetChildren())
		{
			(child as Node).QueueFree();
		}

		int mid = scores.Count / 2;
		GD.Print("✅ Puntajes divididos en dos columnas.");

		for (int i = 0; i < scores.Count; i++)
		{
			var score = (Godot.Collections.Dictionary)scores[i];

			GD.Print("Puntaje recibido:", score);

			var label = new Label
			{
				Text = $"{score["name"]}: {score["points"]}"
			};
			label.AddThemeColorOverride("font_color", new Color(1, 1, 1));  

			if (i < mid)
			{
				allContainer.AddChild(label);
			}
			else
			{
				youContainer.AddChild(label);
			}
		}

		GD.Print("✅ Puntuaciones actualizadas correctamente");
	}
}
