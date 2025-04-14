using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Dummes2DGame.Objects;
public class Block : ObjectBase {
    public Block(int posX, int posY) {
        Visibility = Visibility.Visible;
        ObjectImage = new BitmapImage(new Uri("pack://application:,,,/Resources/block.png"));
        Id = Ids.Block;

        PosX = posX;
        PosY = posY;
    }
}
