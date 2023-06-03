
namespace lib
{

    public class MatrixPrinter : Printer
    {
        public Boolean IsColorPriner { get; set; }
        public Boolean cartrigeFilled = false;



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

        public String refillPrinter()
        {
            cartrigeFilled = true;
            return $"Сartridges were refilled";
        }

        public String removeСartridge()
        {
            cartrigeFilled = false;
            return $"Сartridges were removed";
        }
    }
}