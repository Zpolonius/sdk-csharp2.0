using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Examples
{
    class Program
    {
        public static void Main(string[] args)
        {
            //createPaymentRequest examples
            Console.WriteLine("Executing createPayment examples...");
            CreatePaymentRequestExamples cprExamples = new CreatePaymentRequestExamples();
            cprExamples.CreateSimplePaymentRequest();
            cprExamples.CreateComplexPaymentRequest();

            //capture examples
            Console.WriteLine("Executing capture examples...");
            CaptureExamples captureExamples = new CaptureExamples();
            captureExamples.SimpleCapture();
            captureExamples.SimplePartialCapture();
            captureExamples.ComplexCapture();

            //refund examples
            Console.WriteLine("Executing refund examples...");
            RefundExamples refundExamples = new RefundExamples();
            refundExamples.SimpleRefund();
            refundExamples.SimplePartialRefund();
            refundExamples.ComplexRefund();

            //release examples
            Console.WriteLine("Executing release examples...");
            ReleaseExamples releaseExamples = new ReleaseExamples();
            releaseExamples.Release();
        }
    }
}
