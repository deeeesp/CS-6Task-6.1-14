
namespace lib
{

    public interface PrintingDevice
    {
        public int printingSpeed { get; set; }

        public String print();

        public String increaseSpeed(int delta);
    }
}