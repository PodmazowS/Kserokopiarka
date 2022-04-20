using System;
using System.Collections.Generic;
using System.Text;

namespace Zadanie3
{
    public class Copier : BaseDevice, ICopier
    {
        public int ScanCounter { get; private set; } = 0;

        public int PrintCounter { get; private set; } = 0;

        public new int Counter { get; private set; } = 0;

        private Printer printer = new Printer();

        private Scanner scanner = new Scanner();

        public Copier(Printer printer, Scanner scanner)
        {
            this.printer = printer;
            this.scanner = scanner;
        }

        public void Print(in IDocument document)
        {
            if (this.GetState() == IDevice.State.off)
            {
                return;
            }

            bool toDisabled = false;
            if (this.printer.GetState() == IDevice.State.off)
            {
                toDisabled = true;
                this.printer.PowerOn();
            }

            ++this.PrintCounter;
            this.printer.Print(in document);

            if (toDisabled)
            {
                this.printer.PowerOff();
            }
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG)
        {
            if (this.GetState() == IDevice.State.off)
            {
                document = null;
                return;
            }

            bool toDisabled = false;
            if (this.scanner.GetState() == IDevice.State.off)
            {
                toDisabled = true;
                this.scanner.PowerOn();
            }

            ++this.ScanCounter;
            this.scanner.Scan(out document, formatType);

            if (toDisabled)
            {
                this.scanner.PowerOff();
            }
        }

        public void ScanAndPrint()
        {
            IDocument document;

            Scan(out document, IDocument.FormatType.JPG);
            Print(document);
        }

    }
}
