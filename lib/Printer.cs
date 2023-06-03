
namespace lib
{
    public abstract class Printer : PrintingDevice
    {
        public int dpi = 600;
        public int Cost { get; set; }
        public int maxFormat { get; set; }
        public int printingSpeed { get; set; }


        public string print() => $"Printing...";

        public string increaseSpeed(int delta)
        {
            printingSpeed += delta;
            return $"Printing speed have been inrcreased to {printingSpeed}";
        }

        public string buyPrinter() => $"Buying a printer...";

        public abstract string increaseMaxFormat(int delta);

        public abstract string reduceMaxFormat(int delta);

    }
}