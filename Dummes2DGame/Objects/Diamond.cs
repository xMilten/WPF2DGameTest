using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Dummes2DGame.Objects;
public class Diamond : ObjectBase {
    public Diamond(int posX, int posY) {
        Visibility = Visibility.Visible;
        ObjectImage = new BitmapImage(new Uri("pack://application:,,,/Resources/diamond.png"));
        Id = Ids.Diamond;

        PosX = posX;
        PosY = posY;
    }
}
