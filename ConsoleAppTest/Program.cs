// See https://aka.ms/new-console-template for more information

using Common;

var timestamp = TimeStampHelper.GetTimeStamp();
var signature1 = SignatureCreator.Create("123", "123123123", timestamp);
var signature2 = SignatureCreator.Create(Console.ReadLine(), "123123123", timestamp);
foreach (var signature in new List<SignatureInfo> { signature1, signature2 }) {
    Console.WriteLine($"Signature: {signature.Signature}, Timestamp: {signature.TimeStamp}");
}