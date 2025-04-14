using Dummes2DGame.Objects;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace Dummes2DGame;
public static class MainView {
    private static int _level = 1;
    private static int _diamonds = 0;
    private static int _highScore = 0;

    public static int Level {
        get => _level;
        set {
            if (_level != value) {
                _level = value;
                NotifyStaticPropertyChanged();
            }
        }
    }

    public static int Diamonds {
        get => _diamonds;
        set {
            if (_diamonds != value) {
                _diamonds = value;
                NotifyStaticPropertyChanged();
                if (HighScore < _diamonds)
                    HighScore = _diamonds;
            }
        }
    }

    public static int HighScore {
        get => _highScore;
        set {
            if (_highScore != value) {
                _highScore = value;
                NotifyStaticPropertyChanged();
            }
        }
    }

    public static MediaPlayer MP { get; } = new MediaPlayer() { Volume = 0.3d };

    public static ObservableCollection<ObservableCollection<ObjectBase>> Map { get; private set; } = new();

    public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;

    private static void NotifyStaticPropertyChanged([CallerMemberName] string propertyName = "") {
        StaticPropertyChanged.Invoke(null, new PropertyChangedEventArgs(propertyName));
    }

    public static void CreateMap(int mapWidth, int mapHight) {
        ObjectBase[,] mapArray = new ObjectBase[mapHight, mapWidth];

        for (int y = 0; y < mapHight; y++) {
            if (y == 0 || y == mapHight - 1) {
                for (int x = 0; x < mapWidth; x++) {
                    mapArray[y, x] = new Block(x, y);
                }
            } else {
                mapArray[y, 0] = new Block(0, y);
                mapArray[y, mapWidth - 1] = new Block(y, mapWidth - 1);
            }
        }

        for (int y = 0; y < mapHight; y++) {
            ObservableCollection<ObjectBase> rows = new();

            for (int x = 0; x < mapWidth; x++) {
                if (mapArray[y, x] != null) {
                    rows.Add(mapArray[y, x]);
                } else {
                    rows.Add(new ObjectBase(x, y));
                }
            }

            Map.Add(rows);
        }

        Map[3][3] = new Block(3, 3);
        Map[7][7] = new Block(7, 7);
        Map[3][7] = new Block(7, 3);
        Map[7][3] = new Block(3, 7);
    }
}
