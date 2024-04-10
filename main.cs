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
      // // private static RpcClient _rpcClient = new RpcClient(new Uri("http://20.169.201.3:20332"), null, null, ProtocolSettings.Load("config.json", true));
        //Test
        private static KeyPair keyPair1 = Neo.Network.RPC.Utility.GetKeyPair("L13WmH2wrLEptKStJe2t1DwBNtigy9FQrNuLZMAtE6D9mP5Gw1mt"); //mindy
        private static KeyPair keyPair2 = Neo.Network.RPC.Utility.GetKeyPair("L3xPVzoemKgHD4jkK3i9xiCK9p3UuHwsNhQgMiz6PxxptBWxqpEj");  //test2
        private static KeyPair keyPair3= Neo.Network.RPC.Utility.GetKeyPair("L1vjYo2scbyKvGiTtpt6XBhdDJwpreDbqnwCMMbVMPLWKssbqerK");  //test3
        private static KeyPair keyPairServer = Neo.Network.RPC.Utility.GetKeyPair("L2TpjtsfYrpTg7eARuR4BjuZwVuyigfrGRAZREYy8UWWjTHi547W");
        private static KeyPair validator5 = Neo.Network.RPC.Utility.GetKeyPair("KzwGJdQpNZtkG1xr7r9jnrEMRZPgY2ZAgqL9MQmxeY3Z35wZ4wJp");


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
           // Deposit();
            // UpdateContract();
          //  WithdrawalTetstVerify();
            DeployContract();
            // Register();
            //   SetRecord();
            //  DeleteRecords(); 
            //  SetReverseRecord();
            //  Resolve();
            //string data = "20.25.70.212:8080";
            //bool d = CheckIPv4(data);
            //Console.WriteLine("result:" + d);
            #region
            //   Cancel();
            //UInt160 nep11 = UInt160.Parse("0x4fb2f93b37ff47c0c5d14cfc52087e3ca338bc56");
            //string tokenid = "TWV0YVBhbmFjZWEgIzIwLTAy";
            //Claim(keyPair2,nep11,tokenid);

            //============transfer11======
            //  UInt160 market = contract2;
            // string tokenid = "TWV0YVBhbmFjZWEgIzE5LTAy";
            // string data = "QQUhAQEoFM924ovQBixKR47jVWEBExnzz6TSIQECIQLIACEBAg==";
            //  Transfer11(market,tokenid,data);
            //  DeployContract();
            // SellToOfferer();
            //  BanVote(projectId,voter,invalidVote);
            //Thread.Sleep(15000);
            //test(1,60170);

            //List<string[]> result = GetVoteData();
            //for (int i = 0; i < result.Count; i++)

            //{
            //    BigInteger projectId = BigInteger.Parse(result[i][0].ToString());

            //    UInt160  voter = UInt160.Parse((result[i][1]).ToString());
            //    BigInteger invalidVote = BigInteger.Parse(result[i][2].ToString());

            //    BanVote(projectId, voter, invalidVote);

            //    // AllocateFound(projectId, ss);
            //}

            // BigInteger projectId = BigInteger.Parse("1089990106060171718915444512020358741007375782480");

            //  AllocateFound(projectId, 1000);
            #endregion

        }

        private static void WithdrawalTetstVerify()
        {
            KeyPair keyPair = keyPair2;
            byte[] script;
          
            UInt160 contract = UInt160.Parse("0x2cae0dc9dc004c921a94fbf01a3d764190cba3f1");

            
            
            using (ScriptBuilder sb = new ScriptBuilder())
            {
                object[] @params = new object[]
              {


              };
                sb.EmitDynamicCall(contract, "testEvent");

                script = sb.ToArray();
            }

            Signer[] signers = new[] { new Signer { Scopes = WitnessScope.Global, Account = keyPair.GetScriptHash() } };

            Helper.SignAndSendTx(_rpcClient, script, signers, null, keyPair);
        }
        private static void Deposit()
        {
            KeyPair keyPair = keyPair2;
            byte[] script;

            UInt160 contract = UInt160.Parse("0x64789bde6fd6c0b9b26a1d84b07fb679e0f3639f");

            UInt160 from = UInt160.Parse("0x6fd49ab2f14a6bd9a060bb91fdbf29799a885a9e");
            UInt160 to = UInt160.Parse("0xF5aD3d4e846f33041180Aea32e11137009cC1734");

            using (ScriptBuilder sb = new ScriptBuilder())
            {
                object[] @params = new object[]
              {


              };
                sb.EmitDynamicCall(contract, "deposit", from, to, BigInteger.Parse("200000000"));

                script = sb.ToArray();
            }

            Signer[] signers = new[] { new Signer { Scopes = WitnessScope.Global, Account = keyPair.GetScriptHash() } };

            Helper.SignAndSendTx(_rpcClient, script, signers, null, keyPair);
        }


        private static void EventTest()
        {
            KeyPair keyPair = keyPair2;
            byte[] script;

            UInt160 contract = UInt160.Parse("0xeb04892fb7f3a92b055b88934e1fd05fe734d0e2");

            using (ScriptBuilder sb = new ScriptBuilder())
            {
                object[] @params = new object[]
              {


              };
                sb.EmitDynamicCall(contract, "test", BigInteger.Parse("3"), contract,contract1, BigInteger.Parse("5"));

                script = sb.ToArray();
            }

            Signer[] signers = new[] { new Signer { Scopes = WitnessScope.Global, Account = keyPair.GetScriptHash() } };

            Helper.SignAndSendTx(_rpcClient, script, signers, null, keyPair);
        }
        private static bool CheckIPv4(string ipv4)
        {
            int length = ipv4.Length;
            if (length < 7 || length > 15) return false;
            string[] fragments = ipv4.Split( ".");
            length = fragments.Length;
            if (length != 4) return false;
            byte[] numbers = new byte[4];
            for (int i = 0; i < length; i++)
            {
                string fragment = fragments[i];
                if (fragment.Length == 0) return false;
                byte number = byte.Parse(fragment);
                if (number > 0 && fragment[0] == '0') return false;
                if (number == 0 && fragment.Length > 1) return false;
                numbers[i] = number;
            }
            switch (numbers[0])
            {
                case 0:
                case 10:
                case 100 when numbers[1] >= 64 && numbers[1] <= 127:
                case 127:
                case 169 when numbers[1] == 254:
                case 172 when numbers[1] >= 16 && numbers[1] <= 31:
                case 192 when numbers[1] == 0 && numbers[2] == 0:
                case 192 when numbers[1] == 0 && numbers[2] == 2:
                case 192 when numbers[1] == 88 && numbers[2] == 99:
                case 192 when numbers[1] == 168:
                case 198 when numbers[1] >= 18 && numbers[1] <= 19:
                case 198 when numbers[1] == 51 && numbers[2] == 100:
                case 203 when numbers[1] == 0 && numbers[2] == 113:
                case >= 224:
                    Console.WriteLine( numbers[0]>=224);
                    return false;
            }
            return numbers[3] switch
            {
                0 or 255 => false,
                _ => true,
            };
        }
        private static void Register()
        {
            KeyPair keyPair = keyPair2;
            byte[] script;

            UInt160 contract = UInt160.Parse("0xb611cdc5d9a392f947e1f333c010aebdc9f16b80");

            using (ScriptBuilder sb = new ScriptBuilder())
            {
                object[] @params = new object[]
              {


              };
                sb.EmitDynamicCall(contract, "register", "test2.neo", UInt160.Parse("0x6fd49ab2f14a6bd9a060bb91fdbf29799a885a9e"),"tobey1024@126.com",BigInteger.Parse("2"), BigInteger.Parse("2"), BigInteger.Parse("1669012603"), BigInteger.Parse("2"));

                script = sb.ToArray();
            }

            Signer[] signers = new[] { new Signer { Scopes = WitnessScope.Global, Account = keyPair.GetScriptHash() } };

            Helper.SignAndSendTx(_rpcClient, script, signers, null, keyPair);
        }

        private static void DeleteRecords()
        {
            KeyPair keyPair = keyPair2;
            byte[] script;

            UInt160 contract = UInt160.Parse("0xb611cdc5d9a392f947e1f333c010aebdc9f16b80");

            using (ScriptBuilder sb = new ScriptBuilder())
            {
                object[] @params = new object[]
              {


              };
                sb.EmitDynamicCall(contract, "deleteRecords", "mindy.neo", BigInteger.Parse("6"));

                script = sb.ToArray();
            }

            Signer[] signers = new[] { new Signer { Scopes = WitnessScope.Global, Account = keyPair.GetScriptHash() } };

            Helper.SignAndSendTx(_rpcClient, script, signers, null, keyPair);
        }

        /// <summary>
        /// nns setRecord    
        /// </summary>
        /// 
        private static void Resolve()
        {
            KeyPair keyPair = keyPair2;
            byte[] script;

            UInt160 contract = UInt160.Parse("0xb611cdc5d9a392f947e1f333c010aebdc9f16b80");

            using (ScriptBuilder sb = new ScriptBuilder())
            {
                object[] @params = new object[]
              {


              };
                sb.EmitDynamicCall(contract, "resolve", "mindy.neo", BigInteger.Parse("1"), "baidu.com");

                script = sb.ToArray();
            }

            Signer[] signers = new[] { new Signer { Scopes = WitnessScope.Global, Account = keyPair.GetScriptHash() } };

            Helper.SignAndSendTx(_rpcClient, script, signers, null, keyPair);
        }
        private static void SetRecord()
        {
            KeyPair keyPair = keyPair2;
            byte[] script;

            UInt160 contract = UInt160.Parse("0x50ac1c37690cc2cfc594472833cf57505d5f46de");

            using (ScriptBuilder sb = new ScriptBuilder())
            {
                object[] @params = new object[]
              {


              };
                sb.EmitDynamicCall(contract, "setRecord", "wangmm.neo", BigInteger.Parse("5"), "www.megaoasis.io.");

                script = sb.ToArray();
            }

            Signer[] signers = new[] { new Signer { Scopes = WitnessScope.Global, Account = keyPair.GetScriptHash() } };

            Helper.SignAndSendTx(_rpcClient, script, signers, null, keyPair);
        }

        private static void SetReverseRecord()
        {
            KeyPair keyPair = keyPair2;
            byte[] script;

            UInt160 contract = UInt160.Parse("0x711fd7e746c635c2db8497c0af99b0770a7fe2bf");

            using (ScriptBuilder sb = new ScriptBuilder())
            {
                object[] @params = new object[]
              {


              };
                sb.EmitDynamicCall(contract, "setReverseRecord",UInt160.Parse("0x6fd49ab2f14a6bd9a060bb91fdbf29799a885a9e"), "mindy.neo");

                script = sb.ToArray();
            }

            Signer[] signers = new[] { new Signer { Scopes = WitnessScope.Global, Account = keyPair.GetScriptHash() } };

            Helper.SignAndSendTx(_rpcClient, script, signers, null, keyPair);
        }

        /// <summary>
        /// 读取数据源
        /// </summary>
        /// <returns></returns>
        private static List<string[]> GetVoteData()
        {
            List<string[]> result = new List<string[]>();          
            string path = @"D:\Neo-Project\TestN3\TestN3\test.csv";
            //采用默认ANSI编码格式进行读取
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                //在读取末端进行判断，并循环读取
                while (!sr.EndOfStream)
                { 
                    //输出每一行读取的内容
                    // Console.WriteLine(sr.ReadLine());
                    string line = sr.ReadLine();
                    var arr = line.Split(',');

                    string[] list = { arr[0], arr[4], arr[6]};
                    
                   result.Add(list);

                   
                }

                return result;
            }

        }

         //退票
        private static void AllocateFound(BigInteger pid,BigInteger invaildvote)
        {
            KeyPair keyPair = keyPair1;
            byte[] script;
            using (ScriptBuilder sb = new ScriptBuilder())
            {
                object[] @params = new object[]
              {


              };
                sb.EmitDynamicCall(contract, "allocateFound", pid,invaildvote);

                script = sb.ToArray();
            }

            Signer[] signers = new[] { new Signer { Scopes = WitnessScope.Global, Account = keyPair.GetScriptHash() } };

            Helper.SignAndSendTx(_rpcClient, script, signers, null, keyPair);
        }



        private void GetSetting()
        {
            byte[] script;
            using (ScriptBuilder sb = new ScriptBuilder())
            {
                sb.EmitDynamicCall(Token, "getOwner");
                script = sb.ToArray();
            }
            Helper.InvokeScript(_rpcClient, script);
        }


        private static void Claim()
        {
            KeyPair keyPair = keyPair2;
            byte[] script;

            UInt160 asset =  UInt160.Parse("0x4fb2f93b37ff47c0c5d14cfc52087e3ca338bc56");

            using (ScriptBuilder sb = new ScriptBuilder())
            {
                object[] @params = new object[]
              {


              };
                sb.EmitDynamicCall(contract2, "cancel", asset, "TWV0YVBhbmFjZWEgIzE5LTAy");

                script = sb.ToArray();
            }

            Signer[] signers = new[] { new Signer { Scopes = WitnessScope.Global, Account = keyPair.GetScriptHash() } };

            Helper.SignAndSendTx(_rpcClient, script, signers, null, keyPair);
        }


        //取消上架
        private static void SellToOfferer()
        {
            KeyPair keyPair = keyPair1;
            byte[] script;
            using (ScriptBuilder sb = new ScriptBuilder())
            {
                object[] @params = new object[]
              { 
                   

              };
                sb.EmitDynamicCall(contract1, "sellToOfferer", BigInteger.Parse("29"));

                script = sb.ToArray();
            }

            Signer[] signers = new[] { new Signer { Scopes = WitnessScope.Global, Account = keyPair.GetScriptHash() } };

            Helper.SignAndSendTx(_rpcClient, script, signers, null, keyPair);
        }

        //上架

        private static void Transfer11(UInt160 market, string tokenid, string data)
        {
            KeyPair keyPair = keyPair2;
            byte[] script;
            using (ScriptBuilder sb = new ScriptBuilder())
            {
                sb.EmitDynamicCall(nft, "transfer", market,tokenid,data);

                script = sb.ToArray();
            }

            Signer[] signers = new[] { new Signer { Scopes = WitnessScope.Global, Account = keyPair.GetScriptHash() } };

            Helper.SignAndSendTx(_rpcClient, script, signers, null, keyPair);
        }

        //领取
        private static void Claim(KeyPair key, UInt160 pid, string tokenid)
        {
            KeyPair keyPair = key;
            byte[] script;
            using (ScriptBuilder sb = new ScriptBuilder())
            {
                sb.EmitDynamicCall(contract1, "claim", pid, tokenid);

                script = sb.ToArray();
            }

            Signer[] signers = new[] { new Signer { Scopes = WitnessScope.Global, Account = keyPair.GetScriptHash() } };

            Helper.SignAndSendTx(_rpcClient, script, signers, null, keyPair);
        }



        
        private static void BanVote(BigInteger _projectId,UInt160 _voter,BigInteger _invaildVote)
        {
            KeyPair keyPair = keyPair1;
            byte[] script;
            using (ScriptBuilder sb = new ScriptBuilder())
            {
                sb.EmitDynamicCall(contract, "banVoter", _projectId, _voter,_invaildVote);
               
                script = sb.ToArray();
            }

            Signer[] signers = new[] { new Signer { Scopes = WitnessScope.Global, Account = keyPair.GetScriptHash() } };

            Helper.SignAndSendTx(_rpcClient, script, signers, null, keyPair);
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
