using Neo;
using Neo.IO;
using Neo.SmartContract;
using Neo.Wallets;
using System;
using System.Linq;
using System.Text;
using Neo.Network.RPC;
using Neo.SmartContract.Manifest;
using System.IO;
using Neo.VM;
using Neo.Network.P2P.Payloads;
using System.Numerics;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using Neo.Cryptography.ECC;
using Neo.VM.Types;
using Akka.Actor;
using System.Security.Cryptography;
using ECPoint = Neo.Cryptography.ECC.ECPoint;

namespace Neo3Cases
{
    class Program3
    {
        private static RpcClient _rpcClient = new RpcClient(new Uri("http://seed2t5.neo.org:20332"), null, null, ProtocolSettings.Load("config.json", true));
 
        // 
        private static KeyPair keyPair1 = Neo.Network.RPC.Utility.GetKeyPair(" "); //mindy
        private static KeyPair keyPair2 = Neo.Network.RPC.Utility.GetKeyPair(" ");  //test2
      

        private static UInt160 vaultHash = UInt160.Parse("0x3249d51025a018098b797be8fe34ab419151a1a3");

        private static UInt160 FLM = UInt160.Parse("0x1415ab3b409a95555b77bc4ab6a7d9d7be0eddbd");
        private static UInt160 NEO = Neo.SmartContract.Native.NativeContract.NEO.Hash;
        private static UInt160 GAS = Neo.SmartContract.Native.NativeContract.GAS.Hash;
        private static UInt160 fUSDT = UInt160.Parse("0x83c442b5dc4ee0ed0e5249352fa7c75f65d6bfd6");
        private static UInt160 bneo = UInt160.Parse("0x48c40d4666f93408be1bef038b6722404d9a4c2a");
        private static UInt160 Token = UInt160.Parse("0x6c37dc79e9a1c3dd248485bccfa6085c8863aeb4");
        private static UInt160 contract = UInt160.Parse("0x6e644dda08a62f3fc9d14e824d1a3bd816a0c2d5");
        private static UInt160 nft = UInt160.Parse("0x4fb2f93b37ff47c0c5d14cfc52087e3ca338bc56");

        private static UInt160 contract1 = UInt160.Parse("0xd38537e636e0a8844302bd1a9f9c3b8c5a1a91b5");
        private static UInt160 contract2 = UInt160.Parse("0x3775978b4aa77384aaf2767815d8fc83cc0243af");

        private static UInt160 contract3= UInt160.Parse("0x785962e87fdda10085afd6100e4fa5e22d687ed9");
        private static UInt160 bridge = UInt160.Parse("0x2e332afa8db284cafa9e15c655bcec81116dc0e8");
        static void Main()
        {

            //Transfer();
            // UpdateContract();
            DeployContract();
         
        }

        private static void Transfer17Demo()
        {
            KeyPair keyPair = keyPair1;
            byte[] script;

            object[] @params = new object[]
                {
                    GAS,
                    keyPair.GetScriptHash(),
                    "NQUN2zkzwpypEi6kvGYexy8cQKN2ycyJjF".ToScriptHash(0x35),
                    (BigInteger)1000,

                };

            using (ScriptBuilder sb = new ScriptBuilder())


            {
                sb.EmitDynamicCall(contract, "transfer", @params);

                script = sb.ToArray();
            }
            UInt160[] C = new UInt160[] { NEO, GAS };

            Signer[] signers = new[] { new Signer { Scopes = WitnessScope.CustomContracts, Account = keyPair.GetScriptHash(), AllowedContracts = C} };

            Helper.SignAndSendTx(_rpcClient, script, signers, null, keyPair);
        }


        private static void Transfer()
        {
            KeyPair keyPair = keyPair1;
            byte[] script;
            using (ScriptBuilder sb = new ScriptBuilder())
            {
                sb.EmitDynamicCall(GAS, "transfer", keyPair.GetScriptHash(), "NRErVmF1atpD5RpjxQEMxEd7EAEcWsCrN6".ToScriptHash(0x35), (BigInteger)100_000000, "data");
             //   sb.EmitDynamicCall(FLM, "transfer", keyPair.GetScriptHash(), "NNXB81woRQv2GcEYnRwKqpEdFRcHSXp5cm".ToScriptHash(0x35), (BigInteger)1000000_00000000, "data");

                script = sb.ToArray();
            }

            Signer[] signers = new[] { new Signer { Scopes = WitnessScope.Global, Account = keyPair.GetScriptHash() } };

            Helper.SignAndSendTx(_rpcClient, script, signers, null, keyPair);
        }

        private static void DeployContract()
        {
            Console.WriteLine("deploy contract.");

            //合约路径   
             // string path = @"D:\Neo-Project\dora-quadratic-master\Grant_NEP17\Grant_NEP17\bin\sc";
           // string path = @"D:\Neo-Project\DaraHack-Quadratic-git\DaraHack-Quadratic\bin\sc\";
            //string path = @"C:\aa\";
            string path = @"D:\Neo-Project\n3-neon-js\contract\";
            string fileName = "SampleHelloWorld";
            string nefFilePath = path + fileName + ".nef";
            string manifestFilePath = path + fileName + ".manifest.json";

            NefFile nefFile;
            using (var stream = new BinaryReader(File.OpenRead(nefFilePath), Encoding.UTF8, false))
            {
               nefFile = stream.ReadSerializable<NefFile>();
                 
            }
            var mani = File.ReadAllBytes(manifestFilePath);
            ContractManifest manifest = ContractManifest.Parse(mani);

            ContractClient contractClient = new ContractClient(_rpcClient);
            var tx = contractClient.CreateDeployContractTxAsync(nefFile.ToArray(), manifest, keyPair2).Result;

            Console.WriteLine(_rpcClient.SendRawTransactionAsync(tx.ToArray()).Result);

            var contractHash = Neo.SmartContract.Helper.GetContractHash(tx.Sender, nefFile.CheckSum, manifest.Name);
            Console.WriteLine("contract hash:" + contractHash);

            Console.WriteLine($"Transaction {tx.Hash} is broadcasted!");
        }

        private static void UpdateContract()
        {
            Console.WriteLine("update contract.");

            //合约路径
            string path = @"D:\Neo-Project\ContractTest\ContractTest\bin\sc\";
            string fileName = "ContractTest";
            string nefFilePath = path + fileName + ".nef";
            string manifestFilePath = path + fileName + ".manifest.json";

            NefFile nefFile;
            using (var stream = new BinaryReader(File.OpenRead(nefFilePath), Encoding.UTF8, false))
            {
                nefFile = stream.ReadSerializable<NefFile>();
            }
            var mani = File.ReadAllBytes(manifestFilePath);
            ContractManifest manifest = ContractManifest.Parse(mani);

            byte[] script;
            using (ScriptBuilder sb = new ScriptBuilder())
            {
                sb.EmitDynamicCall(bridge, "update", nefFile.ToArray(), manifest.ToJson().ToString());
                script = sb.ToArray();
            }

            Signer[] signers = new[] { new Signer { Scopes = WitnessScope.Global, Account = keyPair1.GetScriptHash() } };

            Helper.SignAndSendTx(_rpcClient, script, signers, null, keyPair1);
        }

        public static void Signtest()
        {
            Console.Write("wif:");
            string wif = Console.ReadLine();

            byte[] prikey = Wallet.GetPrivateKeyFromWIF(wif);
            KeyPair keyPair = new KeyPair(prikey);
            string message = "Thesekindofcontrolsaregoodweverecommendedbeforebuttheyarespecificto";

            byte[] byteMessage = Encoding.UTF8.GetBytes(message);
            var signData = Neo.Cryptography.Crypto.Sign(byteMessage, keyPair.PrivateKey, keyPair.PublicKey.EncodePoint(false)[1..]);

            Console.WriteLine("message:" + Convert.ToBase64String(byteMessage));
            Console.WriteLine("public key:" + Convert.ToBase64String(keyPair.PublicKey.ToArray()));
            Console.WriteLine("sign data:"+ Convert.ToBase64String(signData));
        }

        private static void CreateAccount()
        {
            for (int i = 0; i < 100000000; i++)
            {
                byte[] privateKey = new byte[32];
                using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(privateKey);
                }
                KeyPair key = new KeyPair(privateKey);

                //var publicKey = key.PublicKey.ToString();
                var wif = key.Export();
                var contract = Contract.CreateSignatureContract(key.PublicKey);
                var address = contract.ScriptHash.ToAddress(ProtocolSettings.Default.AddressVersion);

                if (address.ToLower().StartsWith("neo") || address.ToLower().EndsWith("neo"))
                {
                    Console.WriteLine("addr:" + address);
                    Console.WriteLine("wif:" + wif.ToString());

                }
            }

        }

    }
}
