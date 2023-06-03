
namespace lib
{

    public class InkjetPrinter : Printer
    {
        public List<Сartridge> cartridges = new List<Сartridge>();
        public String TypeColorPrintig { get; set; }



        public override string increaseMaxFormat(int delta)
        {
            maxFormat += delta;
            return $"Max format have been inrcreased to {maxFormat}";
        }

        public override string reduceMaxFormat(int delta)
        {
            maxFormat -= delta;
            return $"Max format have been reduced to {maxFormat}";
        }

        public String fillPrinter()
        {
            cartridges.Add(Сartridge.BLUE);
            cartridges.Add(Сartridge.YELLOW);
            cartridges.Add(Сartridge.PURPLE);
            cartridges.Add(Сartridge.BLACK);
            return $"Сartridges were refilled";
        }

        public String fillСartridge(Сartridge cartridge)
        {
            cartridges.Add(cartridge);
            return $"Сartridge was filled";
        }
    }
}