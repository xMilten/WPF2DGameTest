using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Dummes2DGame.Objects;
public class Player : ObjectBase {
    public Player(int posX, int posY) {
        Visibility = Visibility.Visible;
        ObjectImage = new BitmapImage(new Uri("pack://application:,,,/Resources/player.png"));
        Id = Ids.Player;

        PosX = posX;
        PosY = posY;
    }

    public void Move(KeyEventArgs e) {
        switch (e.Key) {
            case Key.A:
                MoveLeft();
                break;
            case Key.D:
                MoveRight();
                break;
            case Key.W:
                MoveUp();
                break;
            case Key.S:
                MoveDown();
                break;
        }
    }
}