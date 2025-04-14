using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Dummes2DGame.Objects;
public class Hostile : ObjectBase {
    public Hostile(int posX, int posY) {
        Visibility = Visibility.Visible;
        ObjectImage = new BitmapImage(new Uri("pack://application:,,,/Resources/hostile.png"));
        Id = Ids.Hostile;

        PosX = posX;
        PosY = posY;

        Timer.Tick += Move;
        if (!Timer.IsEnabled) {
            Timer.Start();
        }
    }

    public override void Move(object sender, EventArgs e) {
        List<Action> possibleMoves = new();

        if (MainView.Map[PosY][PosX - 1].Id != Ids.Block && MainView.Map[PosY][PosX - 1].Id != Ids.Diamond && MainView.Map[PosY][PosX - 1].Id != Ids.Hostile)
            possibleMoves.Add(MoveLeft);

        if (MainView.Map[PosY][PosX + 1].Id != Ids.Block && MainView.Map[PosY][PosX + 1].Id != Ids.Diamond && MainView.Map[PosY][PosX + 1].Id != Ids.Hostile)
            possibleMoves.Add(MoveRight);

        if (MainView.Map[PosY - 1][PosX].Id != Ids.Block && MainView.Map[PosY - 1][PosX].Id != Ids.Diamond && MainView.Map[PosY - 1][PosX].Id != Ids.Hostile)
            possibleMoves.Add(MoveUp);

        if (MainView.Map[PosY + 1][PosX].Id != Ids.Block && MainView.Map[PosY + 1][PosX].Id != Ids.Diamond && MainView.Map[PosY + 1][PosX].Id != Ids.Hostile)
            possibleMoves.Add(MoveDown);

        if (possibleMoves.Count == 0) {
            Timer.Stop();
            return;
        }

        Random random = new();
        possibleMoves[random.Next(possibleMoves.Count)].Invoke();

        if (MainView.Map[PosY][PosX].Id == Ids.Player)
            Timer.Stop();
    }
}
