using Avalonia.Input;
using Digger.Architecture;

namespace Digger;

public static class Game
{
	private const string mapWithPlayerTerrain = @"
TTTST
TSP T
T T S
TT TT";

	private const string mapWithPlayerTerrainSackGold = @"
PTTGTT TS
TST  TSTT
TTTTTTSTT
T TSTS TT
T TTTG ST
TSTSTT TT";

	private const string mapWithPlayerTerrainSackGoldMonster = @"
PTTGTT TST
TST  TSTTM
TTT TTSTTT
T TSTS TTT
T TTTG STS
T T T   TS
TSTSTT TTT
SMTTST  TG
 TGST  TTT
 T  TMTTTT";

    private const string myMap1 = @"
PT G  MTST
TST  TSTTM
TTT T STTT
TMTMTS TTT
T TTT  STS
T TGT M TS
TSTSTT TTT
SMTTST  TG
 TGST  TTT
 T  TMTTTT";

    public static ICreature[,] Map;
	public static int Scores;
	public static bool IsOver;

	public static Key KeyPressed;
	public static int MapWidth => Map.GetLength(0);
	public static int MapHeight => Map.GetLength(1);

	public static void CreateMap()
	{
		Map = CreatureMapCreator.CreateMap(myMap1);
	}
}