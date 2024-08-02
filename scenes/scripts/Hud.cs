using Godot;
using System;

public partial class Hud : CanvasLayer {
	[Signal]
	public delegate void StartGameEventHandler();

	public override void _Ready() {
	}

	public override void _Process(double delta) {
	}

	public void UpdateScore(int score) {
		GetNode<Label>("ScoreLabel").Text = score.ToString();
	}

	public void ShowMessage(string text) {
		var message = GetNode<Label>("Message");
		message.Text = text;
		message.Show();

		GetNode<Timer>("MessageTimer").Start();
	}

	async public void ShowGameOver() {
		ShowMessage("Game Over");

		var messageTimer = GetNode<Timer>("MessageTimer");
		await ToSignal(messageTimer, Timer.SignalName.Timeout);

		var message = GetNode<Label>("Message");
		message.Text = "Dodge the creeps!";
		message.Show();

		await ToSignal(GetTree().CreateTimer(1.0), SceneTreeTimer.SignalName.Timeout);
		GetNode<Button>("StartButton").Show();
	}

	private void OnStartButtonPressed() {
		GetNode<Button>("StartButton").Hide();
		EmitSignal(SignalName.StartGame);
	}

	private void OnMessageTimerTimeout() {
		GetNode<Label>("Message").Hide();
	}
}
