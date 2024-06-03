#nullable disable
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Puzzler_Game;
public class BoardViewModel : ViewModel
{
    #region Constructor
    public BoardViewModel(GameLevel level)
    {
        Board = new Board(level);
        Level = level;
        Load(Board);
    }
    public void Load(Board board)
    {
        Board = board;
        OnPropertyChanged(nameof(Board));
    }
    private GameLevel Level { get; init; }
    public GridItemsLayout TilesLayout => new GridItemsLayout((int)Level, ItemsLayoutOrientation.Vertical);

    #endregion

    #region Properties
    private Board Board { get; set; }
    public ObservableCollection<Puzzle> Tiles => new(Board.GameState);
    #endregion

    #region Load Images (Not Implemented Now)
    private IDictionary<int,ImageSource> ImagesDictionary { get; set; }
    public ImageSource ThisImage => ImageSource.FromFile("dotnet_bot.png");
    async Task<IDictionary<int, ImageSource>> GetDividedImages() 
    {
        return await new ImageManipulator().DivideImage(ThisImage, 3);
    }
    async void DivideImage()
    {
        var dic = await GetDividedImages();
        ImagesDictionary = dic; 
    }
    void LoadImages()
    {
        for (int i = 0; i < Board.GameState.Count; i++)
        {
            var tile = Board.GameState[i];
            //Board.GameState[i].Image = ImagesDictionary[tile.Value];
        }
        OnPropertyChanged(nameof(Board));
        OnPropertyChanged(nameof(Tiles));
    }
    #endregion

    #region Swap Tiles
    private ICommand swapTiles;
    public ICommand SwipeTiles
    {
        get
        {
            swapTiles ??= new Command(DoSwapTile);
            return swapTiles;
        }
    }
    void DoSwapTile()
    {
        if (selectedTile == null || selectedTile.Index == Board.EmptyFieldIndex || Board.IsSolved) return;
        var options = Board.GetPossibleMoves();
        if (options.Contains(SelectedTile.Index))
        {
            Board.SwapPuzzle(Board.EmptyFieldIndex, SelectedTile.Index);
        }
        SelectedTile = null;
        OnPropertyChanged(nameof(Board));
        OnPropertyChanged(nameof(Tiles));
    }
    #endregion

    #region Selected Tile
    private Puzzle selectedTile { get; set; }
    public Puzzle SelectedTile
    {
        get => selectedTile;
        set
        {
            if (selectedTile != value)
            {
                selectedTile = value;
                OnPropertyChanged(nameof(SelectedTile));
            }
        }
    }
    #endregion
}
