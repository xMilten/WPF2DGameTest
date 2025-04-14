using Dummes2DGame.Objects;
using System;
using System.Collections.Generic;

namespace Dummes2DGame;
public static class Map {
    private static List<ObjectBase> _freeSpaces;
    public static Player Player { get; set; }
    public static List<Hostile> Hostiles { get; set; } = new List<Hostile>();
    public static List<Diamond> Diamonds { get; set; } = new List<Diamond>();

    public static void InitMap() {
        MainView.CreateMap(11, 11);

        Player = new Player(5, 5);
        MainView.Map[5][5] = Player;

        if (_freeSpaces == null) {
            _freeSpaces = new List<ObjectBase>();

            for (int y = 1; y < MainView.Map.Count - 1; y++) {

                for (int x = 1; x < MainView.Map[0].Count - 1; x++) {

                    if (y < 3 || y > 7)
                        _freeSpaces.Add(MainView.Map[y][x]);
                    else if (x < 3 || x > 7)
                        _freeSpaces.Add(MainView.Map[y][x]);
                }
            }
        }

        List<ObjectBase> freeSpaces = new(_freeSpaces);

        if (MainView.Level < 10) {
            Random random = new();

            for (int i = 0; i < ((double)MainView.Level / 2) + 0.5; i++) {
                ObjectBase objectBase;

                if (freeSpaces.Count == 0)
                    break;
                objectBase = freeSpaces[random.Next(freeSpaces.Count)];

                Diamond diamond = new(objectBase.PosX, objectBase.PosY);
                Diamonds.Add(diamond);
                MainView.Map[objectBase.PosY][objectBase.PosX] = diamond;
            }

            for (int i = 0; i < (double)MainView.Level / 2; i++) {
                ObjectBase objectBase;

                if (freeSpaces.Count == 0)
                    break;
                objectBase = freeSpaces[random.Next(freeSpaces.Count)];

                Hostile hostile = new(objectBase.PosX, objectBase.PosY);
                Hostiles.Add(hostile);
                MainView.Map[objectBase.PosY][objectBase.PosX] = hostile;
            }
        }
    }

    public static void ResetMap() {
        Player = null;
        foreach (Hostile hostile in Hostiles) {
            ObjectBase.Timer.Tick -= hostile.Move;
        }
        Hostiles.Clear();
        Diamonds.Clear();
        MainView.Map.Clear();
    }

    public static void DiamondCollected() {
        MainView.Diamonds++;
        if (Diamonds.Count == 0) {
            ResetMap();
            MainView.Level++;
            InitMap();
        }
    }

    public static void Died() {
        ResetMap();
        MainView.Level = 1;
        MainView.Diamonds = 0;
        InitMap();
    }
}
