public interface IHavePiksels
{
    int MinPixelsDrop { get; }
    int MaxPixelsDrop { get; }

    int DropPixels();
}
