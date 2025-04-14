using System;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Dummes2DGame.Objects;

public enum Ids {
    None,
    Block,
    Player,
    Hostile,
    Diamond
}

public class ObjectBase {
    public static DispatcherTimer Timer { get; private set; } = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };
    public Visibility Visibility { get; set; }
    public BitmapImage ObjectImage { get; set; }
    public Ids Id { get; set; }
    public int PosX { get; set; }
    public int PosY { get; set; }

    public ObjectBase() {
        Visibility = Visibility.Hidden;
        ObjectImage = null;
        Id = Ids.None;
        PosX = 0;
        PosY = 0;
    }

    public ObjectBase(int posX, int posY) {
        Visibility = Visibility.Hidden;
        ObjectImage = null;
        Id = Ids.None;
        PosX = posX;
        PosY = posY;
    }

    public virtual void Move(object sender, EventArgs e) {

    }

    public void MoveLeft() {
        if (PosX > 0 && MainView.Map[PosY][PosX - 1].Id != Ids.Block) {
            CheckCollusion(PosX - 1, PosY);
            PosX--;
            CheckHostileCloseToPlayer();
        }
    }

    public void MoveRight() {
        if (PosX < MainView.Map[0].Count - 1 && MainView.Map[PosY][PosX + 1].Id != Ids.Block) {
            CheckCollusion(PosX + 1, PosY);
            PosX++;
            CheckHostileCloseToPlayer();
        }
    }

    public void MoveUp() {
        if (PosY > 0 && MainView.Map[PosY - 1][PosX].Id != Ids.Block) {
            CheckCollusion(PosX, PosY - 1);
            PosY--;
            CheckHostileCloseToPlayer();
        }
    }

    public void MoveDown() {
        if (PosY < MainView.Map.Count - 1 && MainView.Map[PosY + 1][PosX].Id != Ids.Block) {
            CheckCollusion(PosX, PosY + 1);
            PosY++;
            CheckHostileCloseToPlayer();
        }
    }

    private void CheckCollusion(int x, int y) {
        IdsCollide(MainView.Map[y][x]);
        MainView.Map[y][x] = MainView.Map[PosY][PosX];
        MainView.Map[PosY][PosX] = new ObjectBase(PosX, PosY);
    }

    private void IdsCollide(ObjectBase objectBase) {
        if (Id == Ids.Player) {
            if (objectBase.Id == Ids.Diamond) {
                PlayerCollidesDiamond(objectBase);
            } else if (objectBase.Id == Ids.Hostile) {
                PlayerHostileCollides(objectBase);
            }
        } else if (Id == Ids.Hostile) {
            if (objectBase.Id == Ids.Player) {
                PlayerHostileCollides(objectBase);
            }
        }
    }

    private void PlayerCollidesDiamond(ObjectBase objectBase) {
        Map.Diamonds.Remove((Diamond)objectBase);
        MainView.MP.Open(new Uri("pack://siteoforigin:,,,/Resources/Successful_hit.wav"));
        MainView.MP.Play();
        Map.DiamondCollected();
    }

    private void PlayerHostileCollides(ObjectBase objectBase) {
        switch (Id) {
            case Ids.Player:
                Map.Hostiles.Remove((Hostile)objectBase);
                break;
            case Ids.Hostile:
                Map.Player = null;
                break;
        }
        MainView.MP.Open(new Uri("pack://siteoforigin:,,,/Resources/Explosion4.wav"));
        MainView.MP.Play();
        Timer.Tick -= Move;
        Map.Died();
        MessageBox.Show("Game over");
    }

    private void CheckHostileCloseToPlayer() {
        if (Id == Ids.Hostile) {
            if (MainView.Map[PosY][PosX - 1].Id == Ids.Player) {
                PlayFuseSound();
            } else if (MainView.Map[PosY][PosX + 1].Id == Ids.Player) {
                PlayFuseSound();
            } else if (MainView.Map[PosY - 1][PosX].Id == Ids.Player) {
                PlayFuseSound();
            } else if (MainView.Map[PosY + 1][PosX].Id == Ids.Player) {
                PlayFuseSound();
            }
        } else if (Id == Ids.Player) {
            if (MainView.Map[PosY][PosX - 1].Id == Ids.Hostile) {
                PlayFuseSound();
            } else if (MainView.Map[PosY][PosX + 1].Id == Ids.Hostile) {
                PlayFuseSound();
            } else if (MainView.Map[PosY - 1][PosX].Id == Ids.Hostile) {
                PlayFuseSound();
            } else if (MainView.Map[PosY + 1][PosX].Id == Ids.Hostile) {
                PlayFuseSound();
            }
        }
    }

    private void PlayFuseSound() {
        MainView.MP.Open(new Uri("pack://siteoforigin:,,,/Resources/Fuse.wav"));
        MainView.MP.Play();
    }
}
