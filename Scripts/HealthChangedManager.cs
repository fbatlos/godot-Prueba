using Godot;
using System;

public partial class HealthChangedManager : Control
{
    [Export]
<<<<<<< HEAD
    public PackedScene HealthChangedLabelScene;
=======
    public PackedScene HealthChangedLabelScene = (PackedScene)ResourceLoader.Load("res://Scenes/health_changed_label.tscn");
>>>>>>> 8016359 (El label esta vacio)

    private Label healthLabel;

    public override void _Ready()
    {
        GD.Print("HealthChangedManager _Ready() called.");

        if (HealthChangedLabelScene == null)
        {
            GD.Print("Error: HealthChangedLabelScene no está asignado.");
            return;
        }

        var labelInstance = (Control)HealthChangedLabelScene.Instantiate();
        AddChild(labelInstance);
        GD.Print("HealthChangedLabelScene instancia añadida como hijo.");

        // Obtener el Label directamente si está en el nodo raíz
        healthLabel = labelInstance.GetNode<Label>("HitLabel");
        
        if (healthLabel == null)
        {
            // Si no encuentra el Label directamente, intenta obtenerlo desde el nodo Control
            GD.Print("No se encontró HitLabel directamente, buscando dentro del nodo Control.");
            var controlNode = labelInstance.GetNode<Control>("Control");
            if (controlNode == null)
            {
                GD.Print("Error: El nodo Control no fue encontrado.");
                return;
            }
            else
            {
                GD.Print("Nodo Control encontrado.");
                healthLabel = controlNode.GetNode<Label>("HitLabel");
            }
        }

        // Comprobar si healthLabel es null
        if (healthLabel == null)
        {
            GD.Print("Error: HitLabel no encontrado.");
        }
        else
        {
            GD.Print("HitLabel encontrado.");
            healthLabel.Text = "Label inicializado";
            
        }
    }

    public void OnHealthChanged(int newHealth)
    {
        GD.Print("OnHealthChanged called with newHealth: ", newHealth);
        if (healthLabel != null)
        {
            healthLabel.Text = newHealth.ToString();
        }
    }
}


