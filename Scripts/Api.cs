using Godot;
using System;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using juego.Models; 

public partial class Api : Node
{	
	private HttpRequest _httpRequest;
	private string _apiUrl = "https://psp-api-259q.onrender.com/api/scores";

	
	[Signal]
	public delegate void ScoresUpdatedEventHandler();

	public override void _Ready()
	{
		_httpRequest = GetNode<HttpRequest>("HTTPRequest");
		_httpRequest.RequestCompleted += OnRequestCompleted;

		GetAllScores();
	}

	public void GetAllScores()
	{
		_httpRequest.Request(_apiUrl);
	}

	private void OnRequestCompleted(long result, long responseCode, string[] headers, byte[] body)
	{
		string jsonString = Encoding.UTF8.GetString(body);

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
						{ "id", score.Id },       // 📌 Ahora también enviamos el ID
						{ "name", score.Name },
						{ "points", score.Points }
					};
					godotScores.Add(dict);
				}

				EmitSignal(nameof(ScoresUpdatedEventHandler), godotScores);
				GD.PrintErr(godotScores);
			}
		}
		catch (Exception e)
		{
			GD.PrintErr("Error al parsear JSON: " + e.Message);
		}
	}
}
